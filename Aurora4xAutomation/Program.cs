using System;
using System.Threading;
using Aurora4xAutomation.Command;
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
            _events.MakeActive();
            _events.ClickTextFileButton();
            Thread.Sleep(1500);
            EventParser.AnyStopEvents(_systemMap.GetTime());
        }

        private void MakeChoices()
        {
            while (true)
            {
                var choice = Console.ReadLine().ToLower();

                if (choice.Matches("^o(pen)? r$"))
                    Timeline.AddEvent(_commands.OpenCommands.OpenResearch);

                else if (choice.Matches("^o(pen)? ship$"))
                    Timeline.AddEvent(_commands.OpenCommands.OpenShipyard);

                else if (choice.Matches("^o(pen)? tg$"))
                    Timeline.AddEvent(_commands.OpenCommands.OpenTaskGroup);

                else if (choice.Matches("^o(pen)? r [a-zA-Z]+$"))
                    Timeline.AddEvent(_commands.OpenCommands.OpenResearchCategory, choice.Split(' ')[2]);

                else if (choice.Matches("^r(esearch)? [a-zA-Z]+ [0-9]+ [0-9]+ [0-9]+$"))
                    _commands.ResearchCommands.ResearchTechCommand(choice.Split(' ')[1], int.Parse(choice.Split(' ')[2]), int.Parse(choice.Split(' ')[3]), int.Parse(choice.Split(' ')[4]));

                else if (choice.Matches("^adv(ance)? [0-9]*[a-z]+"))
                    Settings.Increment = GetIncrementFromAbbreviation(choice.Split(' ')[1]);

                else if (choice.Matches("^b(uild)? ship [0-9]+ [0-9]+$"))
                {
                    var shipyardNumber = int.Parse(choice.Split(' ')[2]);
                    var shipsNumber = int.Parse(choice.Split(' ')[3]);
                    UIMap.PopulationAndProductionWindow.MakeActive();
                    UIMap.PopulationAndProductionWindow.SelectManageShipyards();
                    UIMap.PopulationAndProductionWindow.SelectNthShipyard(shipyardNumber);
                    for (int i = 0; i < shipsNumber; i++)
                        UIMap.PopulationAndProductionWindow.AddShipyardTask();
                }

                else if (choice.Matches("^b(uild)? inst(allation)? [a-z]+ [0-9]*$"))
                {
                    var installationName = choice.Split(' ')[2];
                    var installationNumber = choice.Split(' ')[3];
                    UIMap.PopulationAndProductionWindow.MakeActive();
                    UIMap.PopulationAndProductionWindow.SelectIndustry();
                    switch (installationName)
                    {
                        case "automine":
                            UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(0);
                            break;
                        case "csc":
                            UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(1);
                            break;
                        case "inf":
                        case "infra":
                        case "infrastructure":
                            UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(10);
                            break;
                        case "massdriver":
                            UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(11);
                            break;
                        case "nsc":
                            UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(14);
                            break;
                    }
                    UIMap.PopulationAndProductionWindow.NumberOfIndustrialProject.Text = installationNumber;
                    UIMap.PopulationAndProductionWindow.CreateIndustrialProject.Click();
                }

                else if (choice.Matches("^auto assign(ment(s)?)? on$"))
                {
                    _commanders.MakeActive();
                    _commanders.SetAutomatedAssignments(true);
                }

                else if (choice.Matches("^auto assign(ment(s)?)? off$"))
                {
                    _commanders.MakeActive();
                    _commanders.SetAutomatedAssignments(false);
                }

                else if (choice.Matches("^auto research focus [a-z]+$"))
                    _commands.ResearchCommands.FocusResearch(choice.Split(' ')[3]);

                else if (choice.Matches("^auto research ban [a-z]+$"))
                    _commands.ResearchCommands.BanResearch(choice.Split(' ')[3]);

                else if (choice.Matches("^auto research on$"))
                    Settings.AutoResearchOn = true;

                else if (choice.Matches("^auto research off$"))
                    Settings.AutoResearchOn = false;

                else if (choice.Matches("^c(ontract)? [a-z]+ [0-9]+ (s|d)"))
                {
                    if (choice.Split(' ')[3] != "s" && choice.Split(' ')[3] != "d")
                        throw new Exception("e");
                    InfrastructureCommands.TransferInfrastructure(choice.Split(' ')[1],
                                                                  int.Parse(choice.Split(' ')[2]),
                                                                  choice.Split(' ')[3] == "s");
                }
                    
                else if (choice.Matches("^clear$"))
                {
                    Settings.FeedbackMessage = "";
                    Settings.InterruptMessage = "";
                    Settings.ErrorMessage = "";
                }

                else if (choice == "")
                {
                    break;
                }

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

        private Settings.IncrementLength GetIncrementFromAbbreviation(string s)
        {
            switch (s)
            {
                case "off":
                    Settings.AutoTurnsOn = false;
                    return Settings.Increment;
                case "on":
                    Settings.AutoTurnsOn = true;
                    return Settings.Increment;
                case "5s":
                    return Settings.IncrementLength.FiveSecond;
                case "30s":
                    return Settings.IncrementLength.ThirtySecond;
                case "2m":
                    return Settings.IncrementLength.TwoMinute;
                case "5m":
                    return Settings.IncrementLength.FiveMinute;
                case "20m":
                    return Settings.IncrementLength.TwentyMinute;
                case "1h":
                    return Settings.IncrementLength.OneHour;
                case "3h":
                    return Settings.IncrementLength.ThreeHour;
                case "8h":
                    return Settings.IncrementLength.EightHour;
                case "1d":
                    return Settings.IncrementLength.OneDay;
                case "5d":
                    return Settings.IncrementLength.FiveDay;
                case "30d":
                    return Settings.IncrementLength.ThirtyDay;
                default:
                    return Settings.IncrementLength.FiveDay;
            }
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

        private readonly Commands _commands = new Commands();
        private readonly EventWindow _events = new EventWindow();
        private readonly ConsoleWindow _console = new ConsoleWindow();
        private readonly SystemMapWindow _systemMap = new SystemMapWindow();
        private readonly CommandersWindow _commanders = new CommandersWindow();
    }
}
