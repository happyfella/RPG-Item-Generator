using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class ItemDefinition
    {
        /// <summary>
        /// Unique Id for the Item Definition. Used for error handling, will not be returned. 
        /// Unique is not enforced, but will assist if you receive any errors or warnings with a referenced Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The TypeId for the generated Item.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// The CategoryId for the generated Item.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// The Name for the generated Item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Description for the generated Item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Item will not generate if this value is above the level passed in during item generation. 
        /// </summary>
        public int MinimumDropLevel { get; set; }

        /// <summary>
        /// Item will not generate if this value is below the level passed in during item generation. This value will be ignored if IgnoreMaximumDropLevel is true.
        /// </summary>
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
        public bool IsConsumable { get; set; }

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
