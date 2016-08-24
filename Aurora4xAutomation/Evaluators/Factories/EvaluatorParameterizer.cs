using System.Linq;

namespace Aurora4xAutomation.Evaluators.Factories
{
    public class EvaluatorParameterizer
    {
        public void SetParameters(IEvaluator evaluator, params string[] parameters)
        {
            var parameterEvaluatorHead = MakeParameterEvaluator(parameters);
            evaluator.Body = parameterEvaluatorHead;
        }

        public void SetParameters(IEvaluator evaluator, params object[] parameters)
        {
            var parameterEvaluatorHead = MakeParameterEvaluator(parameters.Select(parameter => parameter.ToString()).ToArray());
            evaluator.Body = parameterEvaluatorHead;
        }

        private ParameterEvaluator MakeParameterEvaluator(string[] parameters)
        {
            var parameterEvaluator = new ParameterEvaluator(parameters[0]);

            IEvaluator head = parameterEvaluator;
            foreach (var remainingParameter in parameters.Skip(1))
            {
                head.Next = new ParameterEvaluator(remainingParameter);
                head = head.Next;
            }

            return parameterEvaluator;
        }
    }
}
