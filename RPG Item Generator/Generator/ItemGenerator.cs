﻿using RPG_Item_Generator.Generator.Helpers;
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
        ConfigValidation Initialize(ItemGeneratorConfig config);

        /// <summary>
        /// Generates an item (consumable and non-consumable) based on the passed in level.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        Item GenerateItem(int level);

        /// <summary>
        /// Generates a non-consumable item  based on the passed in level.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        Item GenerateNonConsumableItem(int level);

        /// <summary>
        /// Generates a consumable item based on the passed in level. This is not the same as item durability.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        Item GenerateConsumableItem(int level);

        /// <summary>
        /// Generates an item based on the passed in level and TypeId.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        Item GenerateItemOnTypeId(int level, int typeId);

        /// <summary>
        /// Generates an item based on the passed in level and CategoryId.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Item GenerateItemOnCategoryId(int level, int categoryId);
    }

    public class ItemGenerator : IItemGenerator
    {
        private readonly Initializer _initializer;
        private readonly PropertyService _propertyService;
        private readonly LevelService _levelService;
        private readonly NameService _nameService;
        private readonly RarityService _rarityService;
        private readonly CalculationService _calculationService;

        private bool PassedValidation { get; set; }

        public ItemGenerator()
        {
            _initializer = new Initializer();
            _propertyService = new PropertyService();
            _levelService = new LevelService();
            _nameService = new NameService();
            _rarityService = new RarityService();
            _calculationService = new CalculationService();
        }

        public ConfigValidation Initialize(ItemGeneratorConfig config)
        {
            var initialize = _initializer.Init(config);
            PassedValidation = initialize.Passed;
            return initialize;
        }

        public Item GenerateItem(int level)
        {
            if(PassedValidation)
            {
                var definitions = _initializer.GetUsableDefinitions(level);
                if (definitions.Count > 0)
                {
                    var result = GetItem(level, definitions);
                    return result;
                }
            }
            
            return new Item(); // Need to handle this better, what to return if no item was generated or throw custom exception
        }

        public Item GenerateNonConsumableItem(int level)
        {
            if(PassedValidation)
            {
                var definitions = _initializer.GetUsableDefinitions(level, false);
                if (definitions.Count > 0)
                {
                    var result = GetItem(level, definitions);
                    return result;
                }
            }

            return new Item(); // Need to handle this better, what to return if no item was generated
        }

        public Item GenerateConsumableItem(int level)
        {
            if (PassedValidation)
            {
                var definitions = _initializer.GetUsableDefinitions(level, true);
                if (definitions.Count > 0)
                {
                    var result = GetItem(level, definitions);
                    return result;
                }
            }

            return new Item(); // Need to handle this better, what to return if no item was generated
        }

        public Item GenerateItemOnTypeId(int level, int typeId)
        {
            if (PassedValidation)
            {
                var definitions = _initializer.GetUsableDefinitionsByTypeId(level, typeId);
                if (definitions.Count > 0)
                {
                    var result = GetItem(level, definitions);
                    return result;
                }
            }

            return new Item(); // Need to handle this better, what to return if no item was generated
        }

        public Item GenerateItemOnCategoryId(int level, int categoryId)
        {
            if (PassedValidation)
            {
                var definitions = _initializer.GetUsableDefinitionsByCategoryId(level, categoryId);
                if (definitions.Count > 0)
                {
                    var result = GetItem(level, definitions);
                    return result;
                }
            }

            return new Item(); // Need to handle this better, what to return if no item was generated
        }

        private Item GetItem(int level, List<ItemDefinition> definitions)
        {
            var result = new Item();

            var definitionCount = definitions.Count();
            var itemDefinitionIndex = _calculationService.GetRandomInteger(0, definitionCount, true);
            var itemDefinition = definitions[itemDefinitionIndex];

            // Generate values
            var itemLevel = _levelService.GenerateItemLevel(itemDefinition, level);
            var itemRarity = _rarityService.ChooseRarity(itemDefinition.Rarities, _initializer);
            var itemName = _nameService.GenerateItemName(); // TODO: need to dynamicall change the name possibly
            var itemProperties = _propertyService.GenerateProperties(itemDefinition.Properties, itemRarity, _initializer);

            // Map item result
            result.TypeId = itemDefinition.TypeId;
            result.CategoryId = itemDefinition.CategoryId;
            result.ItemLevel = itemLevel;
            result.RarityTypeId = itemRarity.TypeId;
            result.RarityName = itemRarity.Name;
            result.ItemName = itemDefinition.Name; // TODO: possibly use itemName that was generated
            result.ItemDescription = itemDefinition.Description;
            result.Properties = itemProperties;

            return result;
        }
    }
}
