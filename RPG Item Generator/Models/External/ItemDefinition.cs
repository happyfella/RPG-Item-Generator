using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class ItemDefinition
    {
        public int Type { get; set; }

        public int SubType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MinimumDropLevel { get; set; }

        public int MaximumDropLevel { get; set; }

        /// <summary>
        /// This will allow an item to generate regardless of MaximumDropLevel
        /// </summary>
        public bool IgnorMaximumDropLevel { get; set; }

        public List<int> Properties { get; set; }

        /// <summary>
        /// Droprate for all rarities should add up to 1
        /// </summary>
        public List<int> Rarities { get; set; }
    }
}
