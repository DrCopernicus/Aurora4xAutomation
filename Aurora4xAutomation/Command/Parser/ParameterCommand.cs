namespace Aurora4xAutomation.Command.Parser
{
    public class ParameterCommand : CommandEvaluator
    {
        public ParameterCommand(string text, CommandEvaluatorType type)
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
