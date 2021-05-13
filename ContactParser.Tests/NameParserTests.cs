using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactParser.App.Models;
using System.Collections.Generic;
using System;

namespace ContactParser.Tests
{
    [TestClass]
    public class NameParserTests
    {
        /// <summary>
        /// Test a spanish female name
        /// </summary>
        [TestMethod]
        public void ValidSpanishFemaleName()
        {
            // Define expected name
            Name expectedName = new Name
            {
                Gender = "femenino",
                LastName = "van de Güllefass-Stinkstiefel",
                FirstName = "Sibille Isolde",
                MiddleName = "Isolde",
                Salutation = "Senora",
                Title = "Dr.-Ing. Dr. rer. nat. Dr. h.c. mult.",
                Greeting = "Estimada Senora Dr.-Ing. Dr. rer. nat. Dr. h.c. mult. Sibille van de Güllefass-Stinkstiefel"
            };

            // Parse name and compare to expected name
            using (var parser = new NameParser())
            {
                Name parsedName = parser.ParseName("Senora Dr.-Ing. Dr. rer. nat. Dr. h.c. mult. Sibille Isolde van de Güllefass-Stinkstiefel");
                Assert.AreEqual(expectedName, parsedName);
            }
        }

        /// <summary>
        /// Test a german male name
        /// </summary>
        [TestMethod]
        public void ValidGermanMaleName()
        {
            // Define expected name
            Name expectedName = new Name
            {
                Gender = "maennlich",
                LastName = "van de Güllefass-Stinkstiefel",
                FirstName = "Eckart Carsten",
                MiddleName = "Carsten",
                Salutation = "Herr",
                Title = "keine Angabe",
                Greeting = "Sehr geehrter Herr Eckart van de Güllefass-Stinkstiefel"
            };

            // Parse name and compare to expected name
            using (var parser = new NameParser())
            {
                Name parsedName = parser.ParseName("Herr Eckart Carsten van de Güllefass-Stinkstiefel");
                Assert.AreEqual(expectedName, parsedName);
            }
        }

        /// <summary>
        /// Test a german name with an academic title
        /// </summary>
        [TestMethod]
        public void ValidGermanNameWithTitle()
        {
            // Define expected name
            Name expectedName = new Name
            {
                Gender = "maennlich",
                LastName = "van de Güllefass-Stinkstiefel",
                FirstName = "Eckart Carsten",
                MiddleName = "Carsten",
                Salutation = "Herr",
                Title = "Prof. Dr. rer. nat.",
                Greeting = "Sehr geehrter Herr Prof. Dr. rer. nat. Eckart van de Güllefass-Stinkstiefel"
            };

            // Parse name and compare to expected name
            using (var parser = new NameParser())
            {
                Name parsedName = parser.ParseName("Herr Prof. Dr. rer. nat. van de Güllefass-Stinkstiefel, Eckart Carsten");
                Assert.AreEqual(expectedName, parsedName);
            }
        }

        //[TestMethod]
        //public void ValidGreetingMale()
        //{
        //    string expectedGreetingMale = "Sehr geehrter Herr Prof. Sandro Freiherr vom Wald";
        //    string greeetingMale = NameParser.GetGreeting("Freiherr vom Wald", "Sandro", "Herr", "Prof.");

        //    Assert.AreEqual(expectedGreetingMale, greeetingMale);
        //}

        //[TestMethod]
        //public void ValidGreetingFemale()
        //{
        //    string expectedGreetingFemale = "Sehr geehrte Frau Dr. Willma Freiherr vom Wald";
        //    string greeetingFemale = NameParser.GetGreeting("Freiherr vom Wald", "Willma", "Frau", "Dr.");

        //    Assert.AreEqual(expectedGreetingFemale, greeetingFemale);
        //}

        //[TestMethod]
        //public void ValidGreeting()
        //{
        //    //achtung 2 leerzeichen
        //    string expectedGreeting = "Guten Tag  Dr. Willma Freiherr vom Wald";
        //    string greeeting = NameParser.GetGreeting("Freiherr vom Wald", "Willma", "", "Dr.");

        //    Assert.AreEqual(expectedGreeting, greeeting);
        //}

        //[TestMethod]
        //public void ValidMiddleName()
        //{
        //    List<string> elements = new List<string>();
        //    elements.Add("Andreas");
        //    elements.Add("Simon");

        //    string expectedFirstName = "Andreas Simon";

        //    string firstName = NameParser.GetMiddleName(elements);

        //    Assert.AreEqual(firstName, expectedFirstName);
        //}

        //[TestMethod]
        //public void ValidNoMiddleName()
        //{
        //    List<string> elements = new List<string>();


        //    string expectedFirstName = "keine Angabe";

        //    string firstName = NameParser.GetMiddleName(elements);

        //    Assert.AreEqual(firstName, expectedFirstName);
        //}

        //[TestMethod]
        //public void ValidFirstName()
        //{
        //    List<string> elements = new List<string>();            
        //    elements.Add("Andreas");
        //    elements.Add("Simon");

        //    string expectedFirstName = "Andreas";

        //    string firstName = NameParser.GetFirstName(elements);

        //    Assert.AreEqual(firstName, expectedFirstName);
        //}

        //[TestMethod]
        //public void ValidNoTitle()
        //{
        //    List<string> elements = new List<string>();            
        //    elements.Add("Simon");

        //    string expectedTitle = "keine Angabe";

        //    string title = NameParser.GetTitle(elements);

        //    Assert.AreEqual(title, expectedTitle);
        //}

        //[TestMethod]
        //public void ValidTitle()
        //{
        //    List<string> elements = new List<string>();
        //    elements.Add("Dr.-Ing.");
        //    elements.Add("Dr.");
        //    elements.Add("rer.");
        //    elements.Add("nat.");
        //    elements.Add("Dr.");
        //    elements.Add("h.c.");
        //    elements.Add("mult.");
        //    elements.Add("Simon");

        //    string expectedTitle = "Dr.-Ing. Dr. rer. nat. Dr. h.c. mult.";

        //    string title = NameParser.GetTitle(elements);

        //    Assert.AreEqual(title, expectedTitle);
        //}

        //[TestMethod]
        //public void ValidSalutationMale()
        //{      

        //    List<string> elements = new List<string>();
        //    elements.Add("Herr");
        //    elements.Add("Dr.");

        //    string expectedSalutationMale = "Herr";
        //    string salutationMale = NameParser.GetSalutation(elements);

        //    Assert.AreEqual(salutationMale, expectedSalutationMale);
        //    //Assert.AreEqual(salutationMale[1], expectedList);

        //}

        //[TestMethod]
        //public void ValidSalutationFemale()
        //{
        //    List<string> elements = new List<string>();
        //    elements.Add("Ms");
        //    elements.Add("Dr.");

        //    string expectedSalutationFemale = "Ms";
        //    string salutationFemale = NameParser.GetSalutation(elements);


        //    Assert.AreEqual(salutationFemale, expectedSalutationFemale);
        //}

        //[TestMethod]
        //public void ValidSalutation()
        //{
        //    List<string> elements = new List<string>();
        //    elements.Add("Prof.");
        //    elements.Add("Dr.");

        //    string expectedSalutation = "keine Angabe";
        //    string salutation = NameParser.GetSalutation(elements);


        //    Assert.AreEqual(salutation, expectedSalutation);
        //}

        //[TestMethod]
        //public void ValidGenderMale()
        //{
        //    string expectedGenderMale = "maennlich";
        //    string genderMale = NameParser.GetGender("Herr");

        //    Assert.AreEqual(expectedGenderMale, genderMale);

        //}

        //[TestMethod]
        //public void ValidGenderFemale()
        //{
        //    string expectedGenderFemale = "weiblich";
        //    string genderFemale = NameParser.GetGender("Frau");


        //    Assert.AreEqual(expectedGenderFemale, genderFemale);

        //}

        //[TestMethod]
        //public void ValidGender()
        //{

        //    string expectedGenderDiverse = "keine Angabe";
        //    string genderDiverse = NameParser.GetGender("Weder Herr noch Frau in der Anrede");

        //    Assert.AreEqual(expectedGenderDiverse, genderDiverse);
        //}

        //[TestMethod]
        //public void ValidNoblename()
        //{

        //    List<string> elements = new List<string>();
        //    elements.Add("Ms.");
        //    elements.Add("Dr.");
        //    elements.Add("Max");
        //    elements.Add("van");
        //    elements.Add("de");
        //    elements.Add("Hasenfratz-Schreier");

        //    string nobleName = NameParser.GetNobleName(elements);

        //    string expectedNobleName = "van de Hasenfratz-Schreier";

        //    Assert.AreEqual(expectedNobleName, nobleName);

        //}

        //[TestMethod]
        //public void ValidSingleLastName()
        //{
        //    List<string> elements = new List<string>();
        //    elements.Add("Mr.");
        //    elements.Add("Dr.");
        //    elements.Add("Max");        
        //    elements.Add("Rosenbusch");


        //    string nobleName = NameParser.GetNobleName(elements);

        //    string expectedNobleName = "Rosenbusch";

        //    Assert.AreEqual(expectedNobleName, nobleName);

        //}

    }
}
