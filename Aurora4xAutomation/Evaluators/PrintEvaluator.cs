using System;
using Aurora4xAutomation.Command;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Evaluators
{
    public class PrintEvaluator : SettingsEvaluator
    {
        public PrintEvaluator(string text, SettingsStore settings)
            : base(text, settings)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameter, got {0} in function name {1}.",
                    Parameters.Count, Text));

            new MessageCommands(Settings).PrintFeedback(Parameters[0]);
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
