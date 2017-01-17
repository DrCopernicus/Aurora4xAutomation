using Server.Evaluators.Helpers;
using Server.IO;
using Server.Messages;
using System;

namespace Server.Evaluators.Research
{
    public class StartResearchEvaluator : UIEvaluator
    {
        private IMessageManager Messages { get; set; }

        public StartResearchEvaluator(string text, IUIMap uiMap, IMessageManager messages)
            : base(text, uiMap)
        {
            Messages = messages;
        }

        private static string[] _categories =
        {
            "bg", "cp", "ds", "ew", "lg", "mk", "pp", "sf"
        };

        protected override void Evaluate()
        {
            if (Parameters.Count != 4)
                throw new Exception(string.Format("Expected 4 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            ParameterTextParser.ValidateStringAgainstSet(Parameters[0], _categories);
            var technologyNumber = ParameterTextParser.ReadInt(Parameters[1]);
            var scientistNumber = ParameterTextParser.ReadInt(Parameters[2]);
            var numberOfLabs = ParameterTextParser.ReadPositiveInt(Parameters[3]);

            UIMap.PopulationAndProduction.MakeActive();
            UIMap.PopulationAndProduction.SelectResearchTab();
            UIMap.PopulationAndProduction.SelectResearchByCategory(Parameters[0]);

            UIMap.PopulationAndProduction.MatchingScientistsOnly.Select();
            UIMap.PopulationAndProduction.SelectNthResearch(technologyNumber);
            UIMap.PopulationAndProduction.SelectNthScientist(scientistNumber);

            UIMap.PopulationAndProduction.SetAllocatedLabs(numberOfLabs + "");
            UIMap.PopulationAndProduction.CreateResearch();
        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}