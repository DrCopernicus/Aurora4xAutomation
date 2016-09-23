using Aurora4xAutomation.Common;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.Settings;
using System;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command.Parser
{
    public class CommandParser
    {
        public CommandParser(IUIMap uiMap, ISettingsStore settings, IMessageManager messages, IEventManager eventManager)
        {
            UIMap = uiMap;
            Settings = settings;
            Lexer = new CommandLexer(UIMap, settings, messages, eventManager);
        }

        public IEvaluator Parse(string choice)
        {
            try
            {
                return Lexer.Lex(choice);
            }
            catch (Exception e)
            {
                new MessageCommands(Settings).PrintError(e.Message);
                return new NoOpEvaluator("noop");
            }
        }

        public void Parse2(string choice)
        {
//            if (choice.Matches("^r(esearch)? [a-zA-Z]+ [0-9]+ [0-9]+ [0-9]+$"))
//                new ResearchCommands(UIMap, Settings).ResearchTechCommand(choice.Split(' ')[1], int.Parse(choice.Split(' ')[2]), int.Parse(choice.Split(' ')[3]), int.Parse(choice.Split(' ')[4]));
//
//            else if (choice.Matches("^adv(ance)? [0-9]*[a-z]+"))
//                Settings.Increment = GetIncrementFromAbbreviation(choice.Split(' ')[1]);
//
//            else if (choice.Matches("^b(uild)? ship [0-9]+ [0-9]+$"))
//            {
//                var shipyardNumber = int.Parse(choice.Split(' ')[2]);
//                var shipsNumber = int.Parse(choice.Split(' ')[3]);
//                UIMap.PopulationAndProduction.MakeActive();
//                UIMap.PopulationAndProduction.SelectManageShipyards();
//                UIMap.PopulationAndProduction.SelectNthShipyard(shipyardNumber);
//                for (int i = 0; i < shipsNumber; i++)
//                    UIMap.PopulationAndProduction.AddShipyardTask();
//            }
//
//            else if (choice.Matches("^b(uild)? inst(allation)? [a-z0-9\\-]+ [a-z]+ [0-9]+$"))
//            {
//                new InfrastructureCommands(UIMap).BuildInstallation(choice.Split(' ')[2], choice.Split(' ')[3], choice.Split(' ')[4]);
//            }
//
//            else if (choice.Matches("^auto assign(ment(s)?)? on$"))
//            {
//                UIMap.Leaders.MakeActive();
//                UIMap.Leaders.SetAutomatedAssignments(true);
//            }
//
//            else if (choice.Matches("^auto assign(ment(s)?)? off$"))
//            {
//                UIMap.Leaders.MakeActive();
//                UIMap.Leaders.SetAutomatedAssignments(false);
//            }
//
//            else if (choice.Matches("^auto research focus [a-z]+$"))
//                new ResearchCommands(UIMap, Settings).FocusResearch(choice.Split(' ')[3]);
//
//            else if (choice.Matches("^auto research ban [a-z]+$"))
//                new ResearchCommands(UIMap, Settings).BanResearch(choice.Split(' ')[3]);
//
//            else if (choice.Matches("^auto research on$"))
//                Settings.AutoResearchOn = true;
//
//            else if (choice.Matches("^auto research off$"))
//                Settings.AutoResearchOn = false;
//
//            else if (choice.Matches("^clear$"))
//            {
//                Settings.FeedbackMessage = "";
//                Settings.InterruptMessage = "";
//                Settings.ErrorMessage = "";
//            }
        }

        private IncrementLength GetIncrementFromAbbreviation(string s)
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

        private IUIMap UIMap { get; set; }
        private CommandLexer Lexer { get; set; }
        private ISettingsStore Settings { get; set; }
    }
}
