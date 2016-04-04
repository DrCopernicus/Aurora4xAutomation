﻿using System.Collections.Generic;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation
{
    public class Settings
    {
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

        public static bool AutoResearchOn = false;
        public static bool AutoTurnsOn = false;
    }
}
