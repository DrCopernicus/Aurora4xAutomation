using System.Collections.Generic;

namespace Aurora4xAutomation.Command.Parser
{
    public enum CommandEvaluatorType
    {
        Action,
        Parameter,
        Timer,
        Help
    }

    public abstract class CommandEvaluator
    {
        public CommandEvaluator(string text, CommandEvaluatorType type)
        {
            Text = text;
            Type = type;
            Body = null;
            Next = null;
        }

        public string Text { get; private set; }
        public CommandEvaluatorType Type { get; private set; }
        public CommandEvaluator Body { get; set; }
        public CommandEvaluator Next { get; set; }

        public abstract void Evaluate();

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

        private List<CommandEvaluator> _statementList;

        protected List<CommandEvaluator> StatementList
        {
            get
            {
                if (_statementList == null)
                {
                    _statementList = new List<CommandEvaluator>();
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
