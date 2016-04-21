using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aurora4xAutomation.UI;
using NUnit.Framework;

namespace Aurora4xAutomation.Tests
{
    [TestFixture]
    public class Sandbox
    {
        [Test]
        public void MyTest()
        {
            var accessConnStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\prog\Aurora\Aurora_latest\Aurora\Stevefire.mdb;Jet OLEDB:Database Password=raistlin31";
            var getEventsQuery = @"SELECT * FROM GameLog";

            var myDataSet = new DataSet();
            var accessConn = new OleDbConnection(accessConnStr);

            var myAccessCommand = new OleDbCommand(getEventsQuery, accessConn);
            var myDataAdapter = new OleDbDataAdapter(myAccessCommand);

            accessConn.Open();
            myDataAdapter.Fill(myDataSet);
            var r = myDataSet.Tables[0].Rows;
            var taurora = r[r.Count-1]["Time"];
            var treal = DateTime.FromOADate((double) taurora);
            accessConn.Close();
        }
    }
}
