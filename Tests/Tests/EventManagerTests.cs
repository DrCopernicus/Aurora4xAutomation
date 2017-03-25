using NSubstitute;
using NUnit.Framework;
using Server.Evaluators;
using Server.Events;
using Server.IO;
using Server.IO.DB;
using Server.Messages;
using Server.Settings;
using System;
using System.Threading;

namespace Tests.Tests
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
            var db = Substitute.For<IAuroraDB>();
            var executor = Substitute.For<IQueryExecutor>();
            var eventManager = new EventManager(uimap, settings, messages, db, executor);
            
            Assert.DoesNotThrow(() => eventManager.Stop());
        }

        [Test]
        public void ProcessEventsOnTimelineWithoutControlLoop()
        {
            var messages = Substitute.For<IMessageManager>();
            var settings = Substitute.For<ISettingsStore>();
            var uimap = Substitute.For<IUIMap>();
            uimap.GetTime().Returns(new Time());
            var db = Substitute.For<IAuroraDB>();
            var executor = Substitute.For<IQueryExecutor>();
            var eventManager = new EventManager(uimap, settings, messages, db, executor);

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
            var db = Substitute.For<IAuroraDB>();
            var executor = Substitute.For<IQueryExecutor>();
            var eventManager = new EventManager(uimap, settings, messages, db, executor);

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
        public void ExceptionsCannotPercolateFromEvaluatorsToEventManager()
        {
            var messages = Substitute.For<IMessageManager>();
            var settings = Substitute.For<ISettingsStore>();
            var uimap = Substitute.For<IUIMap>();
            uimap.GetTime().Returns(new Time());
            var db = Substitute.For<IAuroraDB>();
            var executor = Substitute.For<IQueryExecutor>();
            var eventManager = new EventManager(uimap, settings, messages, db, executor);

            var evaluator = Substitute.For<IEvaluator>();
            evaluator.When(x => x.Execute()).Do(x => { throw new Exception(); });
            eventManager.AddEvent(evaluator);

            Assert.DoesNotThrow(() => eventManager.ActOnActiveTimelineEntries());
        }

        [Test]
        public void ExceptionsAreContainedInEventManagerControlLoop()
        {
            var messages = Substitute.For<IMessageManager>();
            var settings = Substitute.For<ISettingsStore>();
            var uimap = Substitute.For<IUIMap>();
            uimap.GetTime().Returns(new Time());
            var db = Substitute.For<IAuroraDB>();
            var executor = Substitute.For<IQueryExecutor>();
            var eventManager = new EventManager(uimap, settings, messages, db, executor);

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
