using System.Threading;
using Client.Listeners;
using Client.REST;
using Client.Terminal;

namespace Client
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

            var formatter = new ConsoleFormatter(new ConsoleWriter());
            var terminal = new ClientTerminal();
            var console = new ClientConsole(terminal, formatter, client);

            client.InitializeConnection(console);

            var inputListener = new ConsoleInputListener(console);
            var serverListener = new ServerMessageListener(client, console);

            new Thread(() => inputListener.BeginListening()).Start();
            new Thread(() => serverListener.BeginListening()).Start();
        }
    }
}
