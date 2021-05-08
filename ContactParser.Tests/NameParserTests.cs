using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactParser.Tests
{
    [TestClass]
    public class NameParserTests
    {
        [TestMethod]
        public void ValidGender()
        {
            string expectedGenderMale = "male";
            string genderMale = NameParser.GetGender("Herr");

            string expectedGenderFemale = "female";
            string genderFemale = NameParser.GetGender("Frau");

            string expectedGenderDiverse = "diverse";
            string genderDiverse = NameParser.GetGender("Sonstiges");

            Assert.AreEqual(expectedGenderMale, genderMale);
            Assert.AreEqual(expectedGenderFemale, genderFemale);
            Assert.AreEqual(expectedGenderDiverse, genderDiverse);
        }

        [TestMethod]
        public void ValidNoblename()
        {
            string[] adresselements = { "Herr", "Dr.", "Heinz", "van", "de", "Hasenfratz-Schreier" };
            string nobleName = NameParser.GetNobleName(adresselements);

            string expectedNobleName = "van de Hasenfratz-Schreier ";

            Assert.AreEqual(expectedNobleName, nobleName);

        }

        [TestMethod]
        public void ValidGreeting()
        {

        }
    }
}
