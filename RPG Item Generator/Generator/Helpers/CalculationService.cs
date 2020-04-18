using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Generator.Helpers
{
    static class CalculationService
    {
        static public int GetRandomInteger(int min, int max, bool usedForIndexing)
        {
            var random = new Random();

            // Random will not produce the max value as a result. Add 1 to max if we are using it for value generation and not for list indexing.
            max = usedForIndexing ? max : max + 1;
            return random.Next(min, max);
        }

        static public double GetRandomDouble()
        {
            var random = new Random();
            return random.NextDouble();
        }
    }
}
