using Aurora4xAutomationTests.ScriptFramework;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.ScriptFrameworkTests
{
    [TestFixture]
    public class ScriptableStateTests
    {
        [Test]
        public void AcceptsAcceptableInput()
        {
            var state = new ScriptableState();
            state.Screen = null;
            state.RegisterClick(0, 0, 10, 10, new ScriptableState());
        }
    }
}
