using Aurora4xAutomation.IO;
using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Controls;
using Aurora4xAutomation.IO.UI.Display;
using NSubstitute;
using NUnit.Framework;
using System.Drawing;
using WindowsInput.Native;

namespace Aurora4xAutomationTests.Tests.UI
{
    [TestFixture]
    public class ControlPositioningTests
    {
        private IScreenObject GetWindow()
        {
            var window = Substitute.For<IScreenObject>();
            window.Top.Returns(10);
            window.Bottom.Returns(110);
            window.Left.Returns(10);
            window.Right.Returns(110);
            return window;
        }

        [Test]
        public void TestGenericControlCorrectLocation()
        {
            var control = new Control(GetWindow(), Substitute.For<IInputDevice>(), 10, 30, 10, 50);

            Assert.AreEqual(20, control.Top);
            Assert.AreEqual(40, control.Bottom);
            Assert.AreEqual(20, control.Left);
            Assert.AreEqual(60, control.Right);
        }

        [Test]
        public void TestButtonCorrectLocation()
        {
            var control = new Button(GetWindow(), Substitute.For<IInputDevice>(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }

        [Test]
        public void TestComboboxCorrectLocation()
        {
            var control = new Combobox(GetWindow(), Substitute.For<IInputDevice>(), Substitute.For<IOCRReader>(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }

        [Test]
        public void TestDataGridCorrectLocation()
        {
            var control = new Datagrid(GetWindow(), Substitute.For<IInputDevice>(), Substitute.For<IOCRReader>(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }

        [Test]
        public void TestLabelCorrectLocation()
        {
            var control = new Label(GetWindow(), Substitute.For<IInputDevice>(), Substitute.For<IOCRReader>(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }

        [Test]
        public void TestRadioButtonCorrectLocation()
        {
            var control = new RadioButton(GetWindow(), Substitute.For<IInputDevice>(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }

        [Test]
        public void TestTextboxCorrectLocation()
        {
            var control = new Textbox(GetWindow(), Substitute.For<IInputDevice>(), Substitute.For<IOCRReader>(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }

        [Test]
        public void TestTreeListCorrectLocation()
        {
            var control = new TreeList(GetWindow(), Substitute.For<IInputDevice>(), Substitute.For<IOCRReader>(), 20, 40, 20, 60);

            Assert.AreEqual(30, control.Top);
            Assert.AreEqual(50, control.Bottom);
            Assert.AreEqual(30, control.Left);
            Assert.AreEqual(70, control.Right);
        }
    }
}
