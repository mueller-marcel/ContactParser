using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
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
                    FileName = titleManager.GetOrCreateFile();
                    JsonContent = titleManager.GetContent();
                }
                catch (Exception)
                {
                    throw new IOException("File could not be created/read");
                }
            }
        }

        /// <summary>
        /// Parse the input contact string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>An instance of type <see cref="Name"/> containing all the information about the contact</returns>
        public Name ParseName(string input)
        {

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

            // Extract the Firstname 
            string firstName = GetFirstName(titles.Elements);

            // Extract the Middlename
            string middleName = GetMiddleName(titles.Elements);


            string firstNameTemp, lastNameTemp, salutationTemp, titleTemp;

            // If last char of lastName = "," then store lastname in firstname, and firstname in lastName and remove ","
            List<string> names = ChangeFirstAndLastName(firstName, lastName);
            firstNameTemp = names[0];
            lastNameTemp = names[1];


            // If salutation = "keine Angabe" replace with "" for the Greeting
            if (salutation == "keine Angabe")
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
        private string GetFirstName(List<string> adresselement)
        {
            string firstName;
            firstName = adresselement[0];
            adresselement.RemoveAt(0);
            return firstName;
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
            if (salutation == "Herr" || salutation == "Herrn")
            {
                string greeting = "Sehr geehrter " + "Herr" + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            else if (salutation == "Frau")
            {
                string greeting = "Sehr geehrte " + salutation + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            //English
            else if (salutation == "Mr" || salutation == "Ms" || salutation == "Mrs" || salutation == "Mr." || salutation == "Ms." || salutation == "Mrs.")
            {
                string greeting = "Dear " + salutation + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            //Italy
            else if (salutation == "Signora")
            {
                string greeting = "Gentie " + salutation + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            else if (salutation == "Sig.")
            {
                string greeting = "Egregio " + salutation + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            //France
            else if (salutation == "Mme" || salutation == "Mme.")
            {
                string greeting = "Madame " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            else if (salutation == "M" || salutation == "M.")
            {
                string greeting = "Monsieur " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            //Espanol
            else if (salutation == "Senora")
            {
                string greeting = "Estimada " + salutation + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }
            else if (salutation == "Senor")
            {
                string greeting = "Estimado " + salutation + " " + title + " " + firstName + " " + lastName;
                return greeting;
            }

            else
            {
                string greeting = "Guten Tag " + " " + title + " " + firstName + " " + lastName;

                if (firstName != "keine Angabe")
                {
                    MessageBox.Show("Bitte überprüfe die Vorgeschlagene Anrede.");
                }
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
