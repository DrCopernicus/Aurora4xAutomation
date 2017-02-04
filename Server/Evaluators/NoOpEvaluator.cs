using Server.Evaluators.Helpers;

namespace Server.Evaluators
{
    public class NoOpEvaluator : Evaluator
    {
        public NoOpEvaluator(string text)
            : base(text)
        {
        }
        
        protected override void Evaluate()
        {

        }

        public override HelpText Help
        {
            get { return new HelpText("noop", "")
                .AddRow("Does nothing."); }
        }
    }
}
