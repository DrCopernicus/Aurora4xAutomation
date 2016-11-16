using System;
using System.Collections.Generic;
using Aurora4xAutomation.Evaluators.Factories;
using Aurora4xAutomation.Evaluators.Message;
using Aurora4xAutomation.Messages;
using NSubstitute;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.EvaluatorTests
{
    [TestFixture]
    public class LogEvaluatorTests
    {
        [Test]
        public void WritesDebugMessages()
        {
            var messages = Substitute.For<IMessageManager>();
            var log = new LogEvaluator("log", messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Debug, "debug message");

            log.Execute();

            messages.Received(1).AddMessage(MessageType.Debug, "debug message");
        }

        [Test]
        public void WritesInfoMessages()
        {
            var messages = Substitute.For<IMessageManager>();
            var log = new LogEvaluator("log", messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Information, "information message");

            log.Execute();

            messages.Received(1).AddMessage(MessageType.Information, "information message");
        }

        [Test]
        public void WritesWarningMessages()
        {
            var messages = Substitute.For<IMessageManager>();
            var log = new LogEvaluator("log", messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Warning, "warning message");

            log.Execute();

            messages.Received(1).AddMessage(MessageType.Warning, "warning message");
        }

        [Test]
        public void WritesErrorMessages()
        {
            var messages = Substitute.For<IMessageManager>();
            var log = new LogEvaluator("log", messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Error, "error message");

            log.Execute();

            messages.Received(1).AddMessage(MessageType.Error, "error message");
        }
    }
}
