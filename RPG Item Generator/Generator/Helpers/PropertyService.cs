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
            var random = new Random();
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
            var explicitPropertyCount = random.Next(rarity.MinimumExplicitProperties, rarity.MaximumExplicitProperties + 1);

            while (explicitPropertyCount > propertiesTaken && availableExplicitProperties.Count > 0)
            {
                var index = random.Next(0, availableExplicitProperties.Count);
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
            var random = new Random();

            if (property.IsValueRanged)
            {
                result.Value = 0;
                result.MinimumValue = random.Next(property.BaseMinimumValue, (property.BaseMaximumValue / 2) - 1);
                result.MaximumValue = random.Next(property.BaseMaximumValue / 2, property.BaseMaximumValue);
            }
            else
            {
                result.MinimumValue = 0;
                result.MaximumValue = 0;
                result.Value = random.Next(property.BaseMinimumValue, property.BaseMaximumValue);
            }

            return result;
        }

        static private Property MapProperty(PropertyDefinition property, PropertyValue propertyValue)
        {
            var resultProperty = new Property();

            resultProperty.Type = property.Type;
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
