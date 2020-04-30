using RPG_Item_Generator.Generator.Validation;
using RPG_Item_Generator.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_Item_Generator.Generator
{
    internal class Initializer
    {
        private List<ItemDefinition> Definitions { get; set; }

        private List<PropertyDefinition> Properties { get; set; }

        private List<RarityDefinition> Rarities { get; set; }

        public ValidationResponse Init(ItemGeneratorConfig config)
        {
            var validate = new ConfigValidator(config).Validation();
            if(validate.Passed)
            {
                Definitions = config.ItemDefinitions;
                Properties = config.PropertyDefinitions;
                Rarities = config.RarityDefinitions;
                return validate;
            }
            else
            {
                return validate;
            }
        }

        public List<ItemDefinition> GetUsableDefinitions(int level)
        {
            var result = Definitions.Where(x => x.MinimumDropLevel <= level && x.MaximumDropLevel >= level && !x.IgnoreMaximumDropLevel || x.MinimumDropLevel <= level && x.IgnoreMaximumDropLevel).ToList();
            return result;
        }

        public List<ItemDefinition> GetUsableDefinitions(int level, bool consumable)
        {
            var result = Definitions.Where(x => (x.MinimumDropLevel <= level && x.MaximumDropLevel >= level && !x.IgnoreMaximumDropLevel || x.MinimumDropLevel <= level && x.IgnoreMaximumDropLevel) && x.IsConsumable == consumable).ToList();
            return result;
        }

        public List<ItemDefinition> GetUsableDefinitionsByTypeId(int level, int typeId)
        {
            var result = Definitions.Where(x => (x.MinimumDropLevel <= level && x.MaximumDropLevel >= level && !x.IgnoreMaximumDropLevel || x.MinimumDropLevel <= level && x.IgnoreMaximumDropLevel) && x.TypeId == typeId).ToList();
            return result;
        }

        public List<ItemDefinition> GetUsableDefinitionsByCategoryId(int level, int categoryId)
        {
            var result = Definitions.Where(x => (x.MinimumDropLevel <= level && x.MaximumDropLevel >= level && !x.IgnoreMaximumDropLevel || x.MinimumDropLevel <= level && x.IgnoreMaximumDropLevel) && x.CategoryId == categoryId).ToList();
            return result;
        }

        public ItemDefinition GetItemDefinition(int definitionIndex)
        {
            return Definitions[definitionIndex];
        }

        public List<PropertyDefinition> GetUsableProperties(List<int> propertyTypes)
        {
            var result = Properties.Where(x => propertyTypes.Contains(x.Id)).ToList();
            return result;
        }

        public List<RarityDefinition> GetUsableRarities(List<int> rarityTypes)
        {
            var result = Rarities.Where(x => rarityTypes.Contains(x.Id)).ToList();
            return result;
        }

        public RarityDefinition GetHighestDropWeightRarity()
        {
            var result = Rarities.OrderByDescending(x => x.DropWeight);
            return result.FirstOrDefault();
        }
    }
}
