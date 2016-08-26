using Aurora4xAutomation.Messages;
using System;
using System.ComponentModel;

namespace Aurora4xAutomation.Common.Converters
{
    public static class MessageTypeConverter
    {
        public static string ToString(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Debug:
                    return "DBUG";
                case MessageType.Error:
                    return "CRIT";
                case MessageType.Information:
                    return "INFO";
                case MessageType.Warning:
                    return "WARN";
            }
            throw new InvalidEnumArgumentException("messageType", (int)messageType, typeof(MessageType));
        }

        public static MessageType ToType(string messageType)
        {
            switch (messageType.ToLower())
            {
                case "dbug":
                case "debug":
                    return MessageType.Debug;
                case "crit":
                case "critical":
                case "error":
                    return MessageType.Error;
                case "info":
                case "information":
                    return MessageType.Information;
                case "warn":
                case "warning":
                    return MessageType.Warning;
            }
            throw new ArgumentException(string.Format("Could not handle converting <{0}> to a MessageType.", messageType));
        }
    }
}
