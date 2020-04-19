using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    /// <summary>
    /// Defines the item property. ie... Attack Speed, 
    /// </summary>
    public class PropertyDefinition
    {
        public int TypeId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Base item property, if true this property will always be on the item regardless of weight or rarity
        /// </summary>
        public bool ImplicitProperty { get; set; }

        /// <summary>
        /// Scales the values with help from the item level. Currently not implemented
        /// </summary>
        public double ValueScale { get; set; }

        public int BaseMinimumValue { get; set; }

        public int BaseMaximumValue { get; set; }

        /// <summary>
        /// Is the property value a range or single value. ie... value 2 - 11 or value 5
        /// </summary>
        public bool IsValueRanged { get; set; }

        /// <summary>
        /// How often this property will be on an item. Value from 0 to 1
        /// </summary>
        public double Wieght { get; set; }

        /// <summary>
        /// Hard value that will be set to the property with no additional random generating.
        /// </summary>
        public int StaticValue { get; set; }

        /// <summary>
        /// Flag for StaticValue. True: will apply the StaticValue to the property. False: Will not apply the StaticValue to the property.
        /// </summary>
        public bool SetStaticValue { get; set; }
    }
}
