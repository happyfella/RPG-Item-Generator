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

        public List<Property> GenerateProperties(int itemLevel, ItemDefinition definition, Rarity rarity, Initializer initializer)
        {
            var result = new List<Property>();
            var properties = initializer.GetUsableProperties(definition.PropertyIds);
            var scaler = _calculationService.GetScaledValue(itemLevel, initializer.ItemOverallLevelCap);

            // Implicit properties
            var implicitProperties = properties.Where(x => x.ImplicitProperty).ToList();

            if(implicitProperties.Count > 0)
            {
                foreach (var i in implicitProperties)
                {
                    var propertyValue = GenerateItemPropertyValue(scaler, i);
                    var mappedProperty = MapProperty(i, propertyValue);
                    result.Add(mappedProperty);
                }
            }

            // Explicit Properties
            var propertiesTaken = 0;
            var availableExplicitProperties = properties.Where(x => x.ImplicitProperty == false).ToList();
            var explicitPropertyCount = _calculationService.GetRandomInteger(rarity.MinimumExplicitProperties, rarity.MaximumExplicitProperties, false);

            while (explicitPropertyCount > propertiesTaken && availableExplicitProperties.Count > 0 && !definition.IsConsumable)
            {
                var index = _calculationService.GetRandomInteger(0, availableExplicitProperties.Count, true);
                var propertyToAdd = availableExplicitProperties[index];
                var propertyValue = GenerateItemPropertyValue(scaler, propertyToAdd);

                var property = MapProperty(propertyToAdd, propertyValue);
                result.Add(property);

                availableExplicitProperties.Remove(propertyToAdd);
                propertiesTaken++;
            }

            return result;
        }

        private PropertyValue GenerateItemPropertyValue(double scaler, PropertyDefinition property)
        {
            var result = new PropertyValue();

            result.MinimumValue = 0;
            result.MaximumValue = 0;
            result.Value = 0;

            if (property.IsValueRanged && !property.SetStaticValue)
            {
                var minValue = _calculationService.GetRandomInteger(property.MinimumValue, property.MaximumValue / 2, false) * scaler;
                var maxValue = _calculationService.GetRandomInteger(property.MaximumValue / 2, property.MaximumValue, false) * scaler;

                result.MinimumValue = (int)Math.Ceiling(minValue);
                result.MaximumValue = (int)Math.Ceiling(maxValue);
            }
            else
            {
                if(property.SetStaticValue)
                {
                    result.Value = property.StaticValue;
                }
                else
                {
                    var value = _calculationService.GetRandomInteger(property.MinimumValue, property.MaximumValue, false) * scaler;
                    result.Value = (int)Math.Ceiling(value);
                }
            }

            return result;
        }

        private Property MapProperty(PropertyDefinition property, PropertyValue propertyValue)
        {
            var resultProperty = new Property();

            resultProperty.Id = property.Id;
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
