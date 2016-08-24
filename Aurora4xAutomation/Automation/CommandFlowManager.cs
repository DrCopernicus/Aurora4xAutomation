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
            _eventManager.AddEvent(_commandParser.Parse(command), time);
        }

        public static void QueueCommand(IEvaluator evaluator, Time time = null)
        {
            _eventManager.AddEvent(evaluator, time);
        }

        public static void Begin()
        {
            while (true)
            {
                _eventManager.ActOnActiveTimelineEntries();

                if (!SettingsStore.Stopped)
                {
                    _eventManager.ParseEvents();

                    if (!SettingsStore.Stopped)
                        new TurnCommands(_auroraUI).AdvanceTurn();

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

        private static UIMap _auroraUI = new UIMap();
        private static Timeline _timeline = new Timeline();
        private static readonly EventManager _eventManager = new EventManager(_auroraUI);
        private static readonly CommandParser _commandParser = new CommandParser(_auroraUI);
    }
}
