using RPG_Item_Generator.Models.External;
using RPG_Item_Generator.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_Item_Generator.Generator.Helpers
{
    internal class RarityService
    {
        private readonly CalculationService _calculationService;

        public RarityService()
        {
            _calculationService = new CalculationService();
        }

        public Rarity ChooseRarity(List<int> itemRarety, Initializer initializer)
        {
            var result = new Rarity();
            var chosenRarity = new RarityDefinition();

            var usableRarities = initializer.GetUsableRarities(itemRarety).OrderByDescending(x => x.DropWeight).ToList();
            if(usableRarities.Count > 0)
            {
                if(usableRarities.Count != 1)
                {
                    var highestRarityValue = usableRarities.FirstOrDefault();
                    var brokerValue = Math.Round(_calculationService.GetRandomDouble(), 2);

                    if (brokerValue > highestRarityValue.DropWeight)
                    {
                        brokerValue = highestRarityValue.DropWeight;
                    }

                    foreach (var u in usableRarities)
                    {

                        if (u.DropWeight >= brokerValue)
                        {
                            chosenRarity = u;
                        }
                    }
                }
                else
                {
                    chosenRarity = usableRarities.FirstOrDefault();
                }
                
            }
            else
            {
                // If the ItemDefinition didn't have a Rarity specified, grab from the main list.
                chosenRarity = initializer.GetHighestDropWeightRarity();
            }

            result.TypeId = chosenRarity.Id;
            result.Name = chosenRarity.Name;
            result.MinimumExplicitProperties = chosenRarity.MinimumExplicitProperties;
            result.MaximumExplicitProperties = chosenRarity.MaximumExplicitProperties;

            return result;
        }
    }
}
