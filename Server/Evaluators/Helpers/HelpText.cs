using System.Collections.Generic;

namespace Server.Evaluators.Helpers
{
    public class HelpText
    {
        public HelpText(string commandName, string description)
        {
            CommandName = commandName;
            Description = description;
            Examples = new List<HelpTextExample>();
        }

        public string CommandName { get; set; }
        public string Description { get; set; }
        public List<HelpTextExample> Examples { get; set; }

        public void AddRow(params string[] args)
        {
            Examples.Add(new HelpTextExample(CommandName, args));
        }
    }
}
