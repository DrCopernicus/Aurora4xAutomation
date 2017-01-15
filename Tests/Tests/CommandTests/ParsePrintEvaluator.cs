using System;
using NSubstitute;
using NUnit.Framework;
using Server.Command.Parser;
using Server.Events;
using Server.IO;
using Server.Messages;
using Server.Settings;

namespace Tests.Tests.CommandTests
{
    [TestFixture]
    public class ParsePrintEvaluator
    {
        [Test]
        public void NoParameterPrintThrowsWhenExecuted()
        {
            var lexer = new CommandLexer(Substitute.For<IUIMap>(), Substitute.For<ISettingsStore>(), Substitute.For<IMessageManager>(), Substitute.For<IEventManager>());

            var evaluator = lexer.Lex("print");

            Assert.Throws<Exception>(() => evaluator.Execute());
        }

        [Test]
        public void SingleParameterPrintAddsMessageToMessageManager()
        {
            var messages = Substitute.For<IMessageManager>();
            var lexer = new CommandLexer(Substitute.For<IUIMap>(), Substitute.For<ISettingsStore>(), messages, Substitute.For<IEventManager>());

            var evaluator = lexer.Lex("print abcd");

            Assert.DoesNotThrow(() => evaluator.Execute());
            messages.Received(1).AddMessage(MessageType.Information, "abcd");
        }

        [Test]
        public void MoreThanOneParamterPrintThrowsWhenExecuted()
        {
            var messages = Substitute.For<IMessageManager>();
            var lexer = new CommandLexer(Substitute.For<IUIMap>(), Substitute.For<ISettingsStore>(), messages, Substitute.For<IEventManager>());

            var evaluator = lexer.Lex("print abcd efg hijiijl kljlaisjdknmv rg");

            Assert.Throws<Exception>(() => evaluator.Execute());
        }
    }
}
