using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora4xAutomation.Command.Evaluators;
using Aurora4xAutomation.Common;
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
            Timeline.AddEvent(new EvaluatorDouble());
            Assert.AreEqual(typeof(EvaluatorDouble), Timeline.PopNextActiveEvent(new Time()).GetType());
            Assert.AreEqual(null, Timeline.PopNextActiveEvent(new Time()));
        }

        [Test]
        public void TestTimelineAddInOrder()
        {
            var currentDate = new Time("29th January 2016 20:00:11");
            Timeline.AddEvent(new EvaluatorDouble(), new Time("25th January 2016 20:00:11"));
            Timeline.AddEvent(new EvaluatorDoubleTwo(), new Time("26th January 2016 20:00:11"));
            Timeline.AddEvent(new EvaluatorDoubleThree(), new Time("27th January 2016 20:00:11"));
            Assert.AreEqual(typeof(EvaluatorDouble), Timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(typeof(EvaluatorDoubleTwo), Timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(typeof(EvaluatorDoubleThree), Timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(null, Timeline.PopNextActiveEvent(currentDate));
        }

        [Test]
        public void TestTimelineNotInOrder()
        {
            var currentDate = new Time("29th January 2016 20:00:11");
            Timeline.AddEvent(new EvaluatorDouble(), new Time("25th January 2016 20:00:11"));
            Timeline.AddEvent(new EvaluatorDoubleThree(), new Time("27th January 2016 20:00:11"));
            Timeline.AddEvent(new EvaluatorDoubleTwo(), new Time("26th January 2016 20:00:11"));
            Assert.AreEqual(typeof(EvaluatorDouble), Timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(typeof(EvaluatorDoubleTwo), Timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(typeof(EvaluatorDoubleThree), Timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(null, Timeline.PopNextActiveEvent(currentDate));
        }

        [Test]
        public void TestItemsInTimelineAfterCurrentDate()
        {
            var currentDate = new Time("29th January 2016 20:00:11");
            Timeline.AddEvent(new EvaluatorDouble(), new Time("25th January 2016 20:00:11"));
            Timeline.AddEvent(new EvaluatorDoubleThree(), new Time("30th January 2016 20:00:11"));
            Timeline.AddEvent(new EvaluatorDoubleTwo(), new Time("26th January 2016 20:00:11"));
            Assert.AreEqual(typeof(EvaluatorDouble), Timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(typeof(EvaluatorDoubleTwo), Timeline.PopNextActiveEvent(currentDate).GetType());
            Assert.AreEqual(null, Timeline.PopNextActiveEvent(currentDate));
        }
    }
}
