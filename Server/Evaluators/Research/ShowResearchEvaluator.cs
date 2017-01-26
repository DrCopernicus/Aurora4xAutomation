using Server.Evaluators.Helpers;
using Server.IO;
using Server.Messages;
using System;
using System.Linq;

namespace Server.Evaluators.Research
{
    public class ShowResearchEvaluator : UIEvaluator
    {
        private IMessageManager Messages { get; set; }

        public ShowResearchEvaluator(string text, IUIMap uiMap, IMessageManager messages) : base(text, uiMap)
        {
            Messages = messages;
        }

        private static string[] _categories =
        {
            "bg", "cp", "ds", "ew", "lg", "mk", "pp", "sf"
        };

        private string ReadResearchTables()
        {
            var output = "";
            UIMap.PopulationAndProduction.MatchingScientistsOnly.Select();
            output += UIMap.PopulationAndProduction.ResearchTable.GetText();
            output += UIMap.PopulationAndProduction.AvailableScientistsTable.GetText();
            return output;
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameter, got {0} in function name {1}.",
                    Parameters.Count, Text));

            ParameterTextParser.ValidateStringAgainstSet(Parameters[0], new[] { "all" }.Concat(_categories).ToArray());

            var output = "";

            UIMap.PopulationAndProduction.MakeActive();
            UIMap.PopulationAndProduction.SelectResearchTab();

            output += "Available Labs: " + UIMap.PopulationAndProduction.AvailableLabs.Text + "\n\n";

            if (Parameters[0] == "all")
            {
                foreach (var cat in _categories)
                {
                    UIMap.PopulationAndProduction.SelectResearchByCategory(cat);
                    output += ReadResearchTables();
                }
            }
            else
            {
                UIMap.PopulationAndProduction.SelectResearchByCategory(Parameters[0]);
                output += ReadResearchTables();
            }

            Messages.AddMessage(MessageType.Information, output);
        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}