using System;

namespace Server.Common.Exceptions
{
    public class CommandInvalidParameterException : Exception
    {
        public CommandInvalidParameterException(int parameterNumber, string message)
            : base(string.Format("Parameter {0} invalid: {1}", parameterNumber, message))
        {
        }
    }
}
