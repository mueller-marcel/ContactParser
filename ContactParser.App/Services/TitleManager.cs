using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ContactParser.App.Services
{
    public class TitleManager : IDisposable
    {
        /// <summary>
        /// Holds the path where the ressource file shall be outsourced
        /// </summary>
        public string TitlePath { get; set; }

        /// <summary>
        /// Holds the content of the ressource content of Data.json
        /// </summary>
        public string TitleContent { get; set; }

        /// <summary>
        /// Creates a new file with the json as content in the same directory as the executing assembly
        /// </summary>
        /// <exception cref="Exception">Thrown, when a file could not be read or written</exception>
        public void CreateTitleFile()
        {
            Stream titleStream;

            // Get the path and the content as stream
            var location = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            TitlePath = Path.Combine(location.AbsolutePath, "titles.json");
            try
            {
                titleStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ContactParser.App.Data.Data.json");
            }
            catch (Exception)
            {
                throw;
            }

            using (StreamReader streamReader = new StreamReader(titleStream))
            {
                try
                {
                    TitleContent = streamReader.ReadToEnd();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            // Write content to new file in the same directory as the executing assembly
            try
            {
                using (StreamWriter streamWriter = File.CreateText(TitlePath))
                {
                    streamWriter.WriteLine(TitleContent);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Release the managed properties
        /// </summary>
        public void Dispose()
        {
            TitleContent = null;
            TitlePath = null;
        }
    }
}
