using NSubstitute;
using NUnit.Framework;
using Server.Evaluators;
using Server.Evaluators.Helpers;
using Server.Evaluators.Message;
using Server.Messages;
using System;

namespace Tests.Tests.EvaluatorTests
{
    [TestFixture]
    public class HelpEvaluatorTests
    {
        [Test]
        public void WritesHelpMessage()
        {
            var messages = Substitute.For<IMessageManager>();
            var helpEvaluator = new HelpEvaluator("", messages);
            var evaluatorWithHelpMessage = Substitute.For<IEvaluator>();
            evaluatorWithHelpMessage.Help.Returns(new HelpText("", ""));
            evaluatorWithHelpMessage.Body.Returns(a => null);
            evaluatorWithHelpMessage.Next.Returns(a => null);
            helpEvaluator.Body = evaluatorWithHelpMessage;

            helpEvaluator.Execute();

            messages.Received(1).AddMessage(MessageType.Information, Arg.Any<string>());
        }

        [Test]
        public void MoreThanOneParameterFails()
        {
            var messages = Substitute.For<IMessageManager>();
            var helpEvaluator = new HelpEvaluator("", messages);
            var firstParameter = Substitute.For<IEvaluator>();
            var secondParameter = Substitute.For<IEvaluator>();
            firstParameter.Body.Returns(a => null);
            firstParameter.Next.Returns(a => secondParameter);
            secondParameter.Body.Returns(a => null);
            secondParameter.Next.Returns(a => null);

            helpEvaluator.Body = firstParameter;

            Assert.Throws<Exception>(() => helpEvaluator.Execute());
        }

        [Test]
        public void FewerThanOneParameterFails()
        {
            var messages = Substitute.For<IMessageManager>();
            var helpEvaluator = new HelpEvaluator("", messages);

            Assert.Throws<Exception>(() => helpEvaluator.Execute());
        }

        [Test]
        public void HasHelpText()
        {
            Assert.DoesNotThrow(() =>
            {
                var x = new HelpEvaluator("", Substitute.For<IMessageManager>()).Help;
            });
        }
    }
}
