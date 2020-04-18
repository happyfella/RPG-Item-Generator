using RPG_Item_Generator.Models.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class Item
    {
        public int Type { get; set; }

        public int SubType { get; set; }

        public int ItemLevel { get; set; }

        public int RarityType { get; set; }

        public string RarityName { get; set; }

        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        //public double DamagePerSecond { get; set; } // Not sure on this one yet

        public List<Property> Properties { get; set; }
    }
}
