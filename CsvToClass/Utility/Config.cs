using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvToClass.Utility
{
    /// <summary>
    /// Gets values from the app.config file
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Gets the default csv file from the config appSettings
        /// </summary>
        /// <returns>string - the file path</returns>
        public static string GetDefaultInputFile()
        {
            string defaultOutputDirectory = null;
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            for (int i = 0; i < appSettings.Count; i++)
            {
                if (appSettings.GetKey(i) == "defaultCsvFile")
                {
                    defaultOutputDirectory = appSettings[i];
                }
            }
            return defaultOutputDirectory;
        }

        /// <summary>
        /// Gets the default download directory from the config appSettings
        /// </summary>
        /// <returns>string - the directory path</returns>
        public static string GetDefaultOutputFile()
        {
            string defaultOutputDirectory = null;
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            for (int i = 0; i < appSettings.Count; i++)
            {
                if (appSettings.GetKey(i) == "defaultOutputFile")
                {
                    defaultOutputDirectory = appSettings[i];
                }
            }
            return defaultOutputDirectory;
        }

        public static string GetDefaultNamespace()
        {
            string defaultNameSpace = null;
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            for (int i = 0; i < appSettings.Count; i++)
            {
                if (appSettings.GetKey(i) == "defaultNamespace")
                {
                    defaultNameSpace = appSettings[i];
                }
            }
            return defaultNameSpace;
        }
    }
}
    