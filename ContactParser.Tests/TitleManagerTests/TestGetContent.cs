using System;
using System.IO;
using System.Reflection;
using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactParser.Tests.TitleManagerTests
{
    [TestClass]
    public class TestGetContent
    {
        #region Test Methods
        /// <summary>
        /// Test the retrieval of the content from the assembly
        /// </summary>
        [TestMethod]
        public void GetContentFromAssembly()
        {
            // Declarations
            string expectedContent, actualContent;

            // Get file path of the titles.json file and delete if it exists
            Uri location = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            string filePath = Path.Combine(location.AbsolutePath, "titles.json");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Get ressources of the assembly
            Assembly assembly = Assembly.GetAssembly(typeof(NameParser));
            Stream resourceStream = assembly.GetManifestResourceStream("ContactParser.App.Data.Data.json");

            using (var StreamReader = new StreamReader(resourceStream))
            {
                expectedContent = StreamReader.ReadToEnd();
            }

            using (var titleManager = new TitleManager())
            {
                actualContent = titleManager.GetContent();
            }

            Assert.AreEqual(expectedContent, actualContent);
        }

        /// <summary>
        /// Test the retrieval of the content from the existing file
        /// </summary>
        [TestMethod]
        public void GetContentFromFile()
        {
            // Declarations
            string expectedContent, actualContent;

            // Get ressources of the assembly
            Assembly assembly = Assembly.GetAssembly(typeof(NameParser));
            Stream resourceStream = assembly.GetManifestResourceStream("ContactParser.App.Data.Data.json");

            using (var StreamReader = new StreamReader(resourceStream))
            {
                expectedContent = StreamReader.ReadToEnd();
            }

            // Get file path of the titles.json file and delete if it exists
            Uri location = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            string filePath = Path.Combine(location.AbsolutePath, "titles.json");

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
                File.WriteAllText(filePath, expectedContent);
            }

            // Read the file using the titleManager
            using (var titleManager = new TitleManager())
            {
                actualContent = titleManager.GetContent();
            }

            Assert.AreEqual(expectedContent, actualContent);
        }
        #endregion

        #region Setup and Cleanup Methods
        /// <summary>
        /// Initializes the file for <see cref="GetContentFromFile"/>
        /// </summary>
        [TestInitialize]
        public void CreateFile()
        {
            // Get file path of the titles.json file and delete if it exists
            Uri location = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            string filePath = Path.Combine(location.AbsolutePath, "titles.json");

            // Get ressources of the assembly
            Assembly assembly = Assembly.GetAssembly(typeof(NameParser));
            Stream resourceStream = assembly.GetManifestResourceStream("ContactParser.App.Data.Data.json");

            using (var streamReader = new StreamReader(resourceStream))
            {
                File.WriteAllText(filePath, streamReader.ReadToEnd());
            }
        }

        /// <summary>
        /// Deletes the file that was created
        /// </summary>
        [TestCleanup]
        public void DeleteFile()
        {
            // Get file path of the titles.json file and delete if it exists
            Uri location = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            string filePath = Path.Combine(location.AbsolutePath, "titles.json");

            // Delete the file
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        #endregion
    }
}
