using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class ItemGeneratorConfig
    {
        public ItemGeneratorConfig(List<ItemDefinition> itemDefinitions, List<PropertyDefinition> propertyDefinitions, List<RarityDefinition> raretyDefinitions)
        {
            ItemDefinitions = itemDefinitions;
            PropertyDefinitions = propertyDefinitions;
            RarityDefinitions = raretyDefinitions;
        }

        /// <summary>
        /// All Property Definitions that are possible.
        /// </summary>
        public List<PropertyDefinition> PropertyDefinitions { get; set; }

        /// <summary>
        /// All Rarity Definitions that are possible.
        /// </summary>
        public List<RarityDefinition> RarityDefinitions { get; set; }

        /// <summary>
        /// All Item Definitions that are possible.
        /// </summary>
        public List<ItemDefinition> ItemDefinitions { get; set; }
    }
}
