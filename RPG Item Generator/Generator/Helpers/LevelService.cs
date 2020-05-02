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

        public int GenerateItemLevel(ItemDefinition definition, int level, int levelScale)
        {
            var minimumLevel = level - levelScale;
            var maximumLevel = level + levelScale;

            if(levelScale > 0)
            {
                if (minimumLevel < definition.MinimumDropLevel)
                {
                    minimumLevel = definition.MinimumDropLevel;
                }

                if (maximumLevel > definition.MaximumDropLevel)
                {
                    maximumLevel = definition.MaximumDropLevel;
                }
            }
            else
            {
                minimumLevel = definition.MinimumDropLevel;
                maximumLevel = definition.MaximumDropLevel;
            }
            

            var result = 0;
            if(definition.IgnoreMaximumDropLevel)
            {
                result = level;
            }
            else
            {
                result = _calculationService.GetRandomInteger(minimumLevel, maximumLevel, false);
            }
            
            return result;
        }
    }
}
