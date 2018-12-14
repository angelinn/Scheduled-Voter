using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Voter.ViewModels.Models;

namespace Voter.ViewModels.Services
{
    public class ConfigurationService
    {
        private const string DEFAULT_SETTINGS_FILE = "settings.json";
        public Configuration Configuration { get; private set; }

        public bool Load()
        {
            if (File.Exists(DEFAULT_SETTINGS_FILE))
            {
                string configuration = File.ReadAllText(DEFAULT_SETTINGS_FILE);
                Configuration = JsonConvert.DeserializeObject<Configuration>(configuration);

                return true;
            }

            return false;
        }
    }
}
