using RPG_Item_Generator.Models.External;
using RPG_Item_Generator.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_Item_Generator.Generator.Helpers
{
    static class RarityService
    {
        static public Rarity ChooseRarity(List<int> itemRarety, Initializer initializer)
        {
            var random = new Random();
            var result = new Rarity();
            var chosenRarity = new RaretyDefinition();

            var usableRarities = initializer.GetUsableRarities(itemRarety).OrderByDescending(x => x.DropWeight);
            var highestRarityValue = usableRarities.FirstOrDefault();
            var brokerValue = Math.Round(random.NextDouble(), 2);

            if (brokerValue > highestRarityValue.DropWeight)
            {
                brokerValue = highestRarityValue.DropWeight;
            }

            foreach(var u in usableRarities)
            {
                if(u.DropWeight >= brokerValue)
                {
                    chosenRarity = u;
                }
            }

            result.Type = chosenRarity.Type;
            result.Name = chosenRarity.Name;
            result.MinimumExplicitProperties = chosenRarity.MinimumExplicitProperties;
            result.MaximumExplicitProperties = chosenRarity.MaximumExplicitProperties;

            return result;
        }
    }
}
