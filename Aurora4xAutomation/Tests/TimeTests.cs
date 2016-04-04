using Aurora4xAutomation.Events;
using Aurora4xAutomation.OCR;
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
            Assert.AreEqual(8, time1.Month);
            Assert.AreEqual(29, time1.Day);
            Assert.AreEqual(0, time1.Hour);
            Assert.AreEqual(0, time1.Minute);
            Assert.AreEqual(11, time1.Second);

            var time2 = new Time("29th January 2035 23:00:11");
            Assert.AreEqual(2035, time2.Year);
            Assert.AreEqual(1, time2.Month);
            Assert.AreEqual(29, time2.Day);
            Assert.AreEqual(23, time2.Hour);
            Assert.AreEqual(0, time2.Minute);
            Assert.AreEqual(11, time2.Second);

            var time3 = new Time("2nd January 2035 23:00:11");
            Assert.AreEqual(2035, time3.Year);
            Assert.AreEqual(1, time3.Month);
            Assert.AreEqual(2, time3.Day);
            Assert.AreEqual(23, time3.Hour);
            Assert.AreEqual(0, time3.Minute);
            Assert.AreEqual(11, time3.Second);
        }

        [Test]
        public void TestTimeParserNoSeconds()
        {
            var time1 = new Time("29th August 2035 05:10");
            Assert.AreEqual(2035, time1.Year);
            Assert.AreEqual(8, time1.Month);
            Assert.AreEqual(29, time1.Day);
            Assert.AreEqual(5, time1.Hour);
            Assert.AreEqual(10, time1.Minute);
            Assert.AreEqual(0, time1.Second);
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
    }
}
