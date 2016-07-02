using System.Collections.Generic;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.Command.Evaluators
{
    public enum CommandEvaluatorType
    {
        Action,
        Parameter,
        Timer,
        Help
    }

    public abstract class Evaluator
    {
        protected Evaluator(string text)
        {
            Text = text;
            Body = null;
            Next = null;
        }

        protected Evaluator(string text, params string[] parameters)
            : this(text)
        {
            Body = new ParameterEvaluator(parameters[0], parameters.Subset(1));
        }

        protected Evaluator(params string[] parameters)
            : this("default", parameters)
        {

        }

        public string Text { get; private set; }
        public Evaluator Body { get; set; }
        public Evaluator Next { get; set; }

        public virtual CommandEvaluatorType GetEvaluatorType()
        {
            return CommandEvaluatorType.Action;
        }

        protected abstract void Evaluate();

        public void Execute()
        {
            Evaluate();
            if (Next != null)
                Next.Execute();
        }

        public abstract string Help { get; }

        private List<string> _parameters;

        protected List<string> Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    _parameters = new List<string>();
                    if (Body == null)
                        return _parameters;

                    var param = Body;
                    _parameters.Add(param.Text);
                    while ((param = param.Next) != null)
                        _parameters.Add(param.Text);
                }
                return _parameters;
            }
        }

        private List<Evaluator> _statementList;

        protected List<Evaluator> StatementList
        {
            get
            {
                if (_statementList == null)
                {
                    _statementList = new List<Evaluator>();
                    if (Body == null)
                        return _statementList;

                    var statement = Body;
                    _statementList.Add(statement);
                    while ((statement = statement.Next) != null)
                        _statementList.Add(statement);
                }
                return _statementList;
            }
        }
    }
}
