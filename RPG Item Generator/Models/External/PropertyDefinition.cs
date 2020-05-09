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
        /// <summary>
        /// Unique Id for the Property Definition.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name for the generated property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Base item property, if true this property will always be on the item regardless of weight or rarity
        /// </summary>
        public bool ImplicitProperty { get; set; }

        /// <summary>
        /// The generated value will not fall below this value.
        /// </summary>
        public int MinimumValue { get; set; }

        /// <summary>
        /// The generated value will not exceed this value.
        /// </summary>
        public int MaximumValue { get; set; }

        /// <summary>
        /// Is the property value a range or single value. ie... value 2 - 11 or value 5
        /// </summary>
        public bool IsValueRanged { get; set; }

        /// <summary>
        /// CURRENTLY NOT IMPLEMENTED. How rare this property will be on an item. Value from 0 to 1
        /// </summary>
        public double SelectionWieght { get; set; }

        /// <summary>
        /// Hard value that will be set to the property with no additional random generating.
        /// </summary>
        public int StaticValue { get; set; }

        /// <summary>
        /// Flag for StaticValue. True: will apply the StaticValue to the property. False: Will not apply the StaticValue to the property. Cannot be applied to
        /// Ranged property value, only single value properties.
        /// </summary>
        public bool SetStaticValue { get; set; }
    }
}
