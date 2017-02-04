using System;
using System.Linq;

namespace Server.Evaluators.Helpers
{
    public class HelpTextExample
    {
        public string Invokation { get; set; }
        public string Effect { get; set; }

        public HelpTextExample(string commandName, params string[] args)
        {
            if (args.Length == 0)
                throw new Exception("Must specify a help text example with at least a command name and a description of the effect!");

            var parameters = args.Take(args.Length - 1).ToArray();
            var effect = args.Last();

            Invokation = ParseInvokation(commandName, parameters);
            Effect = ParseEffect(parameters, effect);
        }

        private string ParseInvokation(string commandName, object[] parameters)
        {
            var s = commandName;
            foreach (var parameter in parameters)
            {
                s += string.Format(" {0}", parameter);
            }
            return s;
        }

        private string ParseEffect(object[] parameters, string effect)
        {
            return string.Format(effect, parameters);
        }
    }
}
