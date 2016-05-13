using System;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command.Parser
{
    public class CommandParser
    {
        public static void Parse(string choice)
        {
            try
            {
                var command = CommandLexer.Lex(choice);
                command.Execute();
            }
            catch (Exception e)
            {
                Timeline.AddEvent(MessageCommands.PrintError, e.Message);
            }
        }

        public static void Parse2(string choice)
        {
//            if (choice.Matches("^o(pen)? r$"))
//                Timeline.AddEvent(OpenCommands.OpenResearch);
//
//            else if (choice.Matches("^o(pen)? ship$"))
//                Timeline.AddEvent(OpenCommands.OpenShipyard);
//
//            else if (choice.Matches("^o(pen)? tg$"))
//                Timeline.AddEvent(OpenCommands.OpenTaskGroup);
//
//            else if (choice.Matches("^o(pen)? r [a-zA-Z]+$"))
//                Timeline.AddEvent(OpenCommands.OpenResearchCategory, choice.Split(' ')[2]);

            if (choice.Matches("^r(esearch)? [a-zA-Z]+ [0-9]+ [0-9]+ [0-9]+$"))
                ResearchCommands.ResearchTechCommand(choice.Split(' ')[1], int.Parse(choice.Split(' ')[2]), int.Parse(choice.Split(' ')[3]), int.Parse(choice.Split(' ')[4]));

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

            else if (choice.Matches("^b(uild)? inst(allation)? [a-z0-9\\-]+ [a-z]+ [0-9]+$"))
            {
                InfrastructureCommands.BuildInstallation(choice.Split(' ')[2], choice.Split(' ')[3], choice.Split(' ')[4]);
            }

            else if (choice.Matches("^auto assign(ment(s)?)? on$"))
            {
                UIMap.Leaders.MakeActive();
                UIMap.Leaders.SetAutomatedAssignments(true);
            }

            else if (choice.Matches("^auto assign(ment(s)?)? off$"))
            {
                UIMap.Leaders.MakeActive();
                UIMap.Leaders.SetAutomatedAssignments(false);
            }

            else if (choice.Matches("^auto research focus [a-z]+$"))
                ResearchCommands.FocusResearch(choice.Split(' ')[3]);

            else if (choice.Matches("^auto research ban [a-z]+$"))
                ResearchCommands.BanResearch(choice.Split(' ')[3]);

            else if (choice.Matches("^auto research on$"))
                Settings.AutoResearchOn = true;

            else if (choice.Matches("^auto research off$"))
                Settings.AutoResearchOn = false;

//            else if (choice.Matches("^mv [0-9a-z\\-]+ [a-z]+ [0-9]+ (s|d)"))
//            {
//                if (choice.Split(' ')[4] != "s" && choice.Split(' ')[4] != "d")
//                    throw new Exception("e");
//                InfrastructureCommands.MakeCivilianContract(choice.Split(' ')[1],
//                                                              choice.Split(' ')[2],
//                                                              int.Parse(choice.Split(' ')[3]),
//                                                              choice.Split(' ')[4] == "s");
//            }

            else if (choice.Matches("^clear$"))
            {
                Settings.FeedbackMessage = "";
                Settings.InterruptMessage = "";
                Settings.ErrorMessage = "";
            }
        }

        private static Settings.IncrementLength GetIncrementFromAbbreviation(string s)
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
    }
}
