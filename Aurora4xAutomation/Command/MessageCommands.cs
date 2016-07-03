using System;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Command
{
    public static class MessageCommands
    {
        public static void PrintError(string message)
        {
            SettingsStore.ErrorMessage += string.Format("\n\n{0}", message);
            SettingsCommands.Stop();
        }

        public static void PrintInterrupt(string message)
        {
            SettingsStore.InterruptMessage += string.Format("\n\n{0}", message);
            SettingsCommands.Stop();
        }

        public static void PrintFeedback(string message)
        {
            SettingsStore.FeedbackMessage += string.Format("\n\n{0}", message);
        }
    }
}