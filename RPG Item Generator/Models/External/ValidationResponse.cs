using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Models.External
{
    public class ValidationResponse
    {
        /// <summary>
        /// True is validation has passed, false if validation has failed. The Item Generator will only generate an item when this value is true.
        /// </summary>
        public bool Passed { get; set; }

        /// <summary>
        /// List of warnings from the validation process, should be reviewed but won't stop any item from being generated.
        /// </summary>
        public List<string> Warnings { get; set; }

        /// <summary>
        /// List of errors from the validation process. If this list is greater than 0, Passed will be false.
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
