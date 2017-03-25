using Server.Settings;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Server.IO.DB
{
    public interface IAuroraDB
    {
        long GetTime(ISettingsStore settings, OleDbConnection connection = null);
        List<AuroraEventEntry> GetRecentEvents(ISettingsStore settings, int raceId, double time, OleDbConnection connection);
    }
}