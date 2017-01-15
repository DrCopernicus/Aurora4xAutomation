using System;

namespace Server.Common.Exceptions
{
    public class CommandExecutionException : Exception
    {
        public CommandExecutionException(int expected, int got, string name) : base(string.Format("Expected {0} parameters, got {1} in function name {2}.", expected, got, name))
        {
        }
    }
}
