using System;
using ContactParser.App.Models;
using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactParser.Tests.NameParserTests
{
    [TestClass]
    public class TestInvalidNames
    {
        /// <summary>
        /// Test a name without first name
        /// </summary>
        [TestMethod]
        public void FirstNameMissing()
        {
            // Check if the format exception is thrown
            Assert.ThrowsException<FormatException>(() =>
            {
                // Parse name and compare to expected name
                using (var parser = new NameParser())
                {
                    Name parsedName = parser.ParseName("Herr Mustermann");
                }
            });
        }

        /// <summary>
        /// Test a name with invalid characters
        /// </summary>
        [TestMethod]
        public void InvalidChracter()
        {
            // Check if the format exception is thrown
            Assert.ThrowsException<ArgumentException>(() =>
            {
                // Parse name and compare to expected name
                using (var parser = new NameParser())
                {
                    Name parsedName = parser.ParseName("Herr Mustermann2");
                }
            });
        }
    }
}
