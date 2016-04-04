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
                Thread.Sleep(2000);

                Settings.ErrorMessage = "";
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

                _systemMap.MakeActive();
                switch (_incrementLength)
                {
                    case IncrementLength.FiveSecond:
                        _systemMap.ClickIncrement5SecondsButton();
                        break;
                    case IncrementLength.ThirtySecond:
                        _systemMap.ClickIncrement30SecondsButton();
                        break;
                    case IncrementLength.TwoMinute:
                        _systemMap.ClickIncrement2MinutesButton();
                        break;
                    case IncrementLength.FiveMinute:
                        _systemMap.ClickIncrement5MinutesButton();
                        break;
                    case IncrementLength.TwentyMinute:
                        _systemMap.ClickIncrement20MinutesButton();
                        break;
                    case IncrementLength.OneHour:
                        _systemMap.ClickIncrement1HoursButton();
                        break;
                    case IncrementLength.ThreeHour:
                        _systemMap.ClickIncrement3HoursButton();
                        break;
                    case IncrementLength.EightHour:
                        _systemMap.ClickIncrement8HoursButton();
                        break;
                    case IncrementLength.OneDay:
                        _systemMap.ClickIncrement1DayButton();
                        break;
                    case IncrementLength.FiveDay:
                        _systemMap.ClickIncrement5DaysButton();
                        break;
                    case IncrementLength.ThirtyDay:
                        _systemMap.ClickIncrement30DaysButton();
                        break;
                }
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
                    _incrementLength = GetIncrementFromAbbreviation(choice.Split(' ')[1]);

                else if (choice.Matches("^b(uild)? ship [0-9]+ [0-9]+$"))
                {
                    var shipyardNumber = int.Parse(choice.Split(' ')[2]);
                    var shipsNumber = int.Parse(choice.Split(' ')[3]);
                    _production.MakeActive();
                    _production.SelectManageShipyards();
                    _production.SelectNthShipyard(shipyardNumber);
                    for (int i = 0; i < shipsNumber; i++)
                        _production.AddShipyardTask();
                }

                else if (choice.Matches("^b(uild)? inst(allation)? [a-z]+ [0-9]*$"))
                {
                    var installationName = choice.Split(' ')[2];
                    var installationNumber = choice.Split(' ')[3];
                    _production.MakeActive();
                    _production.SelectIndustry();
                    switch (installationName)
                    {
                        case "automine":
                            _production.ConstructionOptions.ClickRow(0);
                            break;
                        case "csc":
                            _production.ConstructionOptions.ClickRow(1);
                            break;
                        case "massdriver":
                            _production.ConstructionOptions.ClickRow(11);
                            break;
                        case "nsc":
                            _production.ConstructionOptions.ClickRow(14);
                            break;
                    }
                    _production.NumberOfIndustrialProject.Text = installationNumber;
                    _production.CreateIndustrialProject.Click();
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
                    _commands.InfrastructureCommands.TransferInfrastructure(choice.Split(' ')[1],
                                                                            int.Parse(choice.Split(' ')[2]),
                                                                            choice.Split(' ')[3] == "s");
                }
                    
                else if (choice.Matches("^clear$"))
                {
                    Settings.FeedbackMessage = "";
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

        private IncrementLength GetIncrementFromAbbreviation(string s)
        {
            switch (s)
            {
                case "off":
                    Settings.AutoTurnsOn = false;
                    return _incrementLength;
                case "on":
                    Settings.AutoTurnsOn = true;
                    return _incrementLength;
                case "5s":
                    return IncrementLength.FiveSecond;
                case "30s":
                    return IncrementLength.ThirtySecond;
                case "2m":
                    return IncrementLength.TwoMinute;
                case "5m":
                    return IncrementLength.FiveMinute;
                case "20m":
                    return IncrementLength.TwentyMinute;
                case "1h":
                    return IncrementLength.OneHour;
                case "3h":
                    return IncrementLength.ThreeHour;
                case "8h":
                    return IncrementLength.EightHour;
                case "1d":
                    return IncrementLength.OneDay;
                case "5d":
                    return IncrementLength.FiveDay;
                case "30d":
                    return IncrementLength.ThirtyDay;
                default:
                    return IncrementLength.FiveDay;
            }
        }

        private enum IncrementLength
        {
            FiveSecond,
            ThirtySecond,
            TwoMinute,
            FiveMinute,
            TwentyMinute,
            OneHour,
            ThreeHour,
            EightHour,
            OneDay,
            FiveDay,
            ThirtyDay
        }

        private string IncrementString
        {
            get
            {
                switch (_incrementLength)
                {
                    case IncrementLength.FiveSecond:
                        return "5 Seconds";
                    case IncrementLength.ThirtySecond:
                        return "30 Seconds";
                    case IncrementLength.TwoMinute:
                        return "2 Minutes";
                    case IncrementLength.FiveMinute:
                        return "5 Minutes";
                    case IncrementLength.TwentyMinute:
                        return "20 Minutes";
                    case IncrementLength.OneHour:
                        return "1 Hour";
                    case IncrementLength.ThreeHour:
                        return "3 Hours";
                    case IncrementLength.EightHour:
                        return "8 Hours";
                    case IncrementLength.OneDay:
                        return "1 Day";
                    case IncrementLength.FiveDay:
                        return "5 Days";
                    case IncrementLength.ThirtyDay:
                        return "30 Days";
                    default:
                        return "INCORRECT INCREMENT LENGTH";
                }
            }
        }

        private IncrementLength _incrementLength = IncrementLength.FiveDay;

        private readonly Commands _commands = new Commands();
        private readonly EventWindow _events = new EventWindow();
        private readonly ConsoleWindow _console = new ConsoleWindow();
        private readonly SystemMapWindow _systemMap = new SystemMapWindow();
        private readonly CommandersWindow _commanders = new CommandersWindow();
        private readonly PopulationAndProductionWindow _production = new PopulationAndProductionWindow();
    }
}
