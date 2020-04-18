using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.Internal
{
    internal class Rarity
    {
        public int TypeId { get; set; }

        public string Name { get; set; }

        public int MinimumExplicitProperties { get; set; }

        public int MaximumExplicitProperties { get; set; }
    }
}
