using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Evaluators.Message;
using Aurora4xAutomation.Messages;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Aurora4xAutomationTests.Tests.EvaluatorTests
{
    [TestFixture]
    public class HelpEvaluatorTests
    {
        private class TestMessageManager : IMessageManager
        {
            private List<string> _messages = new List<string>();

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

        private class TestEvaluator : IEvaluator
        {
            public void Execute()
            {
                throw new NotImplementedException();
            }

            public string Help { get { return "help text"; } }
            public string Text { get; private set; }
            public IEvaluator Body { get; set; }
            public IEvaluator Next { get; set; }
            public CommandEvaluatorType GetEvaluatorType()
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void WritesHelpMessageForTestEvaluator()
        {
            var messages = new TestMessageManager();
            var helpEvaluator = new HelpEvaluator("", messages);
            helpEvaluator.Body = new TestEvaluator();

            helpEvaluator.Execute();

            Assert.Contains("help text", messages.GetMessagesAfterId(-1,100));
        }
    }
}
