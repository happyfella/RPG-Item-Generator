using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class RaretyDefinition
    {
        /// <summary>
        /// If you are using an enum, pass the enum value.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Display name of the rarity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Weight on how often an item of this rarity will drop. The sum of all rarities should be 1
        /// </summary>
        public double DropWeight { get; set; }

        /// <summary>
        /// The minimum number of properties this rarity supports.
        /// </summary>
        public int MinimumExplicitProperties { get; set; }

        /// <summary>
        /// The maximum number of properties this rarity supports.
        /// </summary>
        public int MaximumExplicitProperties { get; set; }
    }
}
