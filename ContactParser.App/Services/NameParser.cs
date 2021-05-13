using ContactParser.App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows;
namespace ContactParser.App.Services


{
    public class NameParser
    {
        public static string fileName = @"C:\Users\josua\source\repos\ContactParser\ContactParser.App\Data\Data.json";
        public static string jsonString = File.ReadAllText(fileName);

        /// <summary>
        /// Parse the input contact string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>An instance of type <see cref="Name"/> containing all the information about the contact</returns>
        public static Name ParseName(string input)
        {

            //Splitting the input string to List based on empty characters
            List<string> elements = input.Split(' ').ToList();
                        
            Liste el = new Liste();

            el.Elements = elements;

            //Determines the gender from the salutaion
            string gender = GetGender(elements[0]);

            //Extract the Salutation
            string salutation = GetSalutation(elements);


            //Extract the Lastname 
            string lastName = GetNobleName(el.Elements);

            //Extract the Title 
            string title = GetTitle(el.Elements);

            //Extract the Firstname 
            string firstName = GetFirstName(el.Elements);

            //Extract the Middlename
            string middleName = GetMiddleName(el.Elements);


            string fN, lN, sal, ti;

            //if last char of lastName = "," then store lastname in firstname, and firstname in lastName and remove ","
            List<string> lNfN = ChangeFirstAndLastName(firstName, lastName);
            fN = lNfN[0];
            lN = lNfN[1];


            //if salutation = "keine Angabe" replace with "" for the Greeting
            if (salutation == "keine Angabe")
            {
                sal = "";
            }
            else
            {
                sal = salutation;
            }

            //if title = "keine Angabe" replace with "" for the Greeting
            if (title == "keine Angabe")
            {
                ti = "";
            }
            else
            {
                ti = title;
            }

            if (middleName == "keine Angabe")
            {
                middleName = "";
            }

            //Build the full Greeting
            string greeting = GetGreeting(lN, fN, sal, ti);

            //if greeting contains no titel and salutation denn Replace
            greeting = greeting.Replace("   ", " ");
            //if greeting contains no titel or salutation denn Replace
            greeting = greeting.Replace("  ", " ");

            // Fill the Name object to be returned
            Name nameData = new Name
            {
                Gender = gender,
                LastName = lN,
                FirstName = fN + " " + middleName,
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
        public static string GetNobleName(List<string> adresselement)
        {
            string lastName = String.Empty;

            Liste el = new Liste();

            Liste nobleIndicator = JsonSerializer.Deserialize<Liste>(jsonString);

            int pos = -1;

            foreach (string x in nobleIndicator.NobleIndicator)
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

                el.Elements = adresselement;

                return lastName;
            }
            else
            {
                lastName = adresselement[adresselement.Count - 1];
                adresselement.RemoveAt(adresselement.Count - 1);

                el.Elements = adresselement;
            }



            return lastName;
        }

        /// <summary>
        /// Gets the Gender
        /// </summary>
        /// <param name="salutation"></param>
        /// <returns>The gender as <see cref="string"/></returns>
        public static string GetGender(string salutation)
        {

            Liste salutationsMale = JsonSerializer.Deserialize<Liste>(jsonString);
            Liste genderMale = JsonSerializer.Deserialize<Liste>(jsonString);
            Liste salutationsFemale = JsonSerializer.Deserialize<Liste>(jsonString);
            Liste genderFemale = JsonSerializer.Deserialize<Liste>(jsonString);

            string gender;

            int pos;

            foreach (string x in salutationsMale.SalutationsMale)
            {
                if (x == salutation)
                {
                    pos = salutationsMale.SalutationsMale.IndexOf(x);
                    gender = genderMale.GenderMale[pos];
                    return gender;
                }
            }
            foreach (string x in salutationsFemale.SalutationsFemale)
            {
                if (x == salutation)
                {
                    pos = salutationsFemale.SalutationsFemale.IndexOf(x);
                    gender = genderFemale.GenderFemale[pos];
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
        public static string GetFirstName(List<string> adresselement)
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
        public static string GetSalutation(List<string> adresselement)
        {
            string salutation;

            Liste salutationIndicator = JsonSerializer.Deserialize<Liste>(jsonString);

            int pos = 0;
            foreach (string x in salutationIndicator.SalutationIndicator)
            {
                if (x == adresselement[0])
                {
                    pos = salutationIndicator.SalutationIndicator.IndexOf(x);
                    adresselement.RemoveAt(0);
                    break;
                }
            }

            salutation = salutationIndicator.SalutationIndicator[pos];

            Liste el = new Liste();

            el.Elements = adresselement;

            return salutation;

        }

        /// <summary>
        /// Extract the MiddleName
        /// </summary>
        /// <param name="adresselement"></param>
        /// <returns>The middlename as <see cref="string"/></returns>
        public static string GetMiddleName(List<string> adresselement)
        {

            string middleName = "";
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
        public static string GetTitle(List<string> adresselement)
        {
            string title = "";

            Liste titleIndicators = JsonSerializer.Deserialize<Liste>(jsonString);


            Liste el = new Liste();
            int pos = -1;

            foreach (string x in adresselement)
            {
                foreach (string y in titleIndicators.Title)
                {
                    if (x == y)
                    {
                        pos = adresselement.IndexOf(x);
                        title = title + adresselement[pos] + " ";
                    }
                }
            }

            for (int i = pos; i >= 0; i--)
            {
                adresselement.RemoveAt(i);

            }

            el.Elements = adresselement;

            if (title == "")
            {
                title = "keine Angabe";

                return title;
            }

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
        public static string GetGreeting(string lastName, string firstName, string salutation, string title)
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
        public static List<string> ChangeFirstAndLastName(string firstName, string lastName)
        {
            List<string> lNfN = new List<string>();

            if (firstName.EndsWith(","))
            {
                firstName = firstName.Remove(firstName.Length - 1);
                lNfN.Add(lastName);
                lNfN.Add(firstName);
            }
            else
            {
                lNfN.Add(firstName);

                if (lastName.EndsWith(","))
                {
                    lastName = lastName.Remove(lastName.Length - 1);
                    lNfN.Add(lastName);
                }
                else
                {
                    lNfN.Add(lastName);
                }
            }

            return lNfN;
        }
    }


}
