using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.IO.UI.Windows;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.Settings;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Aurora4xAutomationTests.Tests
{
    [TestFixture]
    public class LexerTests
    {
        private class EmptyUIMap : IUIMap
        {
            public BaseAuroraWindow BaseAuroraWindow { get; private set; }
            public EventWindow Events { get; private set; }
            public CommandersWindow Leaders { get; private set; }
            public SystemMapWindow SystemMap { get; private set; }
            public TaskGroupsWindow TaskGroups { get; private set; }
            public PopulationAndProductionWindow PopulationAndProduction { get; private set; }
            public Time GetTime()
            {
                throw new NotImplementedException();
            }
        }

        private class MessageManagerDouble : IMessageManager
        {
            public List<string> GetMessagesAfterId(long start, long end)
            {
                throw new NotImplementedException();
            }

            public void AddMessage(MessageType type, string message)
            {
                throw new NotImplementedException();
            }

            public long GetLastId()
            {
                throw new NotImplementedException();
            }
        }

        private class SettingsStoreDouble : ISettingsStore
        {
            public bool Stopped { get; set; }
            public bool AutoTurnsOn { get; set; }
            public string DatabaseLocation { get; private set; }
            public string DatabasePassword { get; private set; }
            public string EventLogLocation { get; private set; }
            public int RaceId { get; set; }
            public Dictionary<string, string> Research { get; set; }
            public Dictionary<string, Dictionary<string, string>> ResearchFocuses { get; private set; }
            public int GameId { get; set; }
            public IncrementLength Increment { get; set; }
        }

        private class EventManagerDouble : IEventManager
        {
            public void AddEvent(IEvaluator evaluator, Time time)
            {
                throw new NotImplementedException();
            }

            public void Begin(ILogger logger)
            {
                throw new NotImplementedException();
            }

            public void Stop()
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void TestNonsensicalInput()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStoreDouble(), new MessageManagerDouble(), new EventManagerDouble());
            Assert.Throws(typeof(Exception), () => lexer.Lex("awdh3ukhij 3lij 3lij5 la j3il li5 (a24 **248 **@&( kjahkuh"));
        }

        [Test]
        public void TestSimpleAdvanceCommand()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStoreDouble(), new MessageManagerDouble(), new EventManagerDouble());
            var command = lexer.Lex("adv go");
            Assert.AreEqual("adv", command.Text);
            Assert.AreEqual("go", command.Body.Text);
            Assert.AreEqual(null, command.Body.Next);
            Assert.AreEqual(null, command.Next);
        }

        [Test]
        public void TestAdvanceMoreThanMaxParameters()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStoreDouble(), new MessageManagerDouble(), new EventManagerDouble());
            var command = lexer.Lex("adv go go go go go!!!!!");
            Assert.AreEqual("adv", command.Text);
            Assert.AreEqual("go", command.Body.Text);
            Assert.AreEqual("go", command.Body.Next.Text);
            Assert.AreEqual("go", command.Body.Next.Next.Text);
            Assert.AreEqual("go", command.Body.Next.Next.Next.Text);
            Assert.AreEqual("go!!!!!", command.Body.Next.Next.Next.Next.Text);
            Assert.AreEqual(null, command.Body.Next.Next.Next.Next.Next);
            Assert.AreEqual(null, command.Next);
        }

        [Test]
        public void TestTwoAdvanceCommandsInARow()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStoreDouble(), new MessageManagerDouble(), new EventManagerDouble());
            var command = lexer.Lex("adv go ; adv go");
            Assert.AreEqual("adv", command.Text);
            Assert.AreEqual("go", command.Body.Text);
            Assert.AreEqual("adv", command.Next.Text);
            Assert.AreEqual("go", command.Next.Body.Text);
            Assert.AreEqual(null, command.Next.Body.Next);
            Assert.AreEqual(null, command.Next.Next);
        }

        [Test]
        public void TestSimpleTimerCommand()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStoreDouble(), new MessageManagerDouble(), new EventManagerDouble());
            var command = lexer.Lex("( 15s ) => { adv go } ");
            Assert.AreEqual("15s", command.Text);
            Assert.AreEqual("adv", command.Body.Text);
            Assert.AreEqual("go", command.Body.Body.Text);
            Assert.AreEqual(null, command.Body.Body.Next);
            Assert.AreEqual(null, command.Body.Next);
            Assert.AreEqual(null, command.Next);
        }

        [Test]
        public void TestNestedTimerCommand()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStoreDouble(), new MessageManagerDouble(), new EventManagerDouble());
            var command = lexer.Lex("( 15s ) => { ( 8s ) => { ( 729689s ) => { adv go } } } ");
            Assert.AreEqual("15s", command.Text);
            Assert.AreEqual("8s", command.Body.Text);
            Assert.AreEqual("729689s", command.Body.Body.Text);
            Assert.AreEqual("adv", command.Body.Body.Body.Text);
            Assert.AreEqual("go", command.Body.Body.Body.Body.Text);
            Assert.AreEqual(null, command.Body.Body.Body.Body.Next);
            Assert.AreEqual(null, command.Body.Body.Body.Next);
            Assert.AreEqual(null, command.Body.Body.Next);
            Assert.AreEqual(null, command.Body.Next);
            Assert.AreEqual(null, command.Next);
        }
    }
}
