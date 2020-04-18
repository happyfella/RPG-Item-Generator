using RPG_Item_Generator.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_Item_Generator.Generator
{
    class Initializer
    {
        private List<ItemDefinition> Definitions { get; set; }

        private List<PropertyDefinition> Properties { get; set; }

        private List<RaretyDefinition> Rarities { get; set; }

        public bool Init(ItemGeneratorConfig config)
        {
            var rarity = config.Items.Select(x => x.Rarities).ToList();
            // Rarity Weight check
            
            Definitions = config.Items;
            Properties = config.Properties;
            Rarities = config.Rarities;
            return true;
        }

        public List<ItemDefinition> GetUsableDefinitions(int level)
        {
            var result = Definitions.Where(x => x.MinimumDropLevel <= level && x.MaximumDropLevel >= level && x.IgnorMaximumDropLevel == false || x.MinimumDropLevel <= level && x.IgnorMaximumDropLevel).ToList();

            return result;
                
            // TODO: need to throw an exception here instead of returning false
        }

        public ItemDefinition GetItemDefinition(int definitionIndex)
        {
            return Definitions[definitionIndex];
        }

        public List<PropertyDefinition> GetUsableProperties(List<int> propertyTypes)
        {
            var result = Properties.Where(x => propertyTypes.Contains(x.Type)).ToList();

            return result;

             // TODO: need to throw an exception here instead of returning false
        }

        public List<RaretyDefinition> GetUsableRarities(List<int> rarityTypes)
        {
            var result = Rarities.Where(x => rarityTypes.Contains(x.Type)).ToList();

            return result;
        }
    }
}
