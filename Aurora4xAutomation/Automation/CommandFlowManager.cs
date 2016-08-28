using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.Settings;
using System.Collections.Generic;

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
            EventManager.Begin(new Logger());
        }

        public static List<string> GetMessages(long startId, long endId = long.MaxValue)
        {
            return Messages.GetMessagesAfterId(startId, endId);
        }

        public static long GetLastMessageId()
        {
            return Messages.GetLastId();
        }

        private static readonly SettingsStore Settings = new SettingsStore();
        private static readonly UIMap AuroraUI = new UIMap(Settings);
        private static readonly MessageManager Messages = new MessageManager();
        private static readonly EventManager EventManager = new EventManager(AuroraUI, Settings, Messages);
        private static readonly CommandParser CommandParser = new CommandParser(AuroraUI, Settings, Messages);
    }
}
