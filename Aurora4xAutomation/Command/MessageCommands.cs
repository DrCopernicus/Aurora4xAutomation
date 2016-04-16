using System;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command
{
    public static class MessageCommands
    {
        public static void PrintError(object sender, EventArgs e)
        {
            Settings.ErrorMessage += string.Format("\n\n{0}", ((MessageEventArgs)e).Message);
            Timeline.AddEvent(SettingsCommands.Stop);
        }

        public static void PrintInterrupt(object sender, EventArgs e)
        {
            Settings.InterruptMessage += string.Format("\n\n{0}", ((MessageEventArgs)e).Message);
            Timeline.AddEvent(SettingsCommands.Stop);
        }

        public static void PrintFeedback(object sender, EventArgs e)
        {
            Settings.FeedbackMessage += string.Format("\n\n{0}", ((MessageEventArgs)e).Message);
        }
    }
}