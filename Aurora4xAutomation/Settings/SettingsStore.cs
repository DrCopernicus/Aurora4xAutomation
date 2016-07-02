using System.Collections.Generic;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.Settings
{
    public static class SettingsStore
    {
        public enum IncrementLength
        {
            FiveSecond,
            ThirtySecond,
            TwoMinute,
            FiveMinute,
            TwentyMinute,
            OneHour,
            ThreeHour,
            EightHour,
            OneDay,
            FiveDay,
            ThirtyDay
        }

        public static Dictionary<string, Dictionary<string, string>> ResearchFocuses
        {
            get
            {
                if (_researchFocuses == null)
                {
                    _researchFocuses = new Dictionary<string, Dictionary<string, string>>();
                    _researchFocuses.Add("beamfocus", FileReader.ReadSettingsFile("beamfocus.txt"));
                }
                return _researchFocuses;
            }
        }

        private static Dictionary<string, Dictionary<string, string>> _researchFocuses;

        public static Dictionary<string, string> Research
        {
            get
            {
                if (_research == null)
                    _research = new Dictionary<string, string>();
                return _research;
            }
            set
            {
                if (value == null)
                    return;

                if (_research == null)
                    _research = new Dictionary<string, string>();

                _research.Clear();
                foreach (var kvp in value)
                {
                    _research.Add(kvp.Key, kvp.Value);
                }
            }
        }

        private static Dictionary<string, string> _research;

        public static IncrementLength Increment = IncrementLength.FiveDay;

        public static bool AutoResearchOn = false;
        public static bool AutoTurnsOn = false;
        public static bool Stopped = true;

        public static string ErrorMessage = "";
        public static string InterruptMessage = "";
        public static string FeedbackMessage = "";
        public static string StatusMessage = "";

        public static int GameId = 18;
        public static int RaceId = 133;

        public static int MinLabsPerScientist = 2;
        public static int DaysPerLabsCheck = 60;

        private static string _databasePassword;

        public static string DatabasePassword
        {
            get
            {
                if (_databasePassword == null && FileReader.SettingsFileExists("password.txt"))
                    _databasePassword = FileReader.ReadSettingsFile("password.txt")["DatabasePassword"];
                return _databasePassword;
            }
        }
    }
}
