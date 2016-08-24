using Aurora4xAutomation.Messages;

namespace Aurora4xAutomation.Command
{
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