using System;
using System.IO;
using System.Reflection;
using ContactParser.App.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContactParser.Tests.TitleManagerTests
{
    [TestClass]
    public class TestGetOrCreateFile
    {
        /// <summary>
        /// Tests the creation of a new file
        /// </summary>
        [TestMethod]
        public void CreateFileTest()
        {
            // Declarations
            string actualFilePath;

            // Get file path of the titles.json file and delete if it exists
            Uri location = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            string filePath = Path.Combine(location.AbsolutePath, "titles.json");

            // Delete file as its necessary for this test
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Create file and return file path
            using (var titleManager = new TitleManager())
            {
                actualFilePath = titleManager.GetOrCreateFile();
            }

            Assert.AreEqual(filePath, actualFilePath);
        }

        /// <summary>
        /// Tests the return of the path of an existing file
        /// </summary>
        [TestMethod]
        public void ReturnExistingFilePath()
        {
            // Declarations
            string actualFilePath;

            // Get file path of the titles.json file and delete if it exists
            Uri location = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            string filePath = Path.Combine(location.AbsolutePath, "titles.json");

            // Create file and return file path
            using (var titleManager = new TitleManager())
            {
                actualFilePath = titleManager.GetOrCreateFile();
            }

            Assert.AreEqual(filePath, actualFilePath);
        }

        /// <summary>
        /// Initializes the file for <see cref="ReturnExistingFilePath"/>
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
    }
}
