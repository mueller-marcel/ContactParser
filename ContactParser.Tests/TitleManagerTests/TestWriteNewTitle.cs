using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using ContactParser.App.Models;
using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactParser.Tests.TitleManagerTests
{
    [TestClass]
    public class TestWriteNewTitle
    {
        #region Test Methods
        /// <summary>
        /// Try to add new title when there is no existing file yet and the title already exists
        /// </summary>
        [TestMethod]
        public void WriteExistingTitleFromAssembly()
        {
            // Declarations
            string expectedContent, actualContent;

            // Get file path of the titles.json file and delete if it exists
            Uri location = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            string filePath = Path.Combine(location.AbsolutePath, "titles.json");

            // Delete the file if its existing
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

            // Prepare expected object
            TitleDTO titles = JsonSerializer.Deserialize<TitleDTO>(expectedContent);
            expectedContent = JsonSerializer.Serialize(titles);

            // Get the actual content after adding "nat."
            using (var titleManager = new TitleManager())
            {
                titleManager.WriteNewTitle("Dr.");
                actualContent = File.ReadAllText(filePath);
            }

            Assert.AreEqual(expectedContent, actualContent);
        }

        /// <summary>
        /// Add new title when there is no existing file yet
        /// </summary>
        [TestMethod]
        public void WriteNewTitleFromAssembly()
        {
            // Declarations
            string expectedContent, actualContent;

            // Get file path of the titles.json file and delete if it exists
            Uri location = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            string filePath = Path.Combine(location.AbsolutePath, "titles.json");

            // Delete the file if its existing
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

            // Prepare expected object
            TitleDTO titles = JsonSerializer.Deserialize<TitleDTO>(expectedContent);
            titles.Title.Add("Eng.");
            expectedContent = JsonSerializer.Serialize(titles);

            // Get the actual content after adding "nat."
            using (var titleManager = new TitleManager())
            {
                titleManager.WriteNewTitle("Eng.");
                actualContent = File.ReadAllText(filePath);
            }

            Assert.AreEqual(expectedContent, actualContent);
        }

        /// <summary>
        /// Try to add new title into an existing file and the title exists already
        /// </summary>
        [TestMethod]
        public void WriteExistingTitleFromFile()
        {
            // Declarations
            string expectedContent, actualContent;

            // Get ressources of the assembly
            Assembly assembly = Assembly.GetAssembly(typeof(NameParser));
            Stream resourceStream = assembly.GetManifestResourceStream("ContactParser.App.Data.Data.json");

            // Get file path of the titles.json file and delete if it exists
            Uri location = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            string filePath = Path.Combine(location.AbsolutePath, "titles.json");

            using (var StreamReader = new StreamReader(resourceStream))
            {
                expectedContent = StreamReader.ReadToEnd();
            }

            // Build expected object for comparison
            TitleDTO expectedTitles = JsonSerializer.Deserialize<TitleDTO>(expectedContent);
            expectedContent = JsonSerializer.Serialize(expectedTitles);

            using (var titleManager = new TitleManager())
            {
                titleManager.WriteNewTitle("Dr.");
            }

            // Read the content from the file
            actualContent = File.ReadAllText(filePath);

            Assert.AreEqual(expectedContent, actualContent);
        }

        /// <summary>
        /// Add new title into an existing file
        /// </summary>
        [TestMethod]
        public void WriteNewTitleFromFile()
        {
            // Declarations
            string expectedContent, actualContent;

            // Get ressources of the assembly
            Assembly assembly = Assembly.GetAssembly(typeof(NameParser));
            Stream resourceStream = assembly.GetManifestResourceStream("ContactParser.App.Data.Data.json");

            // Get file path of the titles.json file and delete if it exists
            Uri location = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            string filePath = Path.Combine(location.AbsolutePath, "titles.json");

            using (var StreamReader = new StreamReader(resourceStream))
            {
                expectedContent = StreamReader.ReadToEnd();
            }

            // Build expected object for comparison
            TitleDTO expectedTitles = JsonSerializer.Deserialize<TitleDTO>(expectedContent);
            expectedTitles.Title.Add("Sc.");
            expectedContent = JsonSerializer.Serialize(expectedTitles);

            using (var titleManager = new TitleManager())
            {
                titleManager.WriteNewTitle("Sc.");
            }

            // Read the content from the file
            actualContent = File.ReadAllText(filePath);

            Assert.AreEqual(expectedContent, actualContent);
        }
        #endregion

        #region Setup and Cleanup Methods
        /// <summary>
        /// Initializes the file for <see cref="WriteNewTitleFromFile"/>
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
