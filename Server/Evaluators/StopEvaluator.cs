using Server.Common.Exceptions;
using Server.Evaluators.Helpers;
using Server.Settings;

namespace Server.Evaluators
{
    public class StopEvaluator : SettingsEvaluator
    {
        public StopEvaluator(string text, ISettingsStore settings)
            : base(text, settings)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 0)
                throw new WrongParameterCountException(0, Parameters.Count, Text);

            Settings.Stopped = true;
        }

        public override HelpText Help
        {
            get { return new HelpText("stop", "")
                .AddRow("Stops the advancement of turns. Identical to \"adv stop\"."); }
        }
    }
}
