using Aurora4xAutomation.Evaluators.Factories;
using Aurora4xAutomation.Evaluators.Message;
using Aurora4xAutomation.Messages;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Aurora4xAutomation.Tests.EvaluatorTests
{
    [TestFixture]
    public class LogEvaluatorTests
    {
        private class MessageManagerDouble : IMessageManager
        {
            private readonly List<string> _messages = new List<string>();

            public List<string> GetMessagesAfterId(long start, long end)
            {
                return _messages;
            }

            public void AddMessage(MessageType type, string message)
            {
                _messages.Add(message);
            }

            public long GetLastId()
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void WritesDebugMessages()
        {
            var messages = new MessageManagerDouble();
            var log = new LogEvaluator("log", messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Debug, "debug message");

            log.Execute();

            Assert.AreEqual(1, messages.GetMessagesAfterId(-1, 100).Count);
        }

        [Test]
        public void WritesInfoMessages()
        {
            var messages = new MessageManagerDouble();
            var log = new LogEvaluator("log", messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Information, "information message");

            log.Execute();

            Assert.AreEqual(1, messages.GetMessagesAfterId(-1, 100).Count);
        }

        [Test]
        public void WritesWarningMessages()
        {
            var messages = new MessageManagerDouble();
            var log = new LogEvaluator("log", messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Warning, "warning message");

            log.Execute();

            Assert.AreEqual(1, messages.GetMessagesAfterId(-1, 100).Count);
        }

        [Test]
        public void WritesErrorMessages()
        {
            var messages = new MessageManagerDouble();
            var log = new LogEvaluator("log", messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Error, "error message");

            log.Execute();

            Assert.AreEqual(1, messages.GetMessagesAfterId(-1, 100).Count);
        }
    }
}
