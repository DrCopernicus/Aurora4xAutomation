using System;
using System.Collections.Generic;
using System.Linq;
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
            new Thread(() => BeginUpdater(client)).Start();
            ReadInput(client);
        }

        private RESTClient InitializeClient()
        {
            Console.WriteLine("Please specify the server to connect to (example: http://192.168.1.2:1234):");
            var server = Console.ReadLine();
            var client = new RESTClient(server);
            return client;
        }

        private void ReadInput(RESTClient client)
        {
            while (true)
            {
                var character = Console.ReadKey();
                _currentInput += character.KeyChar;
                if (character.Key == ConsoleKey.Enter)
                {
                    lock (_writeToConsoleLock)
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
        }

        private void ProcessInput(RESTClient client, string command)
        {
            var res = new RESTRequest("/command");
            res.AddQuery("q", command);
            Console.WriteLine(client.Execute(res).Content);
        }

        private void BeginUpdater(RESTClient client)
        {
            while (true)
            {
                var latestId = GetLatestMessageId(client);
                var messages = GetNewMessages(client, _lastFoundMessage, latestId);
                _lastFoundMessage = latestId;

                if (messages.Any())
                {
                    lock (_writeToConsoleLock)
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
                }

                Thread.Sleep(1000);
            }
        }

        private List<string> GetNewMessages(RESTClient client, long startingId, long endingId)
        {
            var res = new RESTRequest("/messages");
            res.AddQuery("after", startingId.ToString());
            res.AddQuery("upto", endingId.ToString());
            var response = client.Execute(res).Content;
            if (string.IsNullOrEmpty(response))
                return new List<string>();
            return response.Split('\n').ToList();
        }

        private long GetLatestMessageId(RESTClient client)
        {
            var res = new RESTRequest("/lastmessage");
            return Convert.ToInt64(client.Execute(res).Content);
        }

        private string _currentInput = "";
        private long _lastFoundMessage = -1;

        private readonly object _writeToConsoleLock = new object();
    }
}
