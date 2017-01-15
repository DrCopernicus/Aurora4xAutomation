using Aurora4xAutomationClient.ClientUI;
using Aurora4xAutomationClient.ClientUI.Client;
using Aurora4xAutomationClient.ClientUI.Listeners;
using Aurora4xAutomationClient.ClientUI.Terminal;
using System.Threading;

namespace Aurora4xAutomationClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var p = new Program();
        }

        public Program()
        {
            var client = new ClientWrapper();

            var writer = new ConsoleWriter();
            var terminal = new ClientTerminal();
            var console = new ClientConsole(terminal, writer, client);

            client.InitializeConnection(console);

            var inputListener = new ConsoleInputListener(console);
            var serverListener = new ServerMessageListener(client, console);

            new Thread(() => inputListener.BeginListening()).Start();
            new Thread(() => serverListener.BeginListening()).Start();
        }
    }
}
