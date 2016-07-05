using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grapevine.Server;

namespace Aurora4xAutomation.REST
{
    public class RESTManager
    {
        public void Begin()
        {
            var server = new RESTServer(host: "*");
            server.Start();

            while (server.IsListening)
            {
            }

        }
    }
}
