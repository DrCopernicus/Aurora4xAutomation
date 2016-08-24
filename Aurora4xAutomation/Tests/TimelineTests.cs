using System;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Events;
using NUnit.Framework;

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
    }
}
