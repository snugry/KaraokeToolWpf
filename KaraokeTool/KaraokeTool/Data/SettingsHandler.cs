using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.IO;

namespace KaraokeTool.Data
{
    internal class SettingsHandler
    {
        private const string settingsPath = "settings.json";

        private Settings _settings;

        public SettingsHandler()
        {
            ReadSettings();
        }

        public void ReadSettings()
        {
            if (File.Exists(settingsPath))
            {
                try
                {
                    string content = File.ReadAllText(settingsPath);

                    _settings = JsonSerializer.Deserialize<Settings>(content);
                }
                catch (Exception ex)
                {
                    _settings = new Settings();
                } 
            }
            else
            {
                _settings = new Settings();
            }
        }
        public void WriteSettings()
        {
            string jsonString = JsonSerializer.Serialize(_settings);

            File.WriteAllText(settingsPath, jsonString);
        }

        public void UpdatePath(string path)
        {
            _settings.VideoPath = path;
        }

        public void UpdateUsers(string users)
        {
            var userList = users.Split("\n").Select(x => x.Trim()).ToList();

            _settings.Users = userList;
        }

        public string GetPath()
        {
            return _settings.VideoPath;
        }

        public string GetUsers()
        {
            return String.Join("\n", _settings.Users);
        }
    }
}
