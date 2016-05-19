using System.Data;
using System.Data.OleDb;

namespace Aurora4xAutomation.DB
{
    public class QueryExecutor
    {
        public static OleDbConnection GetConnection()
        {
            var accessConnStr = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\prog\Aurora\Aurora_latest\Aurora\Stevefire.mdb;Jet OLEDB:Database Password={0}", Settings.DatabasePassword);
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
