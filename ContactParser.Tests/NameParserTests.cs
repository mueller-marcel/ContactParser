using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactParser.Tests
{
    [TestClass]
    public class NameParserTests
    {
        [TestMethod]
        public void ValidSalutationMale()
        {
            string expectedSalutationMale = "Herr";
            string salutationMale = NameParser.GetSalutation("Herr");

            Assert.AreEqual(salutationMale, expectedSalutationMale);
        }

        [TestMethod]
        public void ValidSalutationFemale()
        {
            string expectedSalutationFemale = "Ms.";
            string salutationFemale = NameParser.GetSalutation("Ms.");

            Assert.AreEqual(salutationFemale, expectedSalutationFemale);
        }

        [TestMethod]
        public void ValidSalutation()
        {
            string expectedSalutation = "keine Angabe";
            string salutation = NameParser.GetSalutation("Prof.");

            Assert.AreEqual(salutation, expectedSalutation);
        }

        [TestMethod]
        public void ValidGreetingMale()
        {
            string expectedGreetingMale = "Sehr geehrter Herr Prof. Sandro Freiherr vom Wald";
            string greeetingMale = NameParser.GetGreeting("Freiherr vom Wald", "Sandro", "Herr", "Prof.");

            Assert.AreEqual(expectedGreetingMale, greeetingMale);
        }

        [TestMethod]
        public void ValidGreetingFemale()
        {
            string expectedGreetingFemale = "Sehr geehrte Frau Dr. Willma Freiherr vom Wald";
            string greeetingFemale = NameParser.GetGreeting("Freiherr vom Wald", "Willma", "Frau", "Dr.");

            Assert.AreEqual(expectedGreetingFemale, greeetingFemale);
        }

        [TestMethod]
        public void ValidGreeting()
        {
            string expectedGreeting = "Guten Tag Dr. Willma Freiherr vom Wald";
            string greeeting = NameParser.GetGreeting("Freiherr vom Wald", "Willma", "", "Dr.");

            Assert.AreEqual(expectedGreeting, greeeting);
        }

        [TestMethod]
        public void ValidGenderMale()
        {
            string expectedGenderMale = "männlich";
            string genderMale = NameParser.GetGender("Herr");

            Assert.AreEqual(expectedGenderMale, genderMale);

        }

        [TestMethod]
        public void ValidGenderFemale()
        {
            string expectedGenderFemale = "weiblich";
            string genderFemale = NameParser.GetGender("Frau");


            Assert.AreEqual(expectedGenderFemale, genderFemale);

        }

        [TestMethod]
        public void ValidGender()
        {
            string expectedGenderDiverse = "keine Angabe";
            string genderDiverse = NameParser.GetGender("Weder Herr noch Frau in der Anrede");

            Assert.AreEqual(expectedGenderDiverse, genderDiverse);
        }

        [TestMethod]
        public void ValidNoblename()
        {
            string[] adresselements = { "Herr", "Dr.", "Gerold", "van", "de", "Hasenfratz-Schreier" };
            string nobleName = NameParser.GetNobleName(adresselements);

            string expectedNobleName = "van de Hasenfratz-Schreier";

            Assert.AreEqual(expectedNobleName, nobleName);

        }

        [TestMethod]
        public void ValidSingleLastName()
        {
            string[] adresselements = { "Herr", "Dr.", "Isolde", "Rosenbusch" };

            string nobleName = NameParser.GetNobleName(adresselements);

            string expectedNobleName = "Rosenbusch";

            Assert.AreEqual(expectedNobleName, nobleName);

        }
       
    }
}
