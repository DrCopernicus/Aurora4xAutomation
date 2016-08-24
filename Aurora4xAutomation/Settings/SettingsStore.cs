using System.Collections.Generic;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.Settings
{
    public class SettingsStore
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

        public Dictionary<string, Dictionary<string, string>> ResearchFocuses
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

        private Dictionary<string, Dictionary<string, string>> _researchFocuses;

        public Dictionary<string, string> Research
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

        private Dictionary<string, string> _research;

        public IncrementLength Increment = IncrementLength.FiveDay;

        public bool AutoResearchOn = false;
        public bool AutoTurnsOn = false;
        public bool Stopped = true;

        public string ErrorMessage = "";
        public string InterruptMessage = "";
        public string FeedbackMessage = "";
        public string StatusMessage = "";

        public int GameId = 18;
        public int RaceId = 133;

        public int MinLabsPerScientist = 2;
        public int DaysPerLabsCheck = 60;

        public string GameName = "AutomatedAuroraGame";

        private string _databasePassword;

        public string DatabasePassword
        {
            get
            {
                if (_databasePassword == null && FileReader.SettingsFileExists("password.txt"))
                    _databasePassword = FileReader.ReadSettingsFile("password.txt")["DatabasePassword"];
                return _databasePassword;
            }
        }

        public string DatabaseLocation = @"C:\Users\Administrator\Desktop\Aurora\Stevefire.mdb";

        public string EventLogLocation = @"C:\Users\Administrator\Desktop\Aurora\FederationEventLog.txt";

    }
}
