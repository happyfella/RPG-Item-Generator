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
            var result = CalculationService.GetRandomInteger(definition.MinimumDropLevel, definition.MaximumDropLevel, false);
            return result;
        }
    }
}
