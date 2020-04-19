using RPG_Item_Generator.Models.External;
using RPG_Item_Generator.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG_Item_Generator.Generator.Helpers
{
    static class PropertyService
    {
        static public List<Property> GenerateProperties(List<int> propertyTypes, Rarity rarity, Initializer initializer)
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
            var explicitPropertyCount = CalculationService.GetRandomInteger(rarity.MinimumExplicitProperties, rarity.MaximumExplicitProperties, false);

            while (explicitPropertyCount > propertiesTaken && availableExplicitProperties.Count > 0)
            {
                var index = CalculationService.GetRandomInteger(0, availableExplicitProperties.Count, true);
                var propertyToAdd = availableExplicitProperties[index];
                var propertyValue = GenerateItemPropertyValue(propertyToAdd);

                var property = MapProperty(propertyToAdd, propertyValue);
                result.Add(property);

                availableExplicitProperties.Remove(propertyToAdd);
                propertiesTaken++;
            }

            return result;
        }

        static private PropertyValue GenerateItemPropertyValue(PropertyDefinition property)
        {
            var result = new PropertyValue();

            result.MinimumValue = 0;
            result.MaximumValue = 0;
            result.Value = 0;

            if (property.IsValueRanged)
            {
                result.MinimumValue = CalculationService.GetRandomInteger(property.BaseMinimumValue, (property.BaseMaximumValue / 2), false);
                result.MaximumValue = CalculationService.GetRandomInteger(property.BaseMaximumValue / 2, property.BaseMaximumValue, false);
            }
            else
            {
                result.Value = CalculationService.GetRandomInteger(property.BaseMinimumValue, property.BaseMaximumValue, false);
            }

            return result;
        }

        static private Property MapProperty(PropertyDefinition property, PropertyValue propertyValue)
        {
            var resultProperty = new Property();

            resultProperty.TypeId = property.TypeId;
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
