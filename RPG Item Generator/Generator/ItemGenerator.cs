using RPG_Item_Generator.Generator.Helpers;
using RPG_Item_Generator.Models.External;
using RPG_Item_Generator.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG_Item_Generator.Generator
{
    public interface IItemGenerator
    {
        /// <summary>
        /// Initialize the configuration of the item generator.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        bool Initialize(ItemGeneratorConfig config);

        /// <summary>
        /// Generates an item based on the passed in level.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        Item GenerateItem(int level);
    }

    public class ItemGenerator : IItemGenerator
    {
        private readonly Initializer initializer;
        private readonly Random random;

        public ItemGenerator()
        {
            initializer = new Initializer();
            random = new Random();
        }

        public bool Initialize(ItemGeneratorConfig config)
        {
            initializer.Init(config);
            return true;

            // Handle if it fails
        }

        public Item GenerateItem(int level)
        {
            var result = new Item();

            // Choose item definition
            var definitions = initializer.GetUsableDefinitions(level);
            var definitionCount = definitions.Count();
            var itemDefinitionIndex = random.Next(0, definitionCount);
            var itemDefinition = definitions[itemDefinitionIndex];

            // Generate values
            var itemLevel = LevelService.GenerateItemLevel(itemDefinition, level);
            var itemRarity = RarityService.ChooseRarity(itemDefinition.Rarities, initializer);
            var itemName = NameService.GenerateItemName(); // TODO: need to dynamicall change the name possibly
            var itemProperties = PropertyService.GenerateProperties(itemDefinition.Properties, itemRarity, initializer);

            // Map item result
            result.Type = itemDefinition.Type;
            result.SubType = itemDefinition.SubType;
            result.ItemLevel = itemLevel;
            result.RarityType = itemRarity.Type;
            result.RarityName = itemRarity.Name;
            result.ItemName = itemDefinition.Name; // TODO: possibly use itemName that was generated
            result.ItemDescription = itemDefinition.Description;
            result.Properties = itemProperties;
            
            return result;
        }
    }
}
