using ContactParser.App.Models;
using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactParser.Tests.NameParserTests
{
    [TestClass]
    public class TestCommaSeparated
    {
        #region Test Methods
        /// <summary>
        /// Test a non comma separated name
        /// </summary>
        [TestMethod]
        public void NonCommaSeparated()
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
        /// Test a comma separated name
        /// </summary>
        [TestMethod]
        public void CommaSeparated()
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
