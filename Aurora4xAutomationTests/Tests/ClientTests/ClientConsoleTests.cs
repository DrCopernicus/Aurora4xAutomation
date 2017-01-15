using Aurora4xAutomationClient.ClientUI;
using Aurora4xAutomationClient.ClientUI.Terminal;
using Aurora4xAutomationClient.Common.EArgs;
using NSubstitute;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.ClientTests
{
    [TestFixture]
    public class ClientConsoleTests
    {
        [Test]
        public void WritesCharacterToTerminalsCurrentLine()
        {
            var terminal = Substitute.For<ITerminal>();
            var writer = Substitute.For<IConsoleWriter>();
            var console = new ClientConsole(terminal, writer);
            console.WriteToCurrentLine(this, new TerminalEventArgs { Message = "a" });

            terminal.Received(1).AppendToCurrentLine("a");
        }

        [Test]
        public void WritesStringToTerminalsBuffer()
        {
            var terminal = Substitute.For<ITerminal>();
            var writer = Substitute.For<IConsoleWriter>();
            var console = new ClientConsole(terminal, writer);
            console.WriteToBuffer(this, new TerminalEventArgs { Message = "a" });

            terminal.Received(1).WriteLine("a", Arg.Any<TerminalColor>());
        }

        [Test]
        public void WritesBackspaceToCurrentLine()
        {
            var terminal = Substitute.For<ITerminal>();
            var writer = Substitute.For<IConsoleWriter>();
            var console = new ClientConsole(terminal, writer);
            console.WriteToCurrentLine(this, new TerminalEventArgs { Message = "\b" });

            terminal.Received(1).Backspace();
        }

        [Test]
        public void WritesLongStringToCurrentLine()
        {
            var terminal = Substitute.For<ITerminal>();
            var writer = Substitute.For<IConsoleWriter>();
            var console = new ClientConsole(terminal, writer);
            console.WriteToCurrentLine(this, new TerminalEventArgs { Message = "hello" });

            terminal.Received(5).AppendToCurrentLine(Arg.Any<string>());
        }

        [Test]
        public void WritesLongStringToCurrentLineWithBackspaces()
        {
            var terminal = Substitute.For<ITerminal>();
            var writer = Substitute.For<IConsoleWriter>();
            var console = new ClientConsole(terminal, writer);
            console.WriteToCurrentLine(this, new TerminalEventArgs { Message = "ha\belloo\b" });

            terminal.Received(7).AppendToCurrentLine(Arg.Any<string>());
            terminal.Received(2).Backspace();
        }

    }
}
