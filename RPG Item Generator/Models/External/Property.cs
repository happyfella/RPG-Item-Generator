using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class Property
    {
        public int Type { get; set; }

        public string Name { get; set; }

        public int MinimumValue { get; set; }

        public int MaximumValue { get; set; }

        public int Value { get; set; }

        public bool ImplicitProperty { get; set; }

        public bool IsValueRanged { get; set; }
    }
}
