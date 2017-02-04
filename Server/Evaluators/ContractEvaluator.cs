using Server.Common.Exceptions;
using Server.Evaluators.Helpers;
using Server.IO;
using System;

namespace Server.Evaluators
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

            UIMap.PopulationAndProduction.MakeActive();
            UIMap.PopulationAndProduction.Populations.Select(Parameters[0]);
            UIMap.PopulationAndProduction.MakeActive();
            UIMap.PopulationAndProduction.SelectCivilianTab();
            UIMap.PopulationAndProduction.ContractAmount.Text = Parameters[1];
            UIMap.PopulationAndProduction.InstallationType.Text = Parameters[2];
            if (IsSupplyContract(Parameters[3]))
                UIMap.PopulationAndProduction.CivilianContractSupply.Select();
            else
                UIMap.PopulationAndProduction.CivilianContractDemand.Select();
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

        public static ContractEvaluator SupplyContract(IUIMap uiMap, string population, string installation, int amount)
        {
            var evaluator = new ContractEvaluator("contract", uiMap);
            new EvaluatorParameterizer().SetParameters(evaluator, population, amount, installation, true);
            return evaluator;
        }

        public static ContractEvaluator DemandContract(IUIMap uiMap, string population, string installation, int amount)
        {
            var evaluator = new ContractEvaluator("contract", uiMap);
            new EvaluatorParameterizer().SetParameters(evaluator, population, amount, installation, false);
            return evaluator;
        }

        public override string Help
        {
            get { return "contract <colony> <amount> <installation> (s|d|supply|demand|true|false): Create a supply or demand contract on <colony> for <amount> units of <installation>."; }
        }
    }
}
