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
        /// holds the value for the MiddleName
        /// </summary>
        public string MiddleName { get; set; }

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

        /// <summary>
        /// Override the equals method
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if all values are equal</returns>
        public override bool Equals(object obj)
        {
            // Simple null check
            if (obj is null)
            {
                return false;
            }

            // Reference equality means value equality
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // Check for the exact runtime type
            if (GetType() != obj.GetType())
            {
                return false;
            }

            // Check whether all values are equal
            Name name = obj as Name;
            bool isGenderEqual = Gender.Equals(name.Gender);
            bool isLastNameEqual = LastName.Equals(name.LastName);
            bool isMiddleNameEqual = MiddleName.Equals(name.MiddleName);
            bool isTitleEqual = Title.Equals(name.Title);
            bool isSalutationEqual = Salutation.Equals(name.Salutation);
            bool isGreetingEqual = Greeting.Equals(name.Greeting);
            return isGenderEqual && isLastNameEqual && isLastNameEqual && isMiddleNameEqual && isTitleEqual && isSalutationEqual && isGreetingEqual;
        }

        /// <summary>
        /// Calls the hash function of the base class
        /// </summary>
        /// <returns>A hash code for the current instance</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
