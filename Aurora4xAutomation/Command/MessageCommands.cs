using System;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Command
{
    [Obsolete("Commands and related classes should be discontinued in favor of Evaluators, and in the case of duplicated functionality using compound evaluators.")]
    public class MessageCommands
    {
        public MessageCommands(SettingsStore settings)
        {
            Settings = settings;
        }

        public void PrintError(string message)
        {
            MessageManagerManager.AddMessage(message);
            new StopEvaluator("stop", Settings).Execute();
        }

        public void PrintInterrupt(string message)
        {
            MessageManagerManager.AddMessage(message);
            new StopEvaluator("stop", Settings).Execute();;
        }

        public void PrintFeedback(string message)
        {
            MessageManagerManager.AddMessage(message);
        }

        private SettingsStore Settings { get; set; }
    }
}