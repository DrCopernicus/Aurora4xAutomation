using System.Collections.Generic;

namespace Server.Settings
{
    public interface ISettingsStore
    {
        bool Stopped { get; set; }
        bool AutoTurnsOn { get; set; }
        string DatabaseLocation { get; }
        string DatabasePassword { get; }
        string EventLogLocation { get; }
        int RaceId { get; set; }
        Dictionary<string, string> Research { get; set; }
        Dictionary<string, Dictionary<string, string>> ResearchFocuses { get; }
        int GameId { get; set; }
        IncrementLength Increment { get; set; }
        string GameName { get; }
    }
}
