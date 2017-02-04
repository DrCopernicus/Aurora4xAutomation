using System.Collections.Generic;
using Server.Evaluators.Helpers;

namespace Server.Evaluators
{
    public enum CommandEvaluatorType
    {
        Action,
        Parameter,
        Timer,
        Help
    }

    public abstract class Evaluator : IEvaluator
    {
        protected Evaluator(string text)
        {
            Text = text;
            Body = null;
            Next = null;
        }

        public string Text { get; private set; }
        public IEvaluator Body { get; set; }
        public IEvaluator Next { get; set; }

        public virtual CommandEvaluatorType GetEvaluatorType()
        {
            return CommandEvaluatorType.Action;
        }

        protected abstract void Evaluate();

        public virtual void Execute()
        {
            Evaluate();
            if (Next != null)
                Next.Execute();
        }

        public abstract HelpText Help { get; }

        private List<string> _parameters;

        public List<string> Parameters
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

        private List<IEvaluator> _statementList;

        protected List<IEvaluator> StatementList
        {
            get
            {
                if (_statementList == null)
                {
                    _statementList = new List<IEvaluator>();
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
