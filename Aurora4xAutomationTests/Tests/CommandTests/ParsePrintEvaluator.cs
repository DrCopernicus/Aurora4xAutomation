using System;
using System.Collections.Generic;
using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.IO.UI.Windows;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.Settings;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.CommandTests
{
    [TestFixture]
    public class ParsePrintEvaluator
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

            private List<string> _messages = new List<string>();
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
        public void NoParameterPrintThrowsWhenExecuted()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStoreDouble(), new MessageManagerDouble(), new EventManagerDouble());

            var evaluator = lexer.Lex("print");

            Assert.Throws<Exception>(() => evaluator.Execute());
        }

        [Test]
        public void SingleParameterPrintAddsMessageToMessageManager()
        {
            var messages = new MessageManagerDouble();
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStoreDouble(), messages, new EventManagerDouble());

            var evaluator = lexer.Lex("print abcd");

            Assert.DoesNotThrow(() => evaluator.Execute());
            Assert.Contains("abcd", messages.GetMessagesAfterId(-1, 1000));
        }

        [Test]
        public void MoreThanOneParamterPrintThrowsWhenExecuted()
        {
            var messages = new MessageManagerDouble();
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStoreDouble(), messages, new EventManagerDouble());

            var evaluator = lexer.Lex("print abcd efg hijiijl kljlaisjdknmv rg");

            Assert.Throws<Exception>(() => evaluator.Execute());
        }
    }
}
