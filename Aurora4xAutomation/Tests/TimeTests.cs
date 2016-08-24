using System.Collections.Generic;
using Aurora4xAutomation.Events;
using NUnit.Framework;

namespace Aurora4xAutomation.Tests
{
    [TestFixture]
    public class TimeTests
    {
        [Test]
        public void TestTimeParserBasics()
        {
            var time1 = new Time("29th August 2035 00:00:11");
            Assert.AreEqual(2035, time1.Year);
            Assert.AreEqual(7, time1.Month);
            Assert.AreEqual(28, time1.Day);
            Assert.AreEqual(0, time1.Hour);
            Assert.AreEqual(0, time1.Minute);
            Assert.AreEqual(11, time1.Second);

            var time2 = new Time("29th January 2035 23:00:11");
            Assert.AreEqual(2035, time2.Year);
            Assert.AreEqual(0, time2.Month);
            Assert.AreEqual(28, time2.Day);
            Assert.AreEqual(23, time2.Hour);
            Assert.AreEqual(0, time2.Minute);
            Assert.AreEqual(11, time2.Second);

            var time3 = new Time("2nd January 2035 23:00:11");
            Assert.AreEqual(2035, time3.Year);
            Assert.AreEqual(0, time3.Month);
            Assert.AreEqual(1, time3.Day);
            Assert.AreEqual(23, time3.Hour);
            Assert.AreEqual(0, time3.Minute);
            Assert.AreEqual(11, time3.Second);
        }

        [Test]
        public void TestTimeParserNoSeconds()
        {
            var time1 = new Time("29th August 2035 05:10");
            Assert.AreEqual(2035, time1.Year);
            Assert.AreEqual(7, time1.Month);
            Assert.AreEqual(28, time1.Day);
            Assert.AreEqual(5, time1.Hour);
            Assert.AreEqual(10, time1.Minute);
            Assert.AreEqual(0, time1.Second);
        }

        [Test]
        public void TestTimeFromTotalSeconds()
        {
            var time = new Time(63007128655);
            Assert.AreEqual(new Time("10th September 2025 04:10:55"), time);
            Assert.AreEqual(63007128655, time.GetTotalSeconds());
        }

        [Test]
        public void TestTimeComparison()
        {
            var time1 = new Time("29th August 2035 05:10");
            var time2 = new Time("29th August 2035 05:10");
            var time3 = new Time("19th August 2035 05:10");
            var time4 = new Time("19th August 2035 05:10:01");
            var time5 = new Time("20th February 2084 05:10:01");
            var time6 = new Time("2nd March 1204 22:12:40");

            Assert.IsTrue(time1 == time2);
            Assert.IsTrue(time1 != time3);

            Assert.IsTrue(time1 > time3);
            Assert.IsTrue(time3 < time1);

            Assert.IsTrue(time4 > time3);
            Assert.IsTrue(time3 < time4);

            Assert.IsTrue(time5 > time1);
            Assert.IsTrue(time5 > time2);
            Assert.IsTrue(time5 > time3);
            Assert.IsTrue(time5 > time4);
            Assert.IsTrue(time5 == time5);
            Assert.IsTrue(time5 > time6);
        }
        
        [Test]
        public void TestAddingDays()
        {
            var time1 = new Time("29th August 2035 05:10");
            var time2 = new Time(0, 0, 1, 0, 0, 0);

            Assert.AreEqual(new Time("30th August 2035 05:10"), time1 + time2);
        }

        [Test]
        public void TestAddingManyDays()
        {
            var time1 = new Time("30th August 2035 05:10");
            var time2 = new Time(0, 0, 180, 0, 0, 0);

            Assert.AreEqual(new Time("30th February 2036 05:10"), time1 + time2);
        }

        [Test]
        public void TestAddingYears()
        {
            var time1 = new Time("30th August 2035 05:10");
            var time2 = new Time(1000, 0, 0, 0, 0, 0);

            Assert.AreEqual(new Time("30th August 3035 05:10"), time1 + time2);
        }

        [Test]
        public void TestAddingNegativeDays()
        {
            var time1 = new Time("30th August 2035 05:10");
            var time2 = new Time(0, 0, -1, 0, 0, 0);

            Assert.AreEqual(new Time("29th August 2035 05:10"), time1 + time2);
        }

        [Test]
        public void TestAddingNegativeMonths()
        {
            var time1 = new Time("30th February 2036 05:10");
            var time2 = new Time(0, -6, 0, 0, 0, 0);

            Assert.AreEqual(new Time("30th August 2035 05:10"), time1 + time2);
        }

        [Test]
        public void TestAddingManyNegativeDays()
        {
            var time1 = new Time("30th February 2036 05:10");
            var time2 = new Time(0, 0, -180, 0, 0, 0);

            Assert.AreEqual(new Time("30th August 2035 05:10"), time1 + time2);
        }

        [Test]
        public void TestAddingNegativeYears()
        {
            var time1 = new Time("30th August 3035 05:10");
            var time2 = new Time(-1000, 0, 0, 0, 0, 0);

            Assert.AreEqual(new Time("30th August 2035 05:10"), time1 + time2);
        }

        [Test]
        public void TestIncorrectMonths()
        {
            Assert.Throws(typeof(KeyNotFoundException), () => new Time("29th Undecimber 2035 05:10"));
            Assert.Throws(typeof(KeyNotFoundException), () => new Time("80th Feburary 2035 05:10"));
            Assert.Throws(typeof(KeyNotFoundException), () => new Time("1st Minnesota 2035 05:10"));
        }

        [Test]
        public void TestGetTotalSecondsTime()
        {
            var time = new Time("10th September 2067 04:10:55");
            Assert.AreEqual(64313496655, time.GetTotalSeconds());
        }

        [Test]
        public void TestGetNegativeTimeOneYear()
        {
            var time = new Time(-1, 0, 0, 0, 0, 0);
            Assert.AreEqual(-31104000, time.GetTotalSeconds());
        }

        [Test]
        public void TestGetNegativeTimeManyYears()
        {
            var time = new Time(-1000, 0, 0, 0, 0, 0);
            Assert.AreEqual(-31104000000, time.GetTotalSeconds());
        }
    }
}
