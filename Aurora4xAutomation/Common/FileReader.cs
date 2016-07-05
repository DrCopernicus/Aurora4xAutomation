using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Aurora4xAutomation.Common
{
    public static class FileReader
    {
        public static Dictionary<string, string> ReadSettingsFile(string path)
        {
            string cd = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string[] lines = File.ReadAllLines(Path.Combine(cd, @"Settings\" + path));
            
            var dict = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                var split = line.Split(new [] {'\t'}, StringSplitOptions.RemoveEmptyEntries);
                dict.Add(split[1], split[0]);
            }

            return dict;
        }

        public static bool SettingsFileExists(string path)
        {
            string cd = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return File.Exists(Path.Combine(cd, path));
        }
    }
}
