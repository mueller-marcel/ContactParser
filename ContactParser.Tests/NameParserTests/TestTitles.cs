using ContactParser.App.Models;
using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactParser.Tests.NameParserTests
{
    [TestClass]
    public class TestTitles
    {
        #region Test Methods
        /// <summary>
        /// Test a german name without an academic title
        /// </summary>
        [TestMethod]
        public void ValidGermanNameWithoutTitle()
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
                Name parsedName = parser.ParseName("Herr van de Güllefass-Stinkstiefel, Eckart Carsten");
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
                Title = "Prof.",
                Greeting = "Sehr geehrter Herr Prof. Eckart van de Güllefass-Stinkstiefel"
            };

            // Parse name and compare to expected name
            using (var parser = new NameParser())
            {
                Name parsedName = parser.ParseName("Herr Prof. van de Güllefass-Stinkstiefel, Eckart Carsten");
                Assert.AreEqual(expectedName, parsedName);
            }
        }

        /// <summary>
        /// Test a german name with multiple academic titles
        /// </summary>
        [TestMethod]
        public void ValidGermanNameWithTitles()
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
        #endregion
    }
}
