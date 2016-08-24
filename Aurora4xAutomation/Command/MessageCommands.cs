using System;
using Aurora4xAutomation.Messages;

namespace Aurora4xAutomation.Command
{
    [Obsolete("Commands and related classes should be discontinued in favor of Evaluators, and in the case of duplicated functionality using compound evaluators.")]
    public static class MessageCommands
    {
        public static void PrintError(string message)
        {
            MessageManagerManager.AddMessage(message);
            SettingsCommands.Stop();
        }

        public static void PrintInterrupt(string message)
        {
            MessageManagerManager.AddMessage(message);
            SettingsCommands.Stop();
        }

        public static void PrintFeedback(string message)
        {
            MessageManagerManager.AddMessage(message);
        }
    }
}