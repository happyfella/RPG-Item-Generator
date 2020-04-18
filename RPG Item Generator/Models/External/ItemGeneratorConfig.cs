using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class ItemGeneratorConfig
    {
        public ItemGeneratorConfig(List<ItemDefinition> items, List<PropertyDefinition> properties, List<RaretyDefinition> rareties)
        {
            Items = items;
            Properties = properties;
            Rarities = rareties;
        }

        public List<PropertyDefinition> Properties { get; set; }

        public List<RaretyDefinition> Rarities { get; set; }

        public List<ItemDefinition> Items { get; set; }
    }
}
