using RPG_Item_Generator.Models.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Generator.Helpers
{
    internal class NameService
    {
        public string GenerateItemName(ItemDefinition definition)
        {
            // TODO: dynamicall change the item name based on properties, possibly implement a flag in the item definition to do a dynamic name or not
            return definition.Name;
        }
    }
}
