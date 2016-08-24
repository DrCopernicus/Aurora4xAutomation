using System.Text.RegularExpressions;
using Aurora4xAutomation.Automation;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.Evaluators
{
    public class TimerEvaluator : UIEvaluator
    {
        public TimerEvaluator(string text, IUIMap uiMap)
            : base(text, uiMap)
        {
        }

        public override CommandEvaluatorType GetEvaluatorType()
        {
            return CommandEvaluatorType.Timer;
        }

        protected override void Evaluate()
        {
            foreach (var statement in StatementList)
            {
                var time = new Time(UIMap.SystemMap.GetTime()) + TimeFromText;
                CommandFlowManager.QueueCommand(statement, time);
            }
        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }

        private Time TimeFromText
        {
            get
            {
                var matches = Regex.Match(Text, "((?<Years>[0-9]+)y)?((?<Months>[0-9]+)m)?((?<Days>[0-9]+)d)?((?<Hours>[0-9]+)h)?");
                return new Time(matches.Groups["Years"].Value == "" ? 0 : int.Parse(matches.Groups["Years"].Value),
                    matches.Groups["Months"].Value == "" ? 0 : int.Parse(matches.Groups["Months"].Value),
                    matches.Groups["Days"].Value == "" ? 0 : int.Parse(matches.Groups["Days"].Value),
                    matches.Groups["Hours"].Value == "" ? 0 : int.Parse(matches.Groups["Hours"].Value),
                    0,
                    0);
            }
        }
    }
}
