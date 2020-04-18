using RPG_Item_Generator.Models.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Generator.Helpers
{
    static class LevelService
    {
        static public int GenerateItemLevel(ItemDefinition definition, int level)
        {
            var random = new Random();
            var result = random.Next(definition.MinimumDropLevel, definition.MaximumDropLevel + 1);
            return result;
        }
    }
}
