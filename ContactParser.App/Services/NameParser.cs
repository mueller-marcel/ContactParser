using ContactParser.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactParser.App.Services
{
    public class NameParser
    {
        public Name ParseName(string input)
        {

            string[] nameElements = input.Split(' ');


            string gender = GetGender(nameElements[0]);

            string lastName = GetNobleName(nameElements);

            string firstName = GetFirstName(nameElements);

            string middleName = GetMiddleName(nameElements);

            string salutation = GetSalutation(nameElements);

            string title = GetTitel(nameElements);           

            string greeting = GetGreeting(nameElements);


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

        
        static string GetNobleName(string[] adresselements)
        {
            string lastName = String.Empty;

            string[] nobleIndicator = { "von", "Von", "van", "Van" };

            foreach (string x in nobleIndicator)
            {
                int pos = Array.IndexOf(adresselements, x);
                if (pos > -1)
                {
                    
                    for (int i = pos; i < adresselements.Length; i++)
                    {
                        lastName = lastName + adresselements[i] + " ";
                    }
                    string[] result = new string[pos];
                    Array.Copy(adresselements, 0, result, 0, pos);
                    result[pos] = lastName;
                    return lastName;
                }
            }
            return lastName;
        }

        public static string GetGender(string salutation)
        {
            string[] salutationsMale = { "Herr"};
            string[] salutationsFemale = { "Frau" };
            string gender = String.Empty;
            foreach(string x in salutationsMale)
            {
                if(x == salutation)
                {
                    gender = "male";
                    return gender;
                }
            }
            foreach(string x in salutationsFemale)
            {
                if(x == salutation)
                {
                    gender = "female";
                    return gender;
                }
            }
            gender = "diverse";
            return gender;
        }

        static string GetFirstName(string[] adresselements)
        {
            return null;
        }

        static string GetSalutation(string[] adresselements)
        {
            return null;
        }

        static string GetMiddleName(string[] adresselements)
        {
            return null;
        }

        static string GetTitel(string[] adresselements)
        {
            return null;
        }

        static string GetGreeting(string[] adresselements)
        {
            return null;
        }
    }
}
