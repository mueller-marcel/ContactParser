using ContactParser.App.Models;
using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactParser.Tests.NameParserTests
{
    [TestClass]
    public class TestMiddleName
    {
        #region Test Methods
        /// <summary>
        /// Test a german male name without middle name
        /// </summary>
        [TestMethod]
        public void ValidNameWithoutMiddleName()
        {
            // Define expected name
            Name expectedName = new Name
            {
                Gender = "maennlich",
                LastName = "van de Güllefass-Stinkstiefel",
                FirstName = "Eckart",
                MiddleName = "",
                Salutation = "Herr",
                Title = "keine Angabe",
                Greeting = "Sehr geehrter Herr Eckart van de Güllefass-Stinkstiefel"
            };

            // Parse name and compare to expected name
            using (var parser = new NameParser())
            {
                Name parsedName = parser.ParseName("Herr Eckart van de Güllefass-Stinkstiefel");
                Assert.AreEqual(expectedName, parsedName);
            }
        }

        /// <summary>
        /// Test a german male name with one middle name
        /// </summary>
        [TestMethod]
        public void ValidNameWithMiddleName()
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
        /// Test a german male name with multiple middle names
        /// </summary>
        [TestMethod]
        public void ValidNameWithMiddleNames()
        {
            // Define expected name
            Name expectedName = new Name
            {
                Gender = "maennlich",
                LastName = "van de Güllefass-Stinkstiefel",
                FirstName = "Eckart Josua Nico Sven Marcel",
                MiddleName = "Carsten Josua Nico Sven Marcel",
                Salutation = "Herr",
                Title = "keine Angabe",
                Greeting = "Sehr geehrter Herr Eckart van de Güllefass-Stinkstiefel"
            };

            // Parse name and compare to expected name
            using (var parser = new NameParser())
            {
                Name parsedName = parser.ParseName("Herr Eckart Carsten Josua Nico Sven Marcel van de Güllefass-Stinkstiefel");
                Assert.AreEqual(expectedName, parsedName);
            }
        }
        #endregion
    }
}
