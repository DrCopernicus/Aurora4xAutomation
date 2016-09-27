using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Evaluators.Message;
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
    public class ParserTests
    {
        private class TestUIMap : IUIMap
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

        private class TestSettings : ISettingsStore
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

        private class TestMessageManager : IMessageManager
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

        private class TestEventManager : IEventManager
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
        public void HelpCommandHasCorrectEvaluatorLabels()
        {
            var commandParser = new CommandParser(new TestUIMap(), new TestSettings(), new TestMessageManager(),
                new TestEventManager());
            var evaluator = commandParser.Parse("help help");

            Assert.AreEqual("help", evaluator.Text);
            Assert.AreEqual("help", evaluator.Body.Text);
        }

        [Test]
        public void HelpCommandHasCorrectTypes()
        {
            var commandParser = new CommandParser(new TestUIMap(), new TestSettings(), new TestMessageManager(),
                new TestEventManager());
            var evaluator = commandParser.Parse("help help");
            
            Assert.AreEqual(typeof(HelpEvaluator), evaluator.GetType());
            Assert.AreEqual(typeof(HelpEvaluator), evaluator.Body.GetType());
        }

        [Test]
        public void IncorrectCommandWithoutParametersReturnsLogCommand()
        {
            var commandParser = new CommandParser(new TestUIMap(), new TestSettings(), new TestMessageManager(),
                new TestEventManager());
            var evaluator = commandParser.Parse("thiscommanddoesnotexist");

            Assert.AreEqual(typeof(LogEvaluator), evaluator.GetType());
        }

        [Test]
        public void IncorrectCommandWithOneParameterReturnsLogCommand()
        {
            var commandParser = new CommandParser(new TestUIMap(), new TestSettings(), new TestMessageManager(),
                new TestEventManager());
            var evaluator = commandParser.Parse("thiscommanddoesnotexist parameter1");

            Assert.AreEqual(typeof(LogEvaluator), evaluator.GetType());
        }

        [Test]
        public void IncorrectCommandWithFiveParametersReturnsLogCommand()
        {
            var commandParser = new CommandParser(new TestUIMap(), new TestSettings(), new TestMessageManager(),
                new TestEventManager());
            var evaluator = commandParser.Parse("thiscommanddoesnotexist parameter1 parameter2 parameter3 parameter4 parameter5");

            Assert.AreEqual(typeof(LogEvaluator), evaluator.GetType());
        }
    }
}
