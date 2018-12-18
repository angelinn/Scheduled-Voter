using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Voter.Core.Services
{
    public class ConfigurationService
    {
        private const string DEFAULT_SETTINGS_FILE = "settings.json";
        private JObject configuration;

        public bool Load()
        {
            string path = Path.Combine("resources", DEFAULT_SETTINGS_FILE);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                configuration = JObject.Parse(json);

                return true;
            }

            return false;
        }

        public string GetValue(string key)
        {
            return (string)configuration[key];
        }

        public List<string> GetArray(string key)
        {
            return (configuration[key] as JArray).Select(v => (string)v).ToList();
        }

        public string this[string key] => GetValue(key);
    }
}
