using System.Data;
using System.Data.OleDb;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.DB
{
    public class QueryExecutor
    {
        public static OleDbConnection GetConnection()
        {
            var accessConnStr = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password={1}", SettingsStore.DatabaseLocation, SettingsStore.DatabasePassword);
            return new OleDbConnection(accessConnStr);
        }

        public static DataSet Execute(string query, OleDbConnection connection)
        {
            var data = new DataSet();
            var adapter = new OleDbDataAdapter(new OleDbCommand(query, connection));
            adapter.Fill(data);
            return data;
        }
    }
}
