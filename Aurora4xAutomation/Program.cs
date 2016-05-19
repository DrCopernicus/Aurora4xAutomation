using System;
using Aurora4xAutomation.Command;
using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.UI;

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
            Settings.Research = Settings.ResearchFocuses["beamfocus"];

            while (true)
            {
                Settings.Stopped = false;
                Sleeper.Sleep(2000);

                Settings.ErrorMessage = "";
                Settings.InterruptMessage = "";
                Settings.FeedbackMessage = "";

                UIMap.EventWindow.MakeActive();
                ParseEvents();

                if (!Settings.AutoTurnsOn)
                    Timeline.AddEvent(SettingsCommands.Stop);

                ActOnActiveTimelineEntries();

                if (Settings.Stopped)
                {
                    _console.MakeActive();
                    WriteWaitingForInputInfoBar();
                    MakeChoices();
                }

                TurnCommands.AdvanceTurn(this, EventArgs.Empty);
            }
        }

        private void WriteWaitingForInputInfoBar()
        {
            Console.Clear();
            Console.Write("[{0,20}]", IncrementString);
            Console.ForegroundColor = ConsoleColor.Black;
            if (Settings.AutoTurnsOn)
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
            Console.WriteLine(Settings.FeedbackMessage);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Settings.InterruptMessage);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Settings.ErrorMessage == "" ? "" : "\n\n" + Settings.ErrorMessage);
            Console.ForegroundColor = ConsoleColor.White;
            
            _console.MakeActive();
        }

        private void ParseEvents()
        {
            if (Settings.DatabasePassword == null)
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
            AuroraEvent ev;
            while ((ev = Timeline.NextActiveEvent) != null)
                ev.Invoke();
        }

        private string IncrementString
        {
            get
            {
                switch (Settings.Increment)
                {
                    case Settings.IncrementLength.FiveSecond:
                        return "5 Seconds";
                    case Settings.IncrementLength.ThirtySecond:
                        return "30 Seconds";
                    case Settings.IncrementLength.TwoMinute:
                        return "2 Minutes";
                    case Settings.IncrementLength.FiveMinute:
                        return "5 Minutes";
                    case Settings.IncrementLength.TwentyMinute:
                        return "20 Minutes";
                    case Settings.IncrementLength.OneHour:
                        return "1 Hour";
                    case Settings.IncrementLength.ThreeHour:
                        return "3 Hours";
                    case Settings.IncrementLength.EightHour:
                        return "8 Hours";
                    case Settings.IncrementLength.OneDay:
                        return "1 Day";
                    case Settings.IncrementLength.FiveDay:
                        return "5 Days";
                    case Settings.IncrementLength.ThirtyDay:
                        return "30 Days";
                    default:
                        return "INCORRECT INCREMENT LENGTH";
                }
            }
        }

        private readonly ConsoleWindow _console = new ConsoleWindow();
    }
}
