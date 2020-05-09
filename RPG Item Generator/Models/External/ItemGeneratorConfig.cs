using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class ItemGeneratorConfig
    {
        public ItemGeneratorConfig(List<ItemDefinition> itemDefinitions, List<PropertyDefinition> propertyDefinitions, 
            List<RarityDefinition> raretyDefinitions, int itemLevelScale, int itemLevelCap)
        {
            ItemDefinitions = itemDefinitions;
            PropertyDefinitions = propertyDefinitions;
            RarityDefinitions = raretyDefinitions;
            ItemLevelScale = itemLevelScale;
            LevelCap = itemLevelCap;
        }

        private ItemGeneratorConfig() { }

        /// <summary>
        /// Keeps the generated item level +- this value.
        /// </summary>
        public int ItemLevelScale { get; set; }

        /// <summary>
        /// Maximum Level any item can generate at. This value is used to control Property ValueScale from climbing past the MaximumValue.
        /// </summary>
        public int LevelCap { get; set; }

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
