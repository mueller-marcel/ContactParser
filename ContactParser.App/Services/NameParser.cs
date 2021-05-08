using ContactParser.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ContactParser.App.Services
{
    public class NameParser
    {
        /// <summary>
        /// Parse the input contact string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>An instance of type <see cref="Name"/> containing all the information about the contact</returns>
        public Name ParseName(string input)
        {
            //Splitting the input string based on empty characters
            string[] nameElements = input.Split(' ');

            //Determines the gender from the salutaion
            string gender = GetGender(nameElements[0]);

            //Extract the Lastname 
            string lastName = GetNobleName(nameElements);

            //Extract the Firstname 
            string firstName = GetFirstName(nameElements);

            //Extract the Middlename
            string middleName = GetMiddleName(nameElements);

            //Extract the Salutation
            string salutation = GetSalutation(nameElements);

            //Extract the Title 
            string title = GetTitel(nameElements);

            //Build the full Greeting
            string greeting = GetGreeting(lastName, firstName, salutation, title);

            // Fill the Name object to be returned
            Name nameData = new Name
            {
                Gender = gender,
                LastName = lastName,
                FirstName = firstName,
                MiddleName = middleName,
                Salutation = salutation,
                Title = title,
                Greeting = greeting,
            };

            return nameData;
        }

        /// <summary>
        /// Extracts the NobleName
        /// </summary>
        /// <param name="adresselements"></param>
        /// <returns>The lastName as <see cref="string"/></returns>
        public static string GetNobleName(string[] adresselements)
        {
            string lastName = String.Empty;

            string[] nobleIndicator = { "von", "Von", "Vom", "vom", "van", "Van" };


            int pos = -1;
            var adresselementsLength = adresselements.Length;

            foreach (string x in nobleIndicator)
            {
                foreach (string y in adresselements)
                {
                    if (x == y)
                    {
                        pos = Array.IndexOf(adresselements, x);
                        break;
                    }
                }
            }

            if (pos > -1)
            {
                for (int i = pos; i < adresselementsLength; i++)
                {
                    lastName = lastName + adresselements[i] + " ";
                }

                lastName = lastName.Remove(lastName.Length - 1, 1);
                return lastName;
            }
            else
            {
                lastName = adresselements[adresselementsLength - 1];
            }

            return lastName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salutation"></param>
        /// <returns>The gender as <see cref="string"/></returns>
        public static string GetGender(string salutation)
        {
            string[] salutationsMale = { "Herr" };
            string[] salutationsFemale = { "Frau" };
            string gender;


            foreach (string x in salutationsMale)
            {
                if (x == salutation)
                {
                    gender = "male";
                    return gender;
                }
            }
            foreach (string x in salutationsFemale)
            {
                if (x == salutation)
                {
                    gender = "female";
                    return gender;
                }
            }
            gender = "keine Angabe";
            MessageBox.Show("Da das Geschlecht in dem Kontakt  nicht ersichtlich war, wurde keine Angabe angenommen. Bitte das Feld Geschlecht überprüfen!");
            return gender;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adresselements"></param>
        /// <returns>The FirstName as <see cref="string"/></returns>
        static string GetFirstName(string[] adresselements)
        {
            string firstName = ""; 

            return firstName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adresselements"></param>
        /// <returns>The salutation as <see cref="string"/></returns>
        public static string GetSalutation(string[] adresselements)
        {
            string salutation = "";

            return salutation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adresselements"></param>
        /// <returns>The middlename as <see cref="string"/></returns>
        public static string GetMiddleName(string[] adresselements)
        {
            string middleName = "";

            return middleName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adresselements"></param>
        /// <returns>The title as <see cref="string"/></returns>
        static string GetTitel(string[] adresselements)
        {
            string title = "";

            return title;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="salutation"></param>
        /// <param name="title"></param>
        /// <returns>The full greeting as <see cref="string"/></returns>
        public static string GetGreeting(string lastName, string firstName, string salutation, string title)
        {
            if (salutation == "Herr")
            {
                string greetingMale = "Sehr geehrter " + salutation + " " + title + " " + firstName + " " + lastName;
                return greetingMale;
            }

            if (salutation == "Frau")
            {
                string greetingFemale = "Sehr geehrte " + salutation + " " + title + " " + firstName + " " + lastName;
                return greetingFemale;
            }

            return "";
        }
    }
}
