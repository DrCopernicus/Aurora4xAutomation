﻿using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using Server.Evaluators.Helpers;
using Server.Evaluators.Message;
using Server.Messages;

namespace Tests.Tests.EvaluatorTests.Message
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

        [Test]
        public void HasHelpText()
        {
            Assert.DoesNotThrow(() =>
            {
                var x = new PrintEvaluator("", Substitute.For<IMessageManager>()).Help;
            });
        }
    }
}
