using RPG_Item_Generator.Models.External;
using RPG_Item_Generator.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_Item_Generator.Generator.Helpers
{
    internal class PropertyService
    {
        private readonly CalculationService _calculationService;

        public PropertyService()
        {
            _calculationService = new CalculationService();
        }

        public List<Property> GenerateProperties(bool isConsumable, List<int> propertyTypes, Rarity rarity, Initializer initializer)
        {
            var result = new List<Property>();
            var properties = initializer.GetUsableProperties(propertyTypes);

            // TODO: take weight into account for both number of properties on the item and the values

            // Implicit properties first
            var implicitProperties = properties.Where(x => x.ImplicitProperty).ToList();

            if(implicitProperties.Count > 0)
            {
                foreach (var i in implicitProperties)
                {
                    var propertyValue = GenerateItemPropertyValue(i);
                    var mappedProperty = MapProperty(i, propertyValue);
                    result.Add(mappedProperty);
                }
            }

            // Generate explicit properties
            var propertiesTaken = 0;
            var availableExplicitProperties = properties.Where(x => x.ImplicitProperty == false).ToList();
            var explicitPropertyCount = _calculationService.GetRandomInteger(rarity.MinimumExplicitProperties, rarity.MaximumExplicitProperties, false);

            while (explicitPropertyCount > propertiesTaken && availableExplicitProperties.Count > 0 && !isConsumable)
            {
                var index = _calculationService.GetRandomInteger(0, availableExplicitProperties.Count, true);
                var propertyToAdd = availableExplicitProperties[index];
                var propertyValue = GenerateItemPropertyValue(propertyToAdd);

                var property = MapProperty(propertyToAdd, propertyValue);
                result.Add(property);

                availableExplicitProperties.Remove(propertyToAdd);
                propertiesTaken++;
            }

            return result;
        }

        private PropertyValue GenerateItemPropertyValue(PropertyDefinition property)
        {
            var result = new PropertyValue();

            result.MinimumValue = 0;
            result.MaximumValue = 0;
            result.Value = 0;

            if (property.IsValueRanged && !property.SetStaticValue)
            {
                result.MinimumValue = _calculationService.GetRandomInteger(property.MinimumValue, (property.MaximumValue / 2), false);
                result.MaximumValue = _calculationService.GetRandomInteger(property.MaximumValue / 2, property.MaximumValue, false);
            }
            else
            {
                if(property.SetStaticValue)
                {
                    result.Value = property.StaticValue;
                }
                else
                {
                    result.Value = _calculationService.GetRandomInteger(property.MinimumValue, property.MaximumValue, false);
                }
            }

            return result;
        }

        private Property MapProperty(PropertyDefinition property, PropertyValue propertyValue)
        {
            var resultProperty = new Property();

            resultProperty.TypeId = property.Id;
            resultProperty.Name = property.Name;
            resultProperty.MinimumValue = propertyValue.MinimumValue;
            resultProperty.MaximumValue = propertyValue.MaximumValue;
            resultProperty.Value = propertyValue.Value;
            resultProperty.ImplicitProperty = property.ImplicitProperty;
            resultProperty.IsValueRanged = property.IsValueRanged;

            return resultProperty;
        }
    }
}
