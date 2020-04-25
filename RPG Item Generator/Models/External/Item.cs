using RPG_Item_Generator.Models.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class Item
    {
        /// <summary>
        /// TypeId from the ItemDefinition that was passed through the Item Generator.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// CategoryId from the ItemDefinition that was passed through the Item Generator.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Generated item level.
        /// </summary>
        public int ItemLevel { get; set; }

        /// <summary>
        /// Generated Rarity TypeId.
        /// </summary>
        public int RarityTypeId { get; set; }

        /// <summary>
        /// Generated Rarity name.
        /// </summary>
        public string RarityName { get; set; }

        /// <summary>
        /// Generated item name.
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// ItemDescription from the ItemDefinition that was passed through the Item Generator.
        /// </summary>
        public string ItemDescription { get; set; }

        /// <summary>
        /// Generated number of sockets.
        /// </summary>
        public int Sockets { get; set; }

        /// <summary>
        /// Generated item properties.
        /// </summary>
        public List<Property> Properties { get; set; }
    }
}
