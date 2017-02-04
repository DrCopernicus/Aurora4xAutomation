using System.Collections.Generic;
using System.Linq;

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

        public HelpText AddRow(params string[] args)
        {
            Examples.Add(new HelpTextExample(CommandName, args));
            return this;
        }

        public string ToFormattedString()
        {
            return string.Format("{0}\n\n{1}\n\n{2}", CommandName, Description, string.Join("\n", Examples.Select(example => string.Format("{0}: {1}", example.Invokation, example.Effect))));
        }
    }
}
