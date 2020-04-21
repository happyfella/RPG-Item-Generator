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

        public ConfigValidation Result { get; set; }

        public ConfigValidator(ItemGeneratorConfig itemGeneratorConfig)
        {
            _itemGeneratorConfig = itemGeneratorConfig;
            Result = new ConfigValidation();
            Result.Errors = new List<string>();
            Result.Warnings = new List<string>();
            Result.Passed = true;
        }

        public ConfigValidation Validation()
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
                // WARNING: Validate ItemDefinition has at least 1 Implicit Property. Doesn't check Consumable Items
                var definitions = _itemGeneratorConfig.ItemDefinitions.Where(x => !x.IsConsumable).ToList();
                foreach(var d in definitions)
                {
                    var implicitProperties = _itemGeneratorConfig.PropertyDefinitions.Where(x => d.Properties.Contains(x.Id) && x.ImplicitProperty).ToList();
                    if(implicitProperties.Count < 1)
                    {
                        Result.Warnings.Add($"Item Definition Id {d.Id} has no Implicit Properties");
                    }
                    var explicitProperties = _itemGeneratorConfig.PropertyDefinitions.Where(x => d.Properties.Contains(x.Id) && !x.ImplicitProperty).ToList();
                    if (explicitProperties.Count < 1)
                    {
                        Result.Warnings.Add($"Item Definition Id {d.Id} has no Explicit Properties");
                    }
                }
            }
        }

        
        // Soft failures
        // Consumable items with explicit properties, explicit properties will be ignored
        // SetStaticValue is true but IsValueRanged is also true... StaticValue is be ignored
        // Item Definition doesn't have any rarity definitions
    }
}
