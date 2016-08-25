using Aurora4xAutomation.Command;
using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Automation
{
    public static class CommandFlowManager
    {
        public static void QueueCommand(string command, Time time = null)
        {
            EventManager.AddEvent(CommandParser.Parse(command), time);
        }

        public static void QueueCommand(IEvaluator evaluator, Time time = null)
        {
            EventManager.AddEvent(evaluator, time);
        }

        public static void Begin()
        {
            Settings.Research = Settings.ResearchFocuses["beamfocus"];

            while (true)
            {
                EventManager.ActOnActiveTimelineEntries();

                if (!Settings.Stopped)
                {
                    EventManager.ParseEvents();

                    if (!Settings.Stopped)
                        new TurnCommands(AuroraUI, Settings).AdvanceTurn();

                    if (!Settings.AutoTurnsOn)
                        new StopEvaluator("stop", Settings).Execute();
                }
                else
                {
                    Settings.StatusMessage = "Waiting for user input";
                }

                Sleeper.Sleep(1000);
            }
        }

        private static readonly SettingsStore Settings = new SettingsStore();
        private static readonly UIMap AuroraUI = new UIMap(Settings);
        private static readonly MessageManager Messages = new MessageManager();
        private static readonly EventManager EventManager = new EventManager(AuroraUI, Settings);
        private static readonly CommandParser CommandParser = new CommandParser(AuroraUI, Settings, Messages);
    }
}
