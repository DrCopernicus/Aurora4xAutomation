using Aurora4xAutomationClient.ClientUI.Console;
using Grapevine.Client;

namespace Aurora4xAutomationClient.ClientUI.Client
{
    public class ConnectionCreator
    {
        private readonly IConsole _console;

        public ConnectionCreator(IConsole console)
        {
            _console = console;
        }

        public RESTClient CreateClient()
        {
            _console.WriteLine("Please specify the server to connect to (example: http://192.168.1.2:1234):");
            var server = _console.ReadLine();
            var client = new RESTClient(server);
            return client;
        }
    }
}
