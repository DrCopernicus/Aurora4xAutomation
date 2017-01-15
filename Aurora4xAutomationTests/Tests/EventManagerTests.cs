using System;
using System.Threading;
using NSubstitute;
using NUnit.Framework;
using Server.Evaluators;
using Server.Events;
using Server.IO;
using Server.Messages;
using Server.Settings;

namespace Aurora4xAutomationTests.Tests
{
    [TestFixture]
    public class EventManagerTests
    {
        [Test]
        public void DoesNotCrashWhenStoppedWithoutBeingPreviouslyStarted()
        {
            var messages = Substitute.For<IMessageManager>();
            var settings = Substitute.For<ISettingsStore>();
            var uimap = Substitute.For<IUIMap>();
            uimap.GetTime().Returns(new Time());
            var eventManager = new EventManager(uimap, settings, messages);
            
            Assert.DoesNotThrow(() => eventManager.Stop());
        }

        [Test]
        public void ProcessEventsOnTimelineWithoutControlLoop()
        {
            var messages = Substitute.For<IMessageManager>();
            var settings = Substitute.For<ISettingsStore>();
            var uimap = Substitute.For<IUIMap>();
            uimap.GetTime().Returns(new Time());
            var eventManager = new EventManager(uimap, settings, messages);

            var evaluator = Substitute.For<IEvaluator>();
            eventManager.AddEvent(evaluator);

            evaluator.Received(0).Execute();
            eventManager.ActOnActiveTimelineEntries();
            evaluator.Received(1).Execute();
        }
        
        [Test]
        public void ProcessEventsOnTimelineWithControlLoop()
        {
            var messages = Substitute.For<IMessageManager>();
            var settings = Substitute.For<ISettingsStore>();
            settings.DatabasePassword.Returns(x => null);
            var uimap = Substitute.For<IUIMap>();
            uimap.GetTime().Returns(new Time());
            var eventManager = new EventManager(uimap, settings, messages);

            var evaluator = Substitute.For<IEvaluator>();
            eventManager.AddEvent(evaluator);

            evaluator.Received(0).Execute();

            var logger = Substitute.For<ILogger>();
            eventManager.Begin(logger);
            Thread.Sleep(2000);
            eventManager.Stop();

            evaluator.Received(1).Execute();
        }

        [Test]
        public void ExceptionsCanPercolateFromEvaluatorsToEventManager()
        {
            var messages = Substitute.For<IMessageManager>();
            var settings = Substitute.For<ISettingsStore>();
            var uimap = Substitute.For<IUIMap>();
            uimap.GetTime().Returns(new Time());
            var eventManager = new EventManager(uimap, settings, messages);

            var evaluator = Substitute.For<IEvaluator>();
            evaluator.When(x => x.Execute()).Do(x => { throw new Exception(); });
            eventManager.AddEvent(evaluator);

            Assert.Throws<Exception>(() => eventManager.ActOnActiveTimelineEntries());
        }

        [Test]
        public void ExceptionsAreContainedInEventManagerControlLoop()
        {
            var messages = Substitute.For<IMessageManager>();
            var settings = Substitute.For<ISettingsStore>();
            var uimap = Substitute.For<IUIMap>();
            uimap.GetTime().Returns(new Time());
            var eventManager = new EventManager(uimap, settings, messages);

            var evaluator = Substitute.For<IEvaluator>();
            evaluator.When(x => x.Execute()).Do(x => { throw new Exception(); });
            eventManager.AddEvent(evaluator);

            var logger = Substitute.For<ILogger>();
            eventManager.Begin(logger);
            Thread.Sleep(2000);
            eventManager.Stop();

            logger.Received(1).Error(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
