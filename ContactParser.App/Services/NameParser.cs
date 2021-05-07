using System;
using System.Collections.Generic;
using System.Text;

namespace ContactParser.App.Services
{
    public class NameParser
    {
        static string[] GetNobleName(string[] adresselements)
        {
            string[] nobleIndicator = ["von", "Von", "van", "Van"];
            foreach (string x in nobleIndicator)
            {
                int pos = Array.IndexOf(adresselements, x);
                if (pos > -1)
                {
                    string lastName = String.Empty;
                    for (int i = pos; i < adresselements.Length; i++)
                    {
                        lastName = lastName + adresselements[i] + " ";
                    }
                    string[] result = new string[pos];
                    Array.Copy(adresselements, 0, result, 0, pos);
                    result[pos] = lastName;
                    return result;
                }
            }
            return adresselements;
        }

        static string GetGender(string salutation)
        {
            string[] salutationsMale = [];
            string[] salutationsFemale = [];
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
            return gender;
        }

        static void Main(string[] args)
        {
            string address = Console.ReadLine();
            string[] addresselements = address.Split(' ');
            string[] addressWithLastName = GetNobleName(addresselements);
            string gender = GetGender(addressWithLastName[0]);


        }
    }
}
