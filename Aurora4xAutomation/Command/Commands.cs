using System.Threading;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command
{
    public class Commands
    {
        public ResearchCommands ResearchCommands { get { return _researchCommands; } }
        private readonly ResearchCommands _researchCommands = new ResearchCommands();

        public OpenCommands OpenCommands { get { return _openCommands; } }
        private readonly OpenCommands _openCommands = new OpenCommands();
    }

}