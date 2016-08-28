using Aurora4xAutomation.Evaluators.Message;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.IO.UI.Windows;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.Settings;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;

namespace Aurora4xAutomation.Tests
{
    [TestFixture]
    public class EventManagerTests
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
                throw new System.NotImplementedException();
            }

            private List<string> _messages = new List<string>();
        }

        private class MessageWriterEvaluator : MessageEvaluator
        {
            public MessageWriterEvaluator(string text, IMessageManager messages) : base(text, messages)
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

        private class SettingsStoreDouble : ISettingsStore
        {
            public bool Stopped { get { return true; } set {} }
            public bool AutoTurnsOn { get { return false; } set {} }
            public string DatabaseLocation { get; private set; }
            public string DatabasePassword { get; private set; }
            public string EventLogLocation { get; private set; }
            public int RaceId { get; set; }
            public Dictionary<string, string> Research { get; set; }
            public Dictionary<string, Dictionary<string, string>> ResearchFocuses { get; private set; }
            public int GameId { get; set; }
            public IncrementLength Increment { get; set; }
        }

        [Test]
        public void DoesNotCrashWhenStoppedWithoutBeingPreviouslyStarted()
        {
            var messages = new MessageManagerDouble();
            var settings = new SettingsStoreDouble();
            var eventManager = new EventManager(new EmptyUIMap(), settings, messages);
            
            Assert.DoesNotThrow(() => eventManager.Stop());
        }

        [Test]
        public void ProcessEventsOnTimelineWithoutControlLoop()
        {
            var messages = new MessageManagerDouble();
            var settings = new SettingsStoreDouble();
            var eventManager = new EventManager(new EmptyUIMap(), settings, messages);

            eventManager.AddEvent(new MessageWriterEvaluator("", messages));

            Assert.AreEqual(0, messages.GetMessagesAfterId(-1, 100).Count);

            eventManager.ActOnActiveTimelineEntries();

            Assert.Contains("test message", messages.GetMessagesAfterId(-1, 100));
        }
        
        [Test]
        public void ProcessEventsOnTimelineWithControlLoop()
        {
            var messages = new MessageManagerDouble();
            var settings = new SettingsStoreDouble();
            var eventManager = new EventManager(new EmptyUIMap(), settings, messages);

            eventManager.AddEvent(new MessageWriterEvaluator("", messages));

            Assert.AreEqual(0, messages.GetMessagesAfterId(-1, 100).Count);

            eventManager.Begin();
            
            Thread.Sleep(2000);

            eventManager.Stop();

            Assert.Contains("test message", messages.GetMessagesAfterId(-1, 100));
        }
    }
}
