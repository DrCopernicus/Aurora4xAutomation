using System;
using Grapevine.Client;

namespace Aurora4xAutomationClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please specify the server to connect to:");
            var server = Console.ReadLine();
            var client = new RESTClient(server);
            var res = new RESTRequest("/");
            Console.WriteLine(client.Execute(res).Content);
            Console.ReadKey();
        }
    }
}
