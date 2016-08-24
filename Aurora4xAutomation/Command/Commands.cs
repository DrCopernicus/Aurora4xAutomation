using System;

namespace Aurora4xAutomation.Command
{
    [Obsolete("Commands and related classes should be discontinued in favor of Evaluators, and in the case of duplicated functionality using compound evaluators.")]
    public class Commands
    {
        public ResearchCommands ResearchCommands { get { return _researchCommands; } }
        private readonly ResearchCommands _researchCommands = new ResearchCommands();

        public OpenCommands OpenCommands { get { return _openCommands; } }
        private readonly OpenCommands _openCommands = new OpenCommands();
    }

}