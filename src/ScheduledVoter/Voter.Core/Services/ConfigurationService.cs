using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Voter.Core.Models;

namespace Voter.Core.Services
{
    public class ConfigurationService
    {
        private const string DEFAULT_SETTINGS_FILE = "settings.json";
        public Configuration Configuration { get; private set; }

        public bool Load()
        {
            string path = Path.Combine("resources", DEFAULT_SETTINGS_FILE);
            if (File.Exists(path))
            {
                string configuration = File.ReadAllText(path);
                Configuration = JsonConvert.DeserializeObject<Configuration>(configuration);

                return true;
            }

            return false;
        }
    }
}
