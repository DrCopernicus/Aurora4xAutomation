using System;
using System.Threading;
using Aurora4xAutomation.Command;
using Aurora4xAutomation.Common;
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
            while (true)
            {
                Thread.Sleep(2000);

                _extraMessage = "";
                _currentMessage = IsStopEvent();

                if (_currentMessage != "" || !_autoturns)
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
            if (_autoturns)
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
            Console.WriteLine(_currentMessage+(_extraMessage == "" ? "" : "\n\n"+_extraMessage));
        }

        private string IsStopEvent()
        {
            _events.MakeActive();
            _events.ClickTextFileButton();
            Thread.Sleep(1500);
            return EventParser.CanContinue(_systemMap.GetTime());
        }

        private void MakeChoices()
        {
            while (true)
            {
                var choice = Console.ReadLine().ToLower();

                if (choice.Matches("^o r$"))
                    _commands.OpenCommands.OpenResearch();

                else if (choice.Matches("^o ship$"))
                    _commands.OpenCommands.OpenShipyard();

                else if (choice.Matches("^o tg$"))
                    _commands.OpenCommands.OpenTaskGroup();

                else if (choice.Matches("^o r [a-zA-Z]+$"))
                    _extraMessage = _commands.OpenCommands.OpenResearch(choice.Split(' ')[2]);

                else if (choice.Matches("^r [a-zA-Z]+ [0-9]+ [0-9]+ [0-9]+$"))
                    _commands.ResearchCommands.ResearchTechCommand(choice.Split(' ')[1], int.Parse(choice.Split(' ')[2]), int.Parse(choice.Split(' ')[3]), choice.Split(' ')[4]);

                else if (choice.Matches("^adv [0-9]*[a-z]+"))
                    _incrementLength = GetIncrementFromAbbreviation(choice.Split(' ')[1]);

                else if (choice.Matches("^b ship [0-9]+ [0-9]+$"))
                {
                    var shipyardNumber = int.Parse(choice.Split(' ')[2]);
                    var shipsNumber = int.Parse(choice.Split(' ')[3]);
                    _production.MakeActive();
                    _production.SelectManageShipyards();
                    _production.SelectNthShipyard(shipyardNumber);
                    for (int i = 0; i < shipsNumber; i++)
                        _production.AddShipyardTask();
                }

                else if (choice.Matches("^lead(er)? auto=(true|yes|1|on)$"))
                {
                    _commanders.MakeActive();
                    _commanders.SetAutomatedAssignments(true);
                    _console.MakeActive();
                }

                else if (choice.Matches("^lead(er)? auto=(false|no|0|off)$"))
                {
                    _commanders.MakeActive();
                    _commanders.SetAutomatedAssignments(false);
                    _console.MakeActive();
                }

                else if (choice.Matches("^clear$"))
                {
                    _currentMessage = "";
                    _extraMessage = "";
                }

                else if (choice == "")
                {
                    break;
                }

                WriteWaitingForInputInfoBar();
            }
        }

        private IncrementLength GetIncrementFromAbbreviation(string s)
        {
            switch (s)
            {
                case "off":
                    _autoturns = false;
                    return _incrementLength;
                case "on":
                    _autoturns = true;
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
        private bool _autoturns = false;
        private string _currentMessage = "";
        private string _extraMessage = "";

        private readonly Commands _commands = new Commands();
        private readonly EventWindow _events = new EventWindow();
        private readonly ConsoleWindow _console = new ConsoleWindow();
        private readonly SystemMapWindow _systemMap = new SystemMapWindow();
        private readonly CommandersWindow _commanders = new CommandersWindow();
        private readonly PopulationAndProductionWindow _production = new PopulationAndProductionWindow();
    }
}
