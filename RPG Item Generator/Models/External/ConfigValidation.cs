using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class ConfigValidation
    {
        public bool Passed { get; set; }

        public List<string> Warnings { get; set; }

        public List<string> Errors { get; set; }
    }
}
