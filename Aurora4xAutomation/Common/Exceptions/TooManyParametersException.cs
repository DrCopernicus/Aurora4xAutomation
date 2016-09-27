using System;

namespace Aurora4xAutomation.Common.Exceptions
{
    public class TooManyParametersException : Exception
    {
        public TooManyParametersException(int expectedParameterCount, int actualParameterCount, string functionName)
            : base(string.Format("Expected {0} parameters, got {1} in function name {2}.", expectedParameterCount, actualParameterCount, functionName))
        {
        }
    }
}
