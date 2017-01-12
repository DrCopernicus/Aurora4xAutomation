using Aurora4xAutomationTests.ScriptFramework.AcceptableInputs;
using Aurora4xAutomationTests.ScriptFramework.ScriptedInputs;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.ScriptFrameworkTests
{
    [TestFixture]
    public class AcceptableClickTests
    {
        [Test]
        public void AcceptsClick()
        {
            var click = new ScriptedClick(45, 45);
            var acceptableClick = new AcceptableClick(30, 60, 30, 60);

            Assert.IsTrue(acceptableClick.Accepts(click));
        }

        [Test]
        public void RejectsClick()
        {
            var click = new ScriptedClick(20, 45);
            var acceptableClick = new AcceptableClick(30, 60, 30, 60);

            Assert.IsFalse(acceptableClick.Accepts(click));
        }

        [Test]
        public void RejectsKey()
        {
            var click = new ScriptedKeys("e");
            var acceptableClick = new AcceptableClick(30, 60, 30, 60);

            Assert.IsFalse(acceptableClick.Accepts(click));
        }
    }
}
