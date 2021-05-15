using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using ContactParser.App.Models;
namespace ContactParser.App.Services


{
    public class NameParser : IDisposable
    {
        /// <summary>
        /// Holds the file name for the data file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Holds the content of the data file
        /// </summary>
        public string JsonContent { get; set; }

        /// <summary>
        /// Initializes the access to the title file
        /// </summary>
        public NameParser()
        {
            using (var titleManager = new TitleManager())
            {
                try
                {
                    titleManager.CreateTitleFile();
                    FileName = titleManager.TitlePath;
                    JsonContent = titleManager.TitleContent;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private string ValidateAndPrepareInput(string input)
        {
            // Declarations
            Regex regexNumbers = new Regex(@"[0-9/\<>|{}:;?\\´`'#^°_!§$%&()\[\]+~@€²³*""]");
            Regex regexWhiteSpaces = new Regex(@"\s+");

            // Trim whitspaces
            input = input.Trim();

            // If Input contains several Whitespaces in a row
            if (regexWhiteSpaces.IsMatch(input))
            {
                input = Regex.Replace(input, @"\s+", " ");
            }

            // If Input contains Numbers or no Whitspaces
            if (!input.Contains(" "))
            {
                throw new FormatException("Input must contain at least first and lastname");
            }

            // If Input contains Number
            if (regexNumbers.IsMatch(input))
            {
                throw new ArgumentException("Input can only contain characters a-z, A-Z . , -");
            }

            return input;
        }

        /// <summary>
        /// Parse the input contact string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>An instance of type <see cref="Name"/> containing all the information about the contact</returns>
        /// <exception cref="FormatException">Thrown, when no firstname was entered</exception>
        public Name ParseName(string input)
        {
            // Validate input
            input = ValidateAndPrepareInput(input);

            // Splitting the input string to List based on empty characters
            List<string> nameElements = input.Split(' ').ToList();
            TitleDTO titles = new TitleDTO();
            titles.Elements = nameElements;

            // Determines the gender from the salutaion
            string gender = GetGender(nameElements[0]);

            // Extract the Salutation
            string salutation = GetSalutation(nameElements);

            // Extract the Lastname 
            string lastName = GetNobleName(titles.Elements);

            // Extract the Title 
            string title = GetTitle(titles.Elements);

            string firstName;
            try
            {
                // Extract the Firstname 
                firstName = GetFirstName(titles.Elements);
            }
            catch (FormatException e)
            {
                throw e;
            }

            // Extract the Middlename
            string middleName = GetMiddleName(titles.Elements);


            string firstNameTemp, lastNameTemp, salutationTemp, titleTemp;

            // If last char of lastName = "," then store lastname in firstname, and firstname in lastName and remove ","
            List<string> names = ChangeFirstAndLastName(firstName, lastName);
            firstNameTemp = names[0];
            lastNameTemp = names[1];


            // If salutation = "keine Angabe" replace with "" for the Greeting
            if (salutation.Equals("keine Angabe"))
            {
                salutationTemp = "";
            }
            else
            {
                salutationTemp = salutation;
            }

            // If title = "keine Angabe" replace with "" for the Greeting
            if (title == "keine Angabe")
            {
                titleTemp = "";
            }
            else
            {
                titleTemp = title;
            }

            if (middleName == "keine Angabe")
            {
                middleName = "";
            }

            // Build the full Greeting
            string greeting = GetGreeting(lastNameTemp, firstNameTemp, salutationTemp, titleTemp);

            // If greeting contains no titel and salutation denn Replace
            greeting = greeting.Replace("   ", " ");

            // If greeting contains no titel or salutation denn Replace
            greeting = greeting.Replace("  ", " ");

            // Fill the Name object to be returned
            Name nameData = new Name
            {
                Gender = gender,
                LastName = lastNameTemp,
                FirstName = firstNameTemp + " " + middleName,
                MiddleName = middleName,
                Salutation = salutation,
                Title = title,
                Greeting = greeting,
            };

            return nameData;
        }

        /// <summary>
        /// Extract the NobleName
        /// </summary>
        /// <param name="adresselement"></param>
        /// <returns>The lastName as <see cref="string"/></returns>
        private string GetNobleName(List<string> adresselement)
        {
            // Initializations
            string lastName = string.Empty;
            TitleDTO nameElements = new TitleDTO();
            TitleDTO titles = JsonSerializer.Deserialize<TitleDTO>(JsonContent);
            int pos = -1;

            // Check for noble names
            foreach (string x in titles.NobleIndicator)
            {
                foreach (string y in adresselement)
                {
                    if (x == y)
                    {
                        pos = adresselement.IndexOf(x);
                        break;
                    }
                }
            }

            // If no noble name take last elements surname
            if (pos > -1)
            {
                for (int i = pos; i < adresselement.Count; i++)
                {
                    lastName = lastName + adresselement[i] + " ";
                    if (adresselement[i].EndsWith(","))
                    {
                        break;
                    }
                }

                for (int i = pos; i < adresselement.Count; i++)
                {
                    if (adresselement[i].EndsWith(","))
                    {
                        adresselement.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        adresselement.RemoveAt(i);
                        i -= 1;
                    }

                }
                lastName = lastName.Remove(lastName.Length - 1, 1);
                nameElements.Elements = adresselement;
                return lastName;
            }
            else
            {
                lastName = adresselement[adresselement.Count - 1];
                adresselement.RemoveAt(adresselement.Count - 1);
                nameElements.Elements = adresselement;
            }
            return lastName;
        }

        /// <summary>
        /// Gets the Gender
        /// </summary>
        /// <param name="salutation"></param>
        /// <returns>The gender as <see cref="string"/></returns>
        private string GetGender(string salutation)
        {
            // Initializations
            TitleDTO titles = JsonSerializer.Deserialize<TitleDTO>(JsonContent);
            string gender;
            int position;

            // Return correct gender for men
            foreach (string x in titles.SalutationsMale)
            {
                if (x == salutation)
                {
                    position = titles.SalutationsMale.IndexOf(x);
                    gender = titles.GenderMale[position];
                    return gender;
                }
            }

            // Return correct gender for women
            foreach (string x in titles.SalutationsFemale)
            {
                if (x == salutation)
                {
                    position = titles.SalutationsFemale.IndexOf(x);
                    gender = titles.GenderFemale[position];
                    return gender;
                }
            }
            gender = "keine Angabe";
            return gender;
        }

        /// <summary>
        /// Extract the FirstName
        /// </summary>
        /// <param name="adresselement"></param>
        /// <returns>The FirstName as <see cref="string"/></returns>
        /// <exception cref="FormatException">Thrown, when no firstname could be recognized</exception>
        private string GetFirstName(List<string> adresselement)
        {
            try
            {
                string firstName = adresselement[0];
                adresselement.RemoveAt(0);
                return firstName;
            }
            catch (Exception)
            {
                throw new FormatException("There must be at least a firstname and a lastname");
            }
        }


        /// <summary>
        /// Extracts the Salutation
        /// </summary>
        /// <param name="adresselement"></param>
        /// <returns>The salutation as <see cref="string"/></returns>
        private string GetSalutation(List<string> adresselement)
        {
            string salutation;
            TitleDTO titles = JsonSerializer.Deserialize<TitleDTO>(JsonContent);
            int position = 0;

            foreach (string x in titles.SalutationIndicator)
            {
                if (x == adresselement[0])
                {
                    position = titles.SalutationIndicator.IndexOf(x);
                    adresselement.RemoveAt(0);
                    break;
                }
            }

            // Add salutation according to the position
            salutation = titles.SalutationIndicator[position];
            TitleDTO nameElements = new TitleDTO();
            nameElements.Elements = adresselement;
            return salutation;
        }

        /// <summary>
        /// Extract the MiddleName
        /// </summary>
        /// <param name="adresselement"></param>
        /// <returns>The middlename as <see cref="string"/></returns>
        private string GetMiddleName(List<string> adresselement)
        {

            string middleName = string.Empty;
            if (adresselement.Count > 0)
            {
                for (int i = 0; i < adresselement.Count; i++)
                {
                    middleName = middleName + adresselement[i] + " ";
                }
                middleName = middleName.Remove(middleName.Length - 1, 1);
            }
            else
            {
                middleName = "keine Angabe";
            }
            return middleName;
        }

        /// <summary>
        /// Extract the Title
        /// </summary>
        /// <param name="adresselement"></param>
        /// <returns>The title as <see cref="string"/></returns>
        private string GetTitle(List<string> adresselement)
        {
            // Initializations
            string title = string.Empty;
            TitleDTO titles = JsonSerializer.Deserialize<TitleDTO>(JsonContent);
            TitleDTO nameElements = new TitleDTO();
            int pos = -1;

            // Search for recognized title in the titles list until no titles are found anymore
            foreach (string x in adresselement)
            {
                foreach (string y in titles.Title)
                {
                    if (x == y)
                    {
                        pos = adresselement.IndexOf(x);
                        title = title + adresselement[pos] + " ";
                    }
                }
            }

            // Delete title from address title
            for (int i = pos; i >= 0; i--)
            {
                adresselement.RemoveAt(i);

            }

            nameElements.Elements = adresselement;

            if (title == string.Empty)
            {
                title = "keine Angabe";
                return title;
            }

            // Delete last whitespace
            title = title.Remove(title.Length - 1, 1);
            return title;
        }

        /// <summary>
        /// Create the Greeting
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="salutation"></param>
        /// <param name="title"></param>
        /// <returns>The full greeting as <see cref="string"/></returns>     
        private string GetGreeting(string lastName, string firstName, string salutation, string title)
        {
            //German
            if (salutation.Equals("Herr") || salutation.Equals("Herrn") || salutation.Equals("herr") || salutation.Equals("herrn"))
            {
                string greeting = "Sehr geehrter " + "Herr" + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            else if (salutation.Equals("Frau") || salutation.Equals("frau"))
            {
                string greeting = "Sehr geehrte " + "Frau" + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            //English
            else if (salutation.Equals("Mr") || salutation.Equals("Ms") || salutation.Equals("Mrs") || salutation.Equals("Mr.") || salutation.Equals("Ms.") || salutation.Equals("Mrs."))
            {
                string greeting = "Dear " + salutation + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            //Italy
            else if (salutation.Equals("Signora"))
            {
                string greeting = "Gentie " + salutation + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            else if (salutation.Equals("Sig."))
            {
                string greeting = "Egregio " + salutation + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            //France
            else if (salutation.Equals("Mme") || salutation.Equals("Mme."))
            {
                string greeting = "Madame " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            else if (salutation.Equals("M") || salutation.Equals("M."))
            {
                string greeting = "Monsieur " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            //Espanol
            else if (salutation.Equals("Senora"))
            {
                string greeting = "Estimada " + salutation + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            else if (salutation.Equals("Senor"))
            {
                string greeting = "Estimado " + salutation + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }

            else
            {
                string greeting = "Guten Tag " + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
        }

        /// <summary>
        /// if input name like "lastName, firstName" change the names and remove the ","
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>A list with firstName and lastName <see cref="List{String}"/></returns>
        private List<string> ChangeFirstAndLastName(string firstName, string lastName)
        {
            List<string> names = new List<string>();

            // Change names if the firstname ends with comma
            if (firstName.EndsWith(","))
            {
                firstName = firstName.Remove(firstName.Length - 1);
                names.Add(lastName);
                names.Add(firstName);
            }
            else
            {
                names.Add(firstName);

                if (lastName.EndsWith(","))
                {
                    lastName = lastName.Remove(lastName.Length - 1);
                    names.Add(lastName);
                }
                else
                {
                    names.Add(lastName);
                }
            }

            return names;
        }

        /// <summary>
        /// Releases the managed properties
        /// </summary>
        public void Dispose()
        {
            JsonContent = null;
            FileName = null;
        }
    }
}
