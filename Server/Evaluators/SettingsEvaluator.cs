using Server.Settings;

namespace Server.Evaluators
{
    public abstract class SettingsEvaluator : Evaluator
    {
        public SettingsEvaluator(string text, ISettingsStore settings)
            : base(text)
        {
            Settings = settings;
        }

        protected ISettingsStore Settings { get; set; }
    }
}
