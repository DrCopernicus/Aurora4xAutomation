﻿using System.Data;
using System.Data.OleDb;

namespace Server.IO.DB
{
    public class QueryExecutor : IQueryExecutor
    {
        public OleDbConnection GetConnection(string location, string password)
        {
            var accessConnStr = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password={1}", location, password);
            return new OleDbConnection(accessConnStr);
        }

        public DataSet Execute(string query, OleDbConnection connection)
        {
            var data = new DataSet();
            var adapter = new OleDbDataAdapter(new OleDbCommand(query, connection));
            adapter.Fill(data);
            return data;
        }
    }
}
