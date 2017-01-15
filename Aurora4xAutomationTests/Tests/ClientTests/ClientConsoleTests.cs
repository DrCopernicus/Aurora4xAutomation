using Aurora4xAutomationClient.ClientUI;
using Aurora4xAutomationClient.ClientUI.Client;
using Aurora4xAutomationClient.ClientUI.Terminal;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace Aurora4xAutomationTests.Tests.ClientTests
{
    [TestFixture]
    public class ClientConsoleTests
    {
        [Test]
        public void WritesCharacterToTerminalsCurrentLine()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var writer = Substitute.For<IConsoleWriter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, writer, client);
            console.WriteToCurrentLine("a");

            terminal.Received(1).AppendToCurrentLine('a');
        }

        [Test]
        public void WritesStringToTerminalsBuffer()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var writer = Substitute.For<IConsoleWriter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, writer, client);
            console.WriteToBuffer("a");

            terminal.Received(1).WriteLine("a", Arg.Any<TerminalColor>());
        }

        [Test]
        public void WritesBackspaceToCurrentLine()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var writer = Substitute.For<IConsoleWriter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, writer, client);
            console.WriteToCurrentLine("\b");

            terminal.Received(1).Backspace();
        }

        [Test]
        public void WritesLongStringToCurrentLine()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var writer = Substitute.For<IConsoleWriter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, writer, client);
            console.WriteToCurrentLine("hello");

            terminal.Received(5).AppendToCurrentLine(Arg.Any<char>());
        }

        [Test]
        public void WritesLongStringToCurrentLineWithBackspaces()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var writer = Substitute.For<IConsoleWriter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, writer, client);
            console.WriteToCurrentLine("ha\belloo\b");

            terminal.Received(7).AppendToCurrentLine(Arg.Any<char>());
            terminal.Received(2).Backspace();
        }

        [Test]
        public void PushesCurrentLineIntoBufferOnNewline()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var writer = Substitute.For<IConsoleWriter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, writer, client);
            console.WriteToCurrentLine("\n");

            terminal.Received(1).WriteCurrentLine();
        }

        [Test]
        public void DoesNotModifyTerminalOutputWhenNormalCharactersTyped()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var writer = Substitute.For<IConsoleWriter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, writer, client);

            writer.ClearReceivedCalls();

            console.WriteToCurrentLine("h");

            writer.Received(0).Write(Arg.Any<string>());
            writer.Received(0).WriteLine(Arg.Any<string>());
        }

        [Test]
        public void ModifiesTerminalOutputWhenBackspaceIsPressed()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var writer = Substitute.For<IConsoleWriter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, writer, client);
            console.WriteToCurrentLine("h\b");

            writer.Received(1).Write(" \b");
        }
    }
}
