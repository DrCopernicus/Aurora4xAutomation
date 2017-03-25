using System.Data;
using System.Data.OleDb;

namespace Server.IO.DB
{
    public interface IQueryExecutor
    {
        OleDbConnection GetConnection(string location, string password);
        DataSet Execute(string query, OleDbConnection connection);
    }
}