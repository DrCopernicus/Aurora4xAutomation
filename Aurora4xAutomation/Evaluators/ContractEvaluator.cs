using Aurora4xAutomation.Command;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Evaluators.Factories;
using Aurora4xAutomation.IO;
using System;

namespace Aurora4xAutomation.Evaluators
{
    public class ContractEvaluator : UIEvaluator
    {
        public ContractEvaluator(string text, IUIMap uiMap)
            : base(text, uiMap)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 4)
                throw new Exception(string.Format("Expected 4 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            new OpenCommands(UIMap).SelectColony(Parameters[0]);
            UIMap.PopulationAndProduction.MakeActive();
            UIMap.PopulationAndProduction.SelectCivilianTab();
            UIMap.PopulationAndProduction.InstallationType.Text = Parameters[1];
            UIMap.PopulationAndProduction.ContractAmount.Text = Parameters[2];
            if (IsSupplyContract(Parameters[3]))
                UIMap.PopulationAndProduction.CivilianContractSupply.Selected = true;
            else
                UIMap.PopulationAndProduction.CivilianContractDemand.Selected = true;
            UIMap.PopulationAndProduction.AddCivilianContract.Click();
        }

        private static bool IsSupplyContract(string parameter)
        {
            bool isSupply;
            if (bool.TryParse(parameter, out isSupply))
                return isSupply;
            
            if (parameter != "s" && parameter != "d" && parameter != "supply" && parameter != "demand")
                throw new CommandInvalidParameterException(4, "Expected one of the following: s(upply), d(emand), true, false.");

            return parameter == "s" || parameter == "supply";
        }

        public static ContractEvaluator SupplyContract(IUIMap uiMap, string population, string installation, int amount, bool supply)
        {
            var evaluator = new ContractEvaluator("contract", uiMap);
            new EvaluatorParameterizer().SetParameters(evaluator, population, installation, amount, supply);
            return evaluator;
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
