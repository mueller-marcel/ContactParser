using System;
using System.Collections.Generic;
using System.Text;

namespace ContactParser.App.Models
{
    public class Name
    {
        /// <summary>
        /// holds the value for the Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// holds the value for the LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// holds the value for the FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// holds the value for the Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// holds the value for the Salutation
        /// </summary>
        public string Salutation { get; set; }

        /// <summary>
        /// holds the value for the Greeting
        /// </summary>
        public string Greeting { get; set; }
    }
}
