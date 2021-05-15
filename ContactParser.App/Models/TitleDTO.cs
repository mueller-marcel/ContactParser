using System.Collections.Generic;

namespace ContactParser.App.Models
{
    public class TitleDTO
    {
        #region Properties
        /// <summary>
        /// Holds the Splittet String of the Contact
        /// </summary>
        public List<string> Elements { get; set; }

        /// <summary>
        /// Holds the Titles
        /// </summary>
        public List<string> Title { get; set; }

        /// <summary>
        /// Holds the NobleIndicator
        /// </summary>
        public List<string> NobleIndicator { get; set; }

        /// <summary>
        /// Holds the SalutationIndicator
        /// </summary>
        public List<string> SalutationIndicator { get; set; }

        /// <summary>
        /// Holds the SalutationMale
        /// </summary>
        public List<string> SalutationsMale { get; set; }

        /// <summary>
        /// Holds the GenderMale
        /// </summary>
        public List<string> GenderMale { get; set; }

        /// <summary>
        /// Holds the SalutationFemale
        /// </summary>
        public List<string> SalutationsFemale { get; set; }

        /// <summary>
        /// Holds the GenderFemale
        /// </summary>
        public List<string> GenderFemale { get; set; }
        #endregion
    }
}
