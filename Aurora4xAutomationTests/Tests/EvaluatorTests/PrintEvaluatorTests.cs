using System;
using System.Collections.Generic;
using Aurora4xAutomation.Evaluators.Factories;
using Aurora4xAutomation.Evaluators.Message;
using Aurora4xAutomation.Messages;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.EvaluatorTests
{
    [TestFixture]
    public class PrintEvaluatorTests
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
        public void WritesMessageToMessageManager()
        {
            var messages = new MessageManagerDouble();
            var evaluator = new PrintEvaluator("print", messages);
            new EvaluatorParameterizer().SetParameters(evaluator, "parameter");

            evaluator.Execute();

            Assert.AreEqual(1, messages.GetMessagesAfterId(-1, 100).Count);
            Assert.AreEqual("parameter", messages.GetMessagesAfterId(-1, 100)[0]);
        }

    }
}
