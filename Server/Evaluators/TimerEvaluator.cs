﻿using System;
using Server.Evaluators.Helpers;
using Server.Events;
using Server.IO;
using System.Text.RegularExpressions;

namespace Server.Evaluators
{
    public class TimerEvaluator : Evaluator
    {
        public TimerEvaluator(string text, IUIMap uiMap, IEventManager eventManager)
            : base(text)
        {
            UIMap = uiMap;
            EventManager = eventManager;
        }

        public IUIMap UIMap { get; set; }
        public IEventManager EventManager { get; set; }

        public override CommandEvaluatorType GetEvaluatorType()
        {
            return CommandEvaluatorType.Timer;
        }

        protected override void Evaluate()
        {
            foreach (var statement in StatementList)
            {
                var time = UIMap.GetTime() + TimeFromText;
                EventManager.AddEvent(statement, time);
            }
        }

        public override HelpText Help
        {
            get { throw new NotImplementedException(); }
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
