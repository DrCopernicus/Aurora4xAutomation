using System;
using System.Linq;

namespace Server.Evaluators.Helpers
{
    public static class ParameterTextParser
    {
        private static readonly string[] DefaultTrueValues = { "on", "true", "yes" };
        private static readonly string[] DefaultFalseValues = { "off", "false", "no" };

        public static bool ReadBoolean(string actual, string[] trueValues = null, string[] falseValues = null)
        {
            if (trueValues == null)
                trueValues = DefaultTrueValues;
            if (falseValues == null)
                falseValues = DefaultFalseValues;

            if (trueValues.Contains(actual))
                return true;
            if (falseValues.Contains(actual))
                return false;

            throw new Exception(string.Format("Unexpected parameter: <{0}> not valid! Must be one of: {1}.", actual, string.Join(", ", trueValues.Concat(falseValues))));
        }

        public static void ValidateStringAgainstSet(string actual, string[] allowableStrings)
        {
            if (!allowableStrings.Contains(actual))
                throw new Exception(string.Format("Unexpected parameter: <{0}> not valid! Must be one of: {1}.", actual, string.Join(", ", allowableStrings)));
        }

        public static int ReadInt(string actual)
        {
            try
            {
                return int.Parse(actual);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Unexpected parameter: parsing <{0}> threw an exception! {1}\n{2}.", actual, e.Message, e.StackTrace));
            }
        }

        public static int ReadPositiveInt(string actual)
        {
            try
            {
                var parsed = ReadInt(actual);
                if (parsed <= 0)
                    throw new Exception(string.Format("Unexpected parameter: <{0}> not valid! Must be a positive integer (greater than zero).", actual));
                return parsed;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Unexpected parameter: parsing <{0}> threw an exception! {1}\n{2}.", actual, e.Message, e.StackTrace));
            }
        }
    }
}