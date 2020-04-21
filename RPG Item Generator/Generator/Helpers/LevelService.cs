using RPG_Item_Generator.Models.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Generator.Helpers
{
    internal class LevelService
    {
        private readonly CalculationService _calculationService;

        public LevelService()
        {
            _calculationService = new CalculationService();
        }

        public int GenerateItemLevel(ItemDefinition definition, int level)
        {
            var result = _calculationService.GetRandomInteger(definition.MinimumDropLevel, definition.MaximumDropLevel, false);
            return result;
        }
    }
}
