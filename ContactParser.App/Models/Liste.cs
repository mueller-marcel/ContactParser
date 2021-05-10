using System;
using System.Collections.Generic;
using System.Text;

namespace ContactParser.App.Models
{
    class Liste
    {
        /// <summary>
        /// holds the Splittet String of the Contact
        /// </summary>
        public List<string> Elements { get; set; }

        /// <summary>
        /// holds the Titles
        /// </summary>
        public List<string> Title { get; set; }

        /// <summary>
        /// holds the NobleIndicator
        /// </summary>
        public List<string> NobleIndicator { get; set; }

        /// <summary>
        /// holds the SalutationIndicator
        /// </summary>
        public List<string> SalutationIndicator { get; set; }

        /// <summary>
        /// holds the SalutationMale
        /// </summary>
        public List<string> SalutationsMale { get; set; }

        /// <summary>
        /// holds the GenderMale
        /// </summary>
        public List<string> GenderMale { get; set; }

        /// <summary>
        /// holds the SalutationFemale
        /// </summary>
        public List<string> SalutationsFemale { get; set; }

        /// <summary>
        /// holds the GenderFemale
        /// </summary>
        public List<string> GenderFemale { get; set; }

    }
}
