using ContactParser.App.Models;
using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactParser.Tests.NameParserTests
{
    [TestClass]
    public class TestNobleNames
    {
        /// <summary>
        /// Test a german name without a noble name
        /// </summary>
        [TestMethod]
        public void ValidNameWithoutNobleName()
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

        /// <summary>
        /// Test a german male name with one noble name
        /// </summary>
        [TestMethod]
        public void ValidNameWithNobleName()
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
                Name parsedName = parser.ParseName("Herr Prof. Eckart Carsten van de Güllefass-Stinkstiefel");
                Assert.AreEqual(expectedName, parsedName);
            }
        }

        /// <summary>
        /// Test a german male name with multiple noble names
        /// </summary>
        [TestMethod]
        public void ValidNameWithNobleNames()
        {
            // Define expected name
            Name expectedName = new Name
            {
                Gender = "maennlich",
                LastName = "van de Güllefass-Stinkstiefel",
                FirstName = "Eckart Carsten",
                MiddleName = "Carsten",
                Salutation = "Herr",
                Title = "Prof. Dr.",
                Greeting = "Sehr geehrter Herr Prof. Dr. Eckart van de Güllefass-Stinkstiefel"
            };

            // Parse name and compare to expected name
            using (var parser = new NameParser())
            {
                Name parsedName = parser.ParseName("Herr Prof. Dr. Eckart Carsten van de Güllefass-Stinkstiefel");
                Assert.AreEqual(expectedName, parsedName);
            }
        }
    }
}
