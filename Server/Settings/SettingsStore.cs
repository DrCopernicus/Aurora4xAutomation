using Server.Common;
using System;
using System.Collections.Generic;

namespace Server.Settings
{
    public class SettingsStore : ISettingsStore
    {
        public SettingsStore()
        {
            AutoTurnsOn = false;
            Stopped = true;
            RaceId = 133;
            GameId = 18;
            Increment = IncrementLength.FiveDay;
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

        public int GameId { get; set; }
        public IncrementLength Increment { get; set; }

        private Dictionary<string, string> _research;

        public bool AutoResearchOn = false;

        public string ErrorMessage = "";
        public string InterruptMessage = "";
        public string FeedbackMessage = "";
        public string StatusMessage = "";

        public int MinLabsPerScientist = 2;
        public int DaysPerLabsCheck = 60;

        public string GameName
        {
            get { return "New Game Name"; }
        }

        private int _horizontalOffset = 0;
        public int HorizontalWindowOffset
        {
            get { return _horizontalOffset; }
            set { _horizontalOffset = value; }
        }

        private int _verticalOffset = 0;
        public int VerticalWindowOffset
        {
            get { return _verticalOffset; }
            set { _verticalOffset = value; }
        }

        private bool _checkedDatabasePassword;
        private string _databasePassword;

        public string DatabasePassword
        {
            get
            {
                if (_checkedDatabasePassword)
                    return _databasePassword;

                if (FileReader.SettingsFileExists("password.txt"))
                    _databasePassword = FileReader.ReadSettingsFile("password.txt")["Database Password"];
                _checkedDatabasePassword = true;
                Console.WriteLine("password:" + _databasePassword);
                return _databasePassword;
            }
        }

        public bool Stopped { get; set; }
        public bool AutoTurnsOn { get; set; }
        public string DatabaseLocation { get { return @"C:\Users\Administrator\Desktop\Aurora\Stevefire.mdb"; } }

        public string EventLogLocation { get { return @"C:\Users\Administrator\Desktop\Aurora\FederationEventLog.txt"; } }
        public int RaceId { get; set; }
    }
}
