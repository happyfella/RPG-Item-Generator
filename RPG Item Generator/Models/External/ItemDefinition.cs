using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class ItemDefinition
    {
        public int TypeId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MinimumDropLevel { get; set; }

        public int MaximumDropLevel { get; set; }

        /// <summary>
        /// This will allow an item to generate regardless of MaximumDropLevel while maintaining the MinimumDropLevel integrety
        /// </summary>
        public bool IgnoreMaximumDropLevel { get; set; }

        /// <summary>
        /// Ignores MinimumDropLevel and MaximumDropLevel. This should only be used for consumable items (Gold, Health Potions...) that will drop throughout the game lifecycle.
        /// Returned Item Level will be 0 if this is true. Properties still apply, but should only be Implicit Properties since Explicit Properties
        /// will be ignored.
        /// </summary>
        public bool Consumable { get; set; }

        /// <summary>
        /// List of Property Id's that are allowed to be picked for the item when generating.
        /// </summary>
        public List<int> Properties { get; set; }

        /// <summary>
        /// List of Rarity Id's that are allowed to be picked for the item when generating.
        /// </summary>
        public List<int> Rarities { get; set; }
    }
}
