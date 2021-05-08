using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactParser.Tests
{
    [TestClass]
    public class NameParserTests
    {
        [TestMethod]
        public void ValidGenderMale()
        {
            string expectedGenderMale = "male";
            string genderMale = NameParser.GetGender("Herr");

            Assert.AreEqual(expectedGenderMale, genderMale);

        }

        [TestMethod]
        public void ValidGenderFemale()
        {
            string expectedGenderFemale = "female";
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

        [TestMethod]
        public void ValidGreeting()
        {

        }
    }
}
