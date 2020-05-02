using RPG_Item_Generator.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_Item_Generator.Generator.Validation
{
    internal class ConfigValidator
    {
        private readonly ItemGeneratorConfig _itemGeneratorConfig;

        public ValidationResponse Result { get; set; }

        public ConfigValidator(ItemGeneratorConfig itemGeneratorConfig)
        {
            _itemGeneratorConfig = itemGeneratorConfig;
            Result = new ValidationResponse();
            Result.Errors = new List<string>();
            Result.Warnings = new List<string>();
            Result.Passed = true;
        }

        public ValidationResponse Validation()
        {
            RarityDefinitionValidation();
            PropertyDefinitionValidation();
            ItemDefinitionValidation();

            return Result;
        }

        private void RarityDefinitionValidation()
        {
            // ERROR: Validate Rarities is a valid object with a count
            if(_itemGeneratorConfig.RarityDefinitions == null || _itemGeneratorConfig.RarityDefinitions.Count < 1)
            {
                Result.Errors.Add("ERROR: RarityDefinitions list was null or had a count of 0.");
                Result.Passed = false;
            }
            else
            {
                var allDefinitions = _itemGeneratorConfig.RarityDefinitions.ToList();

                // ERROR: Validate DropWeight will sum to 1.00
                {
                    var rarityDropWeightSum = _itemGeneratorConfig.RarityDefinitions.Sum(x => x.DropWeight);
                    if (rarityDropWeightSum < 1.00 || rarityDropWeightSum > 1.00)
                    {
                        Result.Errors.Add($"ERROR: RarityDefinitions DropWeight adds up to {rarityDropWeightSum}. DropWeight should be summed to 1.00.");
                        Result.Passed = false;
                    }
                }

                // WARNING: Validate there are no duplicate DropWeight values
                {
                    foreach(var a in allDefinitions)
                    {
                        var definitionsWithoutCurrent = allDefinitions.Where(x => x.Id != a.Id).ToList();
                        foreach(var b in definitionsWithoutCurrent)
                        {
                            if(b.DropWeight == a.DropWeight)
                            {
                                Result.Warnings.Add($"WARNING: Rarity Id {a.Id} has a duplicate DropWeight with Rarity Id {b.Id}. The system will only generate" +
                                    $"one of these Rarities and skip the other.");
                            }
                        }
                    }
                }
            }
        }

        private void PropertyDefinitionValidation()
        {
            // ERROR: Validate Properties is a valid object with a count
            if(_itemGeneratorConfig.PropertyDefinitions == null || _itemGeneratorConfig.PropertyDefinitions.Count < 1)
            {
                Result.Errors.Add("ERROR: PropertyDefinitions list was null or had a count of 0.");
                Result.Passed = false;
            }
            else
            {
                var allProperties = _itemGeneratorConfig.PropertyDefinitions.ToList();

                // ERROR: Validate MinimumValue is less than MaximumValue 
                {
                    foreach (var p in allProperties)
                    {
                        if(p.IsValueRanged)
                        {
                            if(p.MinimumValue > p.MaximumValue)
                            {
                                Result.Errors.Add($"ERROR: Property {p.Id} has a MinimumValue greater than its MaximumValue.");
                                Result.Passed = false;
                            }
                        }
                    }
                }

                // ERROR: Validate MinimumValue and MaximumValue are not 0
                {
                    foreach (var p in allProperties)
                    {
                        if (p.IsValueRanged)
                        {
                            if (p.MinimumValue == 0)
                            {
                                Result.Errors.Add($"ERROR: Property {p.Id} has a MinimumValue of {p.MinimumValue}.");
                                Result.Passed = false;
                            }

                            if(p.MaximumValue == 0)
                            {
                                Result.Errors.Add($"ERROR: Property {p.Id} has a MaximumValue of {p.MaximumValue}.");
                                Result.Passed = false;
                            }
                        }
                    }
                }

                // WARNING: Validate if SetStaticValue is true then IsValueRanged is false
                {
                    var properties = _itemGeneratorConfig.PropertyDefinitions.Where(x => x.SetStaticValue && x.IsValueRanged).ToList();
                    if (properties.Count > 0)
                    {
                        foreach (var p in properties)
                        {
                            Result.Warnings.Add($"WARNING: Property Id {p.Id} has SetStaticValue set to True and IsValueRanged to True. Property cannot " +
                                $"have a static value and be a value range.");
                        }
                    }
                }

                // WARNING: Validate if SetStaticValue with StaticValue being 0
                {
                    var properties = _itemGeneratorConfig.PropertyDefinitions.Where(x => x.SetStaticValue && x.StaticValue < 1).ToList();
                    if (properties.Count > 0)
                    {
                        foreach (var p in properties)
                        {
                            Result.Warnings.Add($"WARNING: Property Id {p.Id} has SetStaticValue set to True and the StaticValue of {p.StaticValue}.");
                        }
                    }
                }

                // WARNING: Validate if SetStaticValue to false with StaticValue being greater than 0
                {
                    var properties = _itemGeneratorConfig.PropertyDefinitions.Where(x => !x.SetStaticValue && x.StaticValue > 0).ToList();
                    if (properties.Count > 0)
                    {
                        foreach (var p in properties)
                        {
                            Result.Warnings.Add($"WARNING: Property Id {p.Id} has SetStaticValue set to False and the StaticValue of {p.StaticValue}.");
                        }
                    }
                }
            }
        }

        private void ItemDefinitionValidation()
        {
            // ERROR: Validate Properties is a valid object with a count
            if(_itemGeneratorConfig.ItemDefinitions == null || _itemGeneratorConfig.ItemDefinitions.Count < 1)
            {
                Result.Errors.Add("ERROR: ItemDefinitions list was null or had a count of 0.");
                Result.Passed = false;
            }
            else
            {
                var allDefinitions = _itemGeneratorConfig.ItemDefinitions.ToList();
                // ERROR: Validate Properties is not null
                {
                    foreach(var d in allDefinitions)
                    {
                        if(d.PropertyIds == null)
                        {
                            Result.Errors.Add($"ERROR: ItemDefinition Id {d.Id} Properties is null.");
                            Result.Passed = false;
                        }
                    }
                }

                // ERROR: Validate Rarities is not null
                {
                    foreach (var d in allDefinitions)
                    {
                        if (d.RarityIds == null)
                        {
                            Result.Errors.Add($"ERROR: ItemDefinition Id {d.Id} Rarities is null.");
                            Result.Passed = false;
                        }
                    }
                }

                // ERROR: Validate MaximumSocket is greater than MinimumSocket when IsSocketed is true
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.Where(x => x.IsSocketed).ToList();
                    foreach (var d in definitions)
                    {
                        if (d.MaximumSocket < d.MinimumSocket)
                        {
                            Result.Warnings.Add($"ERROR: Item Definition Id {d.Id} MaximumSocket is less than MinimumSocket.");
                            Result.Passed = false;
                        }
                    }
                }

                // WARNING: Validate MinimumDropLevel is less than or equal to MaximumDropLevel when IgnoreMaximumDropLevel is false
                {
                    foreach (var d in allDefinitions)
                    {
                        if(d.MinimumDropLevel > d.MaximumDropLevel && !d.IgnoreMaximumDropLevel)
                        {
                            Result.Warnings.Add($"WARNING: Item Definition Id {d.Id} has a MinimumDropLevel higher than the MaximumDropLevel.");
                        }
                    }
                }

                // WARNING: Validate ItemDefinition has at least 1 Implicit Property. Doesn't check Consumable Items
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.Where(x => !x.IsConsumable).ToList();
                    foreach (var d in definitions)
                    {
                        if(d.PropertyIds != null)
                        {
                            var implicitProperties = _itemGeneratorConfig.PropertyDefinitions.Where(x => d.PropertyIds.Contains(x.Id) && x.ImplicitProperty).ToList();
                            if (implicitProperties.Count < 1)
                            {
                                Result.Warnings.Add($"WARNING: Item Definition Id {d.Id} has no Implicit Properties.");
                            }
                            var explicitProperties = _itemGeneratorConfig.PropertyDefinitions.Where(x => d.PropertyIds.Contains(x.Id) && !x.ImplicitProperty).ToList();
                            if (explicitProperties.Count < 1)
                            {
                                Result.Warnings.Add($"WARNING: Item Definition Id {d.Id} has no Explicit Properties.");
                            }
                        }
                    }
                }

                // WARNING: Validate ItemDefinition on IsConsumable if it has Explicit Properties
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.Where(x => x.IsConsumable).ToList();
                    foreach(var d in definitions)
                    {
                        if(d.PropertyIds != null)
                        {
                            var explicitProperties = _itemGeneratorConfig.PropertyDefinitions.Where(x => d.PropertyIds.Contains(x.Id) && !x.ImplicitProperty).ToList();
                            if (explicitProperties.Count > 0)
                            {
                                foreach (var e in explicitProperties)
                                {
                                    Result.Warnings.Add($"WARNING: Item Definition Id {d.Id} has Explicit Property Id {e.Id}. Explicit Properties will be ignored for Consumable items.");
                                }
                            }
                        }
                    }
                }

                // WARNING: Validate if Rarities is greater than 0
                {
                    foreach(var d in allDefinitions)
                    {
                        if(d.RarityIds != null)
                        {
                            var rarities = _itemGeneratorConfig.RarityDefinitions.Where(x => d.RarityIds.Contains(x.Id)).ToList();
                            if (rarities.Count < 1)
                            {
                                Result.Warnings.Add($"WARNING: Item Definition Id {d.Id} has no Rarities defined. The item will default to the highest DropWeight value");
                            }
                        }
                    }
                }

                // WARNING: Validate MaximumSocket is greater than 0 when IsSocketed is true
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.Where(x => x.IsSocketed).ToList();
                    foreach(var d in definitions)
                    {
                        if(d.MaximumSocket < 1)
                        {
                            Result.Warnings.Add($"WARNING: Item Definition Id {d.Id} with IsSocketed set to True with a MaximumSocket of {d.MaximumSocket}");
                        }
                    }
                }

                // WARNING: Validate MaximumSocket is 0 when IsSocketed is false
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.Where(x => !x.IsSocketed).ToList();
                    foreach (var d in definitions)
                    {
                        if (d.MaximumSocket > 0)
                        {
                            Result.Warnings.Add($"WARNING: Item Definition Id {d.Id} with IsSocketed set to False with a MaximumSocket of {d.MaximumSocket}");
                        }
                    }
                }

                // WARNING: Validate on IsConsumable is true and IsSocketed is false
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.Where(x => x.IsConsumable && x.IsSocketed).ToList();
                    foreach (var d in definitions)
                    {
                        Result.Warnings.Add($"WARNING: Item Definition Id {d.Id} with IsConsumable set to True and IsSocketed set to True. Consumable items cannot have Sockets.");
                    }
                }
            }
        }
    }
}
