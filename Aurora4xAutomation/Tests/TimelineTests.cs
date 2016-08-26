using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Events;
using NUnit.Framework;
using System;
using System.Threading;

namespace Aurora4xAutomation.Tests
{
    [TestFixture]
    public class TimelineTests
    {
        private class EvaluatorDouble : Evaluator
        {
            public EvaluatorDouble() : base("default") { }
            protected override void Evaluate() { throw new NotImplementedException(); }
            public override string Help { get { throw new NotImplementedException(); } }
        }

        private class EvaluatorDoubleTwo : Evaluator
        {
            public EvaluatorDoubleTwo() : base("default") { }
            protected override void Evaluate() { throw new NotImplementedException(); }
            public override string Help { get { throw new NotImplementedException(); } }
        }

        private class EvaluatorDoubleThree : Evaluator
        {
            public EvaluatorDoubleThree() : base("default") { }
            protected override void Evaluate() { throw new NotImplementedException(); }
            public override string Help { get { throw new NotImplementedException(); } }
        }
        
        [Test]
        public void TestTimelineAddAndRemoveOne()
        {
            var timeline = new Timeline();

            timeline.AddEvent(new EvaluatorDouble());
            Assert.AreEqual(typeof(EvaluatorDouble), timeline.PopNextActiveEvent(new Time()).GetType());
            Assert.AreEqual(null, timeline.PopNextActiveEvent(new Time()));
        }

        [Test]
        public void TestTimelineAddInOrder()
        {
            var currentDate = new Time("29th January 2016 20:00:11");
            var timeline = new Timeline();

            timeline.AddEvent(new EvaluatorDouble(), new Time("25th January 2016 20:00:11"));
            timeline.AddEvent(new EvaluatorDoubleTwo(), new Time("26th January 2016 20:00:11"));
            timeline.AddEvent(new EvaluatorDoubleThree(), new Time("27th January 2016 20:00:11"));
            Assert.AreEqual(typeof(EvaluatorDouble), timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(typeof(EvaluatorDoubleTwo), timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(typeof(EvaluatorDoubleThree), timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(null, timeline.PopNextActiveEvent(currentDate));
        }

        [Test]
        public void TestTimelineNotInOrder()
        {
            var currentDate = new Time("29th January 2016 20:00:11");
            var timeline = new Timeline();

            timeline.AddEvent(new EvaluatorDouble(), new Time("25th January 2016 20:00:11"));
            timeline.AddEvent(new EvaluatorDoubleThree(), new Time("27th January 2016 20:00:11"));
            timeline.AddEvent(new EvaluatorDoubleTwo(), new Time("26th January 2016 20:00:11"));
            Assert.AreEqual(typeof(EvaluatorDouble), timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(typeof(EvaluatorDoubleTwo), timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(typeof(EvaluatorDoubleThree), timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(null, timeline.PopNextActiveEvent(currentDate));
        }

        [Test]
        public void TestItemsInTimelineAfterCurrentDate()
        {
            var currentDate = new Time("29th January 2016 20:00:11");
            var timeline = new Timeline();

            timeline.AddEvent(new EvaluatorDouble(), new Time("25th January 2016 20:00:11"));
            timeline.AddEvent(new EvaluatorDoubleThree(), new Time("30th January 2016 20:00:11"));
            timeline.AddEvent(new EvaluatorDoubleTwo(), new Time("26th January 2016 20:00:11"));
            Assert.AreEqual(typeof(EvaluatorDouble), timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(typeof(EvaluatorDoubleTwo), timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(null, timeline.PopNextActiveEvent(currentDate));
        }

        [Test]
        public void CanAddEventsFromAnotherThread()
        {
            var timeline = new Timeline();

            for (var i = 0; i < 5; i++)
                timeline.AddEvent(new EvaluatorDouble());

            Assert.AreEqual(5, timeline.Events.Count);
            
            var addingThread = new Thread(() =>
            {
                for (var i = 0; i < 10; i++)
                    timeline.AddEvent(new EvaluatorDouble());
            });

            addingThread.Start();
            addingThread.Join();

            Assert.AreEqual(15, timeline.Events.Count);
        }

        [Test]
        public void CanPopEventsFromAnotherThread()
        {
            var currentDate = new Time("29th January 2016 20:00:11");
            var timeline = new Timeline();

            for (var i = 0; i < 10; i++)
                timeline.AddEvent(new EvaluatorDouble());

            Assert.AreEqual(10, timeline.Events.Count);

            var removalThread = new Thread(() =>
            {
                for (var i = 0; i < 10; i++)
                    timeline.PopNextActiveEvent(currentDate);
            });

            removalThread.Start();
            removalThread.Join();

            Assert.AreEqual(0, timeline.Events.Count);
        }

        [Test]
        public void MultiThreadedAccessAllowsAddingAndRemovingSimultaneously()
        {
            var currentDate = new Time("29th January 2016 20:00:11");
            var timeline = new Timeline();

            for (var i = 0; i < 1000; i++)
                timeline.AddEvent(new EvaluatorDouble());

            Assert.AreEqual(1000, timeline.Events.Count);

            var removalThread = new Thread(() =>
            {
                for (var i = 0; i < 500; i++)
                    timeline.PopNextActiveEvent(currentDate);
            });

            var addingThread = new Thread(() =>
            {
                for (var i = 0; i < 2000; i++)
                    timeline.AddEvent(new EvaluatorDouble());
            });

            removalThread.Start();
            addingThread.Start();

            removalThread.Join();
            addingThread.Join();

            Assert.AreEqual(2500, timeline.Events.Count);
        }
    }
}
