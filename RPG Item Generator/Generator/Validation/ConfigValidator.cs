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
                Result.Errors.Add("RarityDefinitions list was null or had a count of 0.");
                Result.Passed = false;
            }
            else
            {
                // ERROR: Validate DropWeight will sum to 1.00
                var rarityDropWeightSum = _itemGeneratorConfig.RarityDefinitions.Sum(x => x.DropWeight);
                if(rarityDropWeightSum < 1.00 || rarityDropWeightSum > 1.00)
                {
                    Result.Errors.Add($"RarityDefinitions DropWeight adds up to {rarityDropWeightSum}. DropWeight should be summed to 1.00.");
                    Result.Passed = false;
                }
            }
        }

        private void PropertyDefinitionValidation()
        {
            // ERROR: Validate Properties is a valid object with a count
            if(_itemGeneratorConfig.PropertyDefinitions == null || _itemGeneratorConfig.PropertyDefinitions.Count < 1)
            {
                Result.Errors.Add("PropertyDefinitions list was null or had a count of 0.");
                Result.Passed = false;
            }
            else
            {
                // Room for specific 
            }
        }

        private void ItemDefinitionValidation()
        {
            // ERROR: Validate Properties is a valid object with a count
            if(_itemGeneratorConfig.ItemDefinitions == null || _itemGeneratorConfig.ItemDefinitions.Count < 1)
            {
                Result.Errors.Add("ItemDefinitions list was null or had a count of 0.");
                Result.Passed = false;
            }
            else
            {
                // ERROR: Validate Properties is not null
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.ToList();
                    foreach(var d in definitions)
                    {
                        if(d.Properties == null)
                        {
                            Result.Errors.Add($"ItemDefinition Id {d.Id} Properties is null.");
                            Result.Passed = false;
                        }
                    }
                }

                // ERROR: Validate Rarities is not null
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.ToList();
                    foreach (var d in definitions)
                    {
                        if (d.Rarities == null)
                        {
                            Result.Errors.Add($"ItemDefinition Id {d.Id} Rarities is null.");
                            Result.Passed = false;
                        }
                    }
                }

                // WARNING: Validate ItemDefinition has at least 1 Implicit Property. Doesn't check Consumable Items
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.Where(x => !x.IsConsumable).ToList();
                    foreach (var d in definitions)
                    {
                        if(d.Properties != null)
                        {
                            var implicitProperties = _itemGeneratorConfig.PropertyDefinitions.Where(x => d.Properties.Contains(x.Id) && x.ImplicitProperty).ToList();
                            if (implicitProperties.Count < 1)
                            {
                                Result.Warnings.Add($"Item Definition Id {d.Id} has no Implicit Properties.");
                            }
                            var explicitProperties = _itemGeneratorConfig.PropertyDefinitions.Where(x => d.Properties.Contains(x.Id) && !x.ImplicitProperty).ToList();
                            if (explicitProperties.Count < 1)
                            {
                                Result.Warnings.Add($"Item Definition Id {d.Id} has no Explicit Properties.");
                            }
                        }
                    }
                }

                // WARNING: Validate ItemDefinition on IsConsumable if it has Explicit Properties
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.Where(x => x.IsConsumable).ToList();
                    foreach(var d in definitions)
                    {
                        if(d.Properties != null)
                        {
                            var explicitProperties = _itemGeneratorConfig.PropertyDefinitions.Where(x => d.Properties.Contains(x.Id) && !x.ImplicitProperty).ToList();
                            if (explicitProperties.Count > 0)
                            {
                                foreach (var e in explicitProperties)
                                {
                                    Result.Warnings.Add($"Item Definition Id {d.Id} has Explicit Property Id {e.Id}. Explicit Properties will be ignored for Consumable items.");
                                }
                            }
                        }
                    }
                }

                // WARNING: Validate if SetStaticValue is true then IsValueRanged is false
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.ToList();
                    foreach(var d in definitions)
                    {
                        if(d.Properties != null)
                        {
                            var properties = _itemGeneratorConfig.PropertyDefinitions.Where(x => d.Properties.Contains(x.Id) && x.SetStaticValue && x.IsValueRanged).ToList();
                            if (properties.Count > 0)
                            {
                                foreach (var p in properties)
                                {
                                    Result.Warnings.Add($"Item Definition Id {d.Id} has Property Id {p.Id} with SetStaticValue to True and IsValueRanged to True. Property cannot " +
                                        $"have a static value and be a value range.");
                                }
                            }
                        }
                    }
                }

                // WARNING: Validate if SetStaticValue with StaticValue being 0
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.ToList();
                    foreach (var d in definitions)
                    {
                        if(d.Properties != null)
                        {
                            var properties = _itemGeneratorConfig.PropertyDefinitions.Where(x => d.Properties.Contains(x.Id) && x.SetStaticValue && x.StaticValue < 1).ToList();
                            if (properties.Count > 0)
                            {
                                foreach (var p in properties)
                                {
                                    Result.Warnings.Add($"Item Definition Id {d.Id} has Property Id {p.Id} with SetStaticValue to True and the StaticValue of {p.StaticValue}.");
                                }
                            }
                        }
                    }
                }

                // WARNING: Validate if Rarities is greater than 0
                {
                    var definitions = _itemGeneratorConfig.ItemDefinitions.ToList();
                    foreach(var d in definitions)
                    {
                        if(d.Rarities != null)
                        {
                            var rarities = _itemGeneratorConfig.RarityDefinitions.Where(x => d.Rarities.Contains(x.Id)).ToList();
                            if (rarities.Count < 1)
                            {
                                Result.Warnings.Add($"Item Definition Id {d.Id} has no Rarities defined. The item will default to the highest DropWeight value");
                            }
                        }
                    }
                }
            }
        }
    }

    //Hard failures
    //No property should be null within the definitions
}
