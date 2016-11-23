namespace Aurora4xAutomation.Evaluators
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

        public override string Help
        {
            get { return "noop: Does nothing."; }
        }
    }
}
