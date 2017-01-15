using Aurora4xAutomationClient.ClientUI.Terminal;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Aurora4xAutomationTests.Tests.ClientTests
{
    [TestFixture]
    public class ConsoleFormatterTests
    {
        [Test]
        public void WritesFlairInFrontOfInputtedCommand()
        {
            var writer = Substitute.For<IConsoleWriter>();
            var formatter = new ConsoleFormatter(writer);

            formatter.WriteLine(new TerminalMessage("print hello", TerminalStyle.Command));
            writer.Received(1).WriteLine(" > print hello");
        }

        [Test]
        public void DefaultFormatDoesNotChangeString()
        {
            var writer = Substitute.For<IConsoleWriter>();
            var formatter = new ConsoleFormatter(writer);

            formatter.WriteLine(new TerminalMessage("this is a normal message", TerminalStyle.Default));
            writer.Received(1).WriteLine("this is a normal message");
        }

        [Test]
        public void ChangesColorOnError()
        {
            var writer = Substitute.For<IConsoleWriter>();
            var formatter = new ConsoleFormatter(writer);

            formatter.WriteLine(new TerminalMessage("[ERROR] Broke the everything", TerminalStyle.Error));
            writer.Received(1).WriteLine("[ERROR] Broke the everything");
            writer.Received(1).ChangeColor(ConsoleColor.Red);
            writer.Received(1).ChangeColor(ConsoleColor.White);
        }
    }
}
