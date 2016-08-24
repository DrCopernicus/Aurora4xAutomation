using Aurora4xAutomation.Command;
using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO;
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
            while (true)
            {
                EventManager.ActOnActiveTimelineEntries();

                if (!SettingsStore.Stopped)
                {
                    EventManager.ParseEvents();

                    if (!SettingsStore.Stopped)
                        new TurnCommands(AuroraUI).AdvanceTurn();

                    if (!SettingsStore.AutoTurnsOn)
                        SettingsCommands.Stop();
                }
                else
                {
                    SettingsStore.StatusMessage = "Waiting for user input";
                }

                Sleeper.Sleep(1000);
            }
        }

        private static readonly UIMap AuroraUI = new UIMap();
        private static readonly EventManager EventManager = new EventManager(AuroraUI);
        private static readonly CommandParser CommandParser = new CommandParser(AuroraUI);
    }
}
