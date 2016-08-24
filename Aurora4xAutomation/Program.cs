using System;
using System.Threading;
using Aurora4xAutomation.Automation;
using Aurora4xAutomation.REST;
using Aurora4xAutomation.Settings;

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
            new Thread(CommandFlowManager.Begin);
            new RESTManager().Begin();
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
