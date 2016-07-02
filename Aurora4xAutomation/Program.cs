using System;
using Aurora4xAutomation.Command;
using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.Settings;
using Aurora4xAutomation.UI;
using Grapevine.Server;

namespace Aurora4xAutomation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var p = new Program();
        }

        public Program()
        {
            SettingsStore.Research = SettingsStore.ResearchFocuses["beamfocus"];

            var server = new RESTServer(host: "*");
            server.Start();

            while (server.IsListening)
            {
                ActOnActiveTimelineEntries();

                if (!SettingsStore.Stopped)
                {
                    ParseEvents();

                    if (!SettingsStore.Stopped)
                        TurnCommands.AdvanceTurn(this, EventArgs.Empty);

                    if (!SettingsStore.AutoTurnsOn)
                        Timeline.AddEvent(SettingsCommands.Stop);
                }
                else
                {
                    SettingsStore.StatusMessage = "Waiting for user input";
                }

                Sleeper.Sleep(2000);
            }
        }

        private void WriteWaitingForInputInfoBar()
        {
            Console.Clear();
            Console.Write("[{0,20}]", IncrementString);
            Console.ForegroundColor = ConsoleColor.Black;
            if (SettingsStore.AutoTurnsOn)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write("[{0}]", "AUTOTURNS  ENABLED");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("[{0}]", "AUTOTURNS DISABLED");
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(SettingsStore.FeedbackMessage);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(SettingsStore.InterruptMessage);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(SettingsStore.ErrorMessage == "" ? "" : "\n\n" + SettingsStore.ErrorMessage);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void ParseEvents()
        {
            SettingsStore.StatusMessage = "Parsing events log";
            UIMap.EventWindow.MakeActive();
            if (SettingsStore.DatabasePassword == null)
                EventParser.ParseUsingEventWindow(UIMap.SystemMap.GetTime());
            else
                EventParser.ParseUsingDatabase();
        }

        private void MakeChoices()
        {
            while (true)
            {
                var choice = Console.ReadLine().ToLower();

                if (choice == "")
                    break;

                CommandParser.Parse(choice);

                ActOnActiveTimelineEntries();
                WriteWaitingForInputInfoBar();
            }
        }

        private void ActOnActiveTimelineEntries()
        {
            SettingsStore.StatusMessage = "Evaluating events";
            AuroraEvent ev;
            while ((ev = Timeline.NextActiveEvent) != null)
                ev.Invoke();
        }

        private string IncrementString
        {
            get
            {
                switch (SettingsStore.Increment)
                {
                    case SettingsStore.IncrementLength.FiveSecond:
                        return "5 Seconds";
                    case SettingsStore.IncrementLength.ThirtySecond:
                        return "30 Seconds";
                    case SettingsStore.IncrementLength.TwoMinute:
                        return "2 Minutes";
                    case SettingsStore.IncrementLength.FiveMinute:
                        return "5 Minutes";
                    case SettingsStore.IncrementLength.TwentyMinute:
                        return "20 Minutes";
                    case SettingsStore.IncrementLength.OneHour:
                        return "1 Hour";
                    case SettingsStore.IncrementLength.ThreeHour:
                        return "3 Hours";
                    case SettingsStore.IncrementLength.EightHour:
                        return "8 Hours";
                    case SettingsStore.IncrementLength.OneDay:
                        return "1 Day";
                    case SettingsStore.IncrementLength.FiveDay:
                        return "5 Days";
                    case SettingsStore.IncrementLength.ThirtyDay:
                        return "30 Days";
                    default:
                        return "INCORRECT INCREMENT LENGTH";
                }
            }
        }
    }
}
