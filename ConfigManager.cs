using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftKvmSwitch.Configuration;
using System.IO;
using System.Reflection;

namespace SoftKvmSwitch
{
    internal class ConfigurationManager
    {
        /// <summary>
        /// This Manager is expecting a Config.json inside the executable directory.
        /// The app configuration will be read from this file.
        /// </summary>
        /// <returns>An object containing the configuration data loaded from the config file.</returns>
        internal static AppConfig LoadConfig()
        {
            AppConfig result;

            try
            {
                var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Config.json");
                var fileText = File.ReadAllText(filePath);

                result = JsonConvert.DeserializeObject<AppConfig>(fileText)
                    ?? throw new NullReferenceException("Deserialization returned null.");
            }
            catch (Exception err)
            {
                throw new Exception("Could not load configuration from file.", err);
            }

            return result;
        }
    }
}
