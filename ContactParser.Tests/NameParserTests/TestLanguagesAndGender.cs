using ContactParser.App.Models;
using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactParser.Tests.NameParserTests
{
    [TestClass]
    public class TestLanguagesAndGender
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
        /// Test a german name without gender
        /// </summary>
        [TestMethod]
        public void ValidNameWithoutSex()
        {
            // Define expected name
            Name expectedName = new Name
            {
                Gender = "keine Angabe",
                LastName = "van Güllefass-Stinkstiefel",
                FirstName = "Eckart Carsten",
                MiddleName = "Carsten",
                Salutation = "keine Angabe",
                Title = "keine Angabe",
                Greeting = "Guten Tag Eckart van Güllefass-Stinkstiefel"
            };

            // Parse name and compare to expected name
            using (var parser = new NameParser())
            {
                Name parsedName = parser.ParseName("Eckart Carsten van Güllefass-Stinkstiefel");
                Assert.AreEqual(expectedName, parsedName);
            }
        }
    }
}
