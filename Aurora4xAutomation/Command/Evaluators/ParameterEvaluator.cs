using Aurora4xAutomation.Command.Parser;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class ParameterEvaluator : Evaluator
    {
        public ParameterEvaluator(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        public override void Evaluate()
        {

        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
