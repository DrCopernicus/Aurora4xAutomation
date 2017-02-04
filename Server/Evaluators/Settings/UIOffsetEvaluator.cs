using Server.Evaluators.Helpers;
using Server.Settings;
using System;

namespace Server.Evaluators.Settings
{
    public class UIOffsetEvaluator : SettingsEvaluator
    {
        public UIOffsetEvaluator(string text, ISettingsStore settings) : base(text, settings)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 2)
                throw new Exception(string.Format("Expected 2 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            var vertical = ParameterTextParser.ReadBoolean(Parameters[0], new []{"vertical", "v"}, new []{"horizontal", "h"});
            var offset = ParameterTextParser.ReadInt(Parameters[1]);

            if (vertical)
                Settings.VerticalWindowOffset = offset;
            else
                Settings.HorizontalWindowOffset = offset;
        }

        public override HelpText Help
        {
            get { return new HelpText("set-offset", "")
                .AddRow("<direction>", "<amount>", "Sets the window offset in {0} to {1}. Useful for when controls are not being found correctly."); }
        }
    }
}