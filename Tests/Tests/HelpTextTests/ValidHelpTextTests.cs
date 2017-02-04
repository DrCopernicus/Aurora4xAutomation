using NUnit.Framework;
using Server.Evaluators.Helpers;
using System;

namespace Tests.Tests.HelpTextTests
{
    [TestFixture]
    public class ValidHelpTextTests
    {
        [Test]
        public void CannotCreateHelpTextExampleWithoutDescription()
        {
            var helpText = new HelpText("command", "this is the description");
            Assert.Throws<Exception>(() => helpText.AddRow());
        }

        [Test]
        public void CreatesParameterlessHelpTextWithOneRow()
        {
            var helpText = new HelpText("command", "this is the description");
            helpText.AddRow("this command does things");

            Assert.AreEqual("command", helpText.CommandName);
            Assert.AreEqual("this is the description", helpText.Description);
            Assert.AreEqual(1, helpText.Examples.Count);
            Assert.AreEqual("command", helpText.Examples[0].Invokation);
            Assert.AreEqual("this command does things", helpText.Examples[0].Effect);
        }
    }
}
