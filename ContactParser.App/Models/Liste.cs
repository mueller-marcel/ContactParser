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
    }
}
