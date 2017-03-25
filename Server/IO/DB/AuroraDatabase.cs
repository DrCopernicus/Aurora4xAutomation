using Server.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace Server.IO.DB
{
    public class AuroraDatabase : IAuroraDB
    {
        private IQueryExecutor Executor { get; set; }

        public AuroraDatabase(IQueryExecutor executor)
        {
            Executor = executor;
        }

        public long GetTime(ISettingsStore settings, OleDbConnection connection = null)
        {
            var previousConnection = connection != null;
            if (!previousConnection)
            {
                connection = Executor.GetConnection(settings.DatabaseLocation, settings.DatabasePassword);
                connection.Open();
            }

            var data = Executor.Execute(string.Format("SELECT GameTime FROM Game WHERE GameID={0}", settings.GameId), connection);
            var time = data.Tables[0].Rows[0]["GameTime"];

            if (!previousConnection)
                connection.Close();

            return Convert.ToInt64(time);
        }

        public List<AuroraEventEntry> GetRecentEvents(ISettingsStore settings, int raceId, double time, OleDbConnection connection)
        {
            var previousConnection = connection != null;
            if (!previousConnection)
            {
                connection = Executor.GetConnection(settings.DatabaseLocation, settings.DatabasePassword);
                connection.Open();
            }

            var data = Executor.Execute(string.Format("SELECT EventType, MessageText FROM GameLog WHERE GameID={0} AND RaceID={1} AND Time>={2}", settings.GameId, raceId, time), connection);
            var list = new List<AuroraEventEntry>();
            foreach (DataRow row in data.Tables[0].Rows)
                list.Add(new AuroraEventEntry((int) row["EventType"], (string) row["MessageText"]));

            if (!previousConnection)
                connection.Close();

            return list;
        }
    }
}
