using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class Property
    {
        /// <summary>
        /// TypeId from the PropertyDefinition that was passed through the Item Generator.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Name from the PropertyDefinition that was passed through the Item Generator.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If IsValueRanged is true, this will have the generated MinimumValue. If IsValueRanged is false, this will be 0.
        /// </summary>
        public int MinimumValue { get; set; }

        /// <summary>
        /// If IsValueRanged is true, this will have the generated MaximumValue. If IsValueRanged is false, this will be 0.
        /// </summary>
        public int MaximumValue { get; set; }

        /// <summary>
        /// If IsValueRanged is false, this will have the generated Value. If IsValueRanged is true, this will be 0.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// ImplicitProperty from the PropertyDefinition that was passed through the Item Generator.
        /// </summary>
        public bool ImplicitProperty { get; set; }

        /// <summary>
        /// IsValueRanged from the PropertyDefinition that was passed through the Item Generator.
        /// </summary>
        public bool IsValueRanged { get; set; }
    }
}
