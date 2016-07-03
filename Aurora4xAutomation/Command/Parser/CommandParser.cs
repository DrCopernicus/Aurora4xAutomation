using System;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.Settings;
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
                MessageCommands.PrintError(e.Message);
            }
        }

        public static void Parse2(string choice)
        {
            if (choice.Matches("^r(esearch)? [a-zA-Z]+ [0-9]+ [0-9]+ [0-9]+$"))
                ResearchCommands.ResearchTechCommand(choice.Split(' ')[1], int.Parse(choice.Split(' ')[2]), int.Parse(choice.Split(' ')[3]), int.Parse(choice.Split(' ')[4]));

            else if (choice.Matches("^adv(ance)? [0-9]*[a-z]+"))
                SettingsStore.Increment = GetIncrementFromAbbreviation(choice.Split(' ')[1]);

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
                SettingsStore.AutoResearchOn = true;

            else if (choice.Matches("^auto research off$"))
                SettingsStore.AutoResearchOn = false;

            else if (choice.Matches("^clear$"))
            {
                SettingsStore.FeedbackMessage = "";
                SettingsStore.InterruptMessage = "";
                SettingsStore.ErrorMessage = "";
            }
        }

        private static SettingsStore.IncrementLength GetIncrementFromAbbreviation(string s)
        {
            switch (s)
            {
                case "off":
                    SettingsStore.AutoTurnsOn = false;
                    return SettingsStore.Increment;
                case "on":
                    SettingsStore.AutoTurnsOn = true;
                    return SettingsStore.Increment;
                case "5s":
                    return SettingsStore.IncrementLength.FiveSecond;
                case "30s":
                    return SettingsStore.IncrementLength.ThirtySecond;
                case "2m":
                    return SettingsStore.IncrementLength.TwoMinute;
                case "5m":
                    return SettingsStore.IncrementLength.FiveMinute;
                case "20m":
                    return SettingsStore.IncrementLength.TwentyMinute;
                case "1h":
                    return SettingsStore.IncrementLength.OneHour;
                case "3h":
                    return SettingsStore.IncrementLength.ThreeHour;
                case "8h":
                    return SettingsStore.IncrementLength.EightHour;
                case "1d":
                    return SettingsStore.IncrementLength.OneDay;
                case "5d":
                    return SettingsStore.IncrementLength.FiveDay;
                case "30d":
                    return SettingsStore.IncrementLength.ThirtyDay;
                default:
                    return SettingsStore.IncrementLength.FiveDay;
            }
        }
    }
}
