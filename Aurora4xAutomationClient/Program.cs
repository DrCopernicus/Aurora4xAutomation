using System;
using System.Collections.Generic;
using System.Threading;
using Grapevine.Client;

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
            var client = InitializeClient();
            Console.Write("$> ");
            new Thread(BeginUpdater).Start();
            ReadInput(client);
        }

        private RESTClient InitializeClient()
        {
            Console.WriteLine("Please specify the server to connect to:");
            var server = Console.ReadLine();
            var client = new RESTClient(server);
            return client;
        }

        private void WaitForServerNotBusy()
        {
            
        }

        private void ReadInput(RESTClient client)
        {
            while (true)
            {
                var character = Console.ReadKey();
                _currentInput += character.KeyChar;
                if (character.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    ProcessInput(client, _currentInput);
                    Console.WriteLine();
                    Console.Write("$> ");
                    _currentInput = "";
                }
            }
        }

        private void ProcessInput(RESTClient client, string command)
        {
            var res = new RESTRequest("/command?q=" + command);
            Console.WriteLine(client.Execute(res).Content);
        }

        private void BeginUpdater()
        {
            while (true)
            {
                var messages = GetNewMessages();

                if (messages.Count > 0)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write("                                                                        ");

                    foreach (var message in messages)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.WriteLine(message);
                    }

                    Console.Write("$> " + _currentInput);
                }

                Thread.Sleep(2000);
            }
        }

        private List<string> GetNewMessages()
        {
            return new List<string>(){};
        }

        private string _currentInput = "";
    }
}
