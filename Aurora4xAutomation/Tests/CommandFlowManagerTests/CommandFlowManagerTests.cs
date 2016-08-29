using Aurora4xAutomation.Automation;
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
using System.Threading;

namespace Aurora4xAutomation.Tests.CommandFlowManagerTests
{
    [TestFixture]
    public class CommandFlowManagerTests
    {
        private class EmptyUIMap : IUIMap
        {
            public BaseAuroraWindow BaseAuroraWindow { get; private set; }
            public EventWindow EventWindow { get; private set; }
            public CommandersWindow Leaders { get; private set; }
            public SystemMapWindow SystemMap { get; private set; }
            public TaskGroupsWindow TaskGroups { get; private set; }
            public PopulationAndProductionWindow PopulationAndProductionWindow { get; private set; }
            public Time GetTime()
            {
                return new Time();
            }
        }

        private class MessageWriterEvaluator : MessageEvaluator
        {
            public MessageWriterEvaluator(string text, IMessageManager messages)
                : base(text, messages)
            {
            }

            protected override void Evaluate()
            {
                Messages.AddMessage(MessageType.Information, "test message");
            }

            public override string Help
            {
                get { throw new System.NotImplementedException(); }
            }
        }

        private class ThrowEvaluator : Evaluator
        {
            public ThrowEvaluator(string text)
                : base(text)
            {
            }

            protected override void Evaluate()
            {
                throw new Exception();
            }

            public override string Help
            {
                get { throw new System.NotImplementedException(); }
            }
        }

        private class SettingsStoreDouble : ISettingsStore
        {
            public bool Stopped { get { return true; } set { } }
            public bool AutoTurnsOn { get { return false; } set { } }
            public string DatabaseLocation { get; private set; }
            public string DatabasePassword { get; private set; }
            public string EventLogLocation { get; private set; }
            public int RaceId { get; set; }
            public Dictionary<string, string> Research { get; set; }
            public Dictionary<string, Dictionary<string, string>> ResearchFocuses { get; private set; }
            public int GameId { get; set; }
            public IncrementLength Increment { get; set; }
        }

        private class LoggerDouble : ILogger
        {
            public void Error(Exception e)
            {
                ErrorsInLog++;
            }

            public void Write(string message)
            {
            }

            public int ErrorsInLog { get; set; }
        }

        [Test]
        public void ParsesAndExecutesPrintFunction()
        {
            var logger = new LoggerDouble();
            var settings = new SettingsStoreDouble();
            var uiMap = new EmptyUIMap();
            var messages = new MessageManager();
            var eventManager = new EventManager(uiMap, settings, messages);

            var commandFlowManager = new CommandFlowManager(settings, uiMap, messages, eventManager, logger);

            commandFlowManager.QueueCommand("print abcdefg");

            commandFlowManager.Begin();

            Thread.Sleep(2000);

            commandFlowManager.Stop();

            Assert.Contains("[INFO] abcdefg", messages.GetMessagesAfterId(-1, 1000));
            Assert.GreaterOrEqual(messages.GetLastId(), 0);
        }
    }
}
