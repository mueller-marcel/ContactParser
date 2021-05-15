using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using ContactParser.App.Models;

namespace ContactParser.App.Services
{
    public class TitleManager : IDisposable
    {
        /// <summary>
        /// Get the content either from titles.json on the desktop or the default text from the assembly
        /// </summary>
        /// <returns>The text from titles.json or the assembly</returns>
        /// <exception cref="IOException">Thrown, when reading from the file or the assembly goes wrong</exception>
        public string GetContent()
        {
            // Initializations
            string filePath, content;

            // Try to read the titles.json (Create it if not done so far)
            try
            {
                filePath = GetOrCreateFile();
                content = File.ReadAllText(filePath);
            }
            catch (Exception)
            {
                throw new IOException("Error while creating and reading from titles.json");
            }

            // Read from the assembly
            if (content.Equals(string.Empty))
            {
                try
                {
                    content = GetTextFromAssembly();
                }
                catch (Exception)
                {
                    throw new IOException("Error while reading from the assembly");
                }
                return content;
            }
            else
            {
                try
                {
                    content = File.ReadAllText(filePath);
                    return content;
                }
                catch (Exception)
                {
                    throw new IOException("Error while reading the titles.json");
                }
            }
        }

        /// <summary>
        /// Creates an empty file if there is no titles file on the desktop
        /// </summary>
        /// <exception cref="ArgumentException">Thrown, when the arguments for the path are wrong</exception>
        /// <exception cref="PlatformNotSupportedException">Thrown, when the desktop and folder is not available due to OS incompatibilities</exception>
        /// <exception cref="Exception">Thrown, when anything else gone wrong</exception>
        public string GetOrCreateFile()
        {
            try
            {
                Uri location = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                string filePath = Path.Combine(location.AbsolutePath, "titles.json");

                if (File.Exists(filePath))
                {
                    return filePath;
                }

                // Create empty file and dispose
                File.Create(filePath).Dispose();
                return filePath;
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (PlatformNotSupportedException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retrieves the json text in the assembly
        /// </summary>
        /// <returns>The text from the assembly as text</returns>
        /// <exception cref="FileLoadException">Thrown when an <see cref="IOException"/> was wrong by the GetManifestResourceStream Method</exception>
        /// <exception cref="BadImageFormatException">Thrown, when the name is not a valid assembly</exception>
        /// <exception cref="ArgumentException">Thrown, when the argument for the <see cref="StreamReader"/> was null or wrong</exception>
        /// <exception cref="OutOfMemoryException">Thrown, when ran out of RAM when reading the titles.json stream</exception>
        /// <exception cref="IOException">Thrown, when an IO error occurs while reading the <see cref="Stream"/></exception>
        private string GetTextFromAssembly()
        {
            Stream titleStream;
            try
            {
                titleStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ContactParser.App.Data.Data.json");
            }
            catch (FileLoadException e)
            {
                throw e;
            }
            catch (BadImageFormatException e)
            {
                throw e;
            }

            try
            {
                using (var streamReader = new StreamReader(titleStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (OutOfMemoryException e)
            {
                throw e;
            }
            catch (IOException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Writes a new title to the json file on the desktop
        /// </summary>
        /// <param name="title">The title to write</param>
        /// <exception cref="IOException">Thrown, when reading or writing to the file failed</exception>
        /// <exception cref="ArgumentException">Thrown, when an argument was wrong</exception>
        /// <exception cref="JsonException">Thrown, when the JSON could not be serialized</exception>
        /// <exception cref="Exception">Thrown, when another fatal error occured</exception>
        public void WriteNewTitle(string title)
        {
            // Declarations
            string filePath, content;

            // Split input and get file
            var titleElements = title.Split(" ");

            try
            {
                // Get or create the file and return the path
                filePath = GetOrCreateFile();

                // Read all from file
                content = File.ReadAllText(filePath);
            }
            catch (Exception)
            {
                throw new IOException("Error while reading from titles.json");
            }

            // Check the content whether its empty or filled
            if (string.IsNullOrEmpty(content))
            {
                try
                {
                    // Read from the assembly
                    string ressourceContent = GetTextFromAssembly();
                    TitleDTO titles = JsonSerializer.Deserialize<TitleDTO>(ressourceContent);
                    foreach (var item in titleElements)
                    {
                        if (!titles.Title.Contains(item))
                        {
                            titles.Title.Add(item);
                        }
                    }
                    File.WriteAllText(filePath, JsonSerializer.Serialize(titles));
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("An argument was wrong");
                }
                catch (JsonException)
                {
                    throw new JsonException("JSON could not be serialized");
                }
                catch (Exception)
                {
                    throw new Exception("An fatal error occured");
                }
            }
            else
            {
                try
                {
                    // Add new title when there is no duplicate
                    TitleDTO titles = JsonSerializer.Deserialize<TitleDTO>(content);
                    foreach (var item in titleElements)
                    {
                        if (!titles.Title.Contains(item))
                        {
                            titles.Title.Add(item);
                        }
                    }
                    File.WriteAllText(filePath, JsonSerializer.Serialize(titles));
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("An argument was wrong");
                }
                catch (JsonException)
                {
                    throw new JsonException("JSON could not be serialized");
                }
                catch (Exception)
                {
                    throw new Exception("An fatal error occured");
                }
            }
        }

        /// <summary>
        /// Implementation of <see cref="IDisposable"/>
        /// </summary>
        public void Dispose() { }
    }
}
