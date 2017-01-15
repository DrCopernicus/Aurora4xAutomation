using NSubstitute;
using NUnit.Framework;
using Server.IO;
using Server.IO.UI;
using Server.IO.UI.Controls;
using Server.IO.UI.Display;

namespace Aurora4xAutomationTests.Tests.UI
{
    [TestFixture]
    public class ClickingTests
    {
        private void AssertClicksAreCorrect(IScreenObject control, IInputDevice inputDevice)
        {
            var clicks = new[]
            {
                new []{0, 0},
                new []{2, 2},
                new []{-1, -55},
                new []{-100, 2},
                new []{3, 4},
                new []{1, 4}
            };

            foreach (var click in clicks)
            {
                control.Click(click[0], click[1], 0);
                inputDevice.Received(1).Click(click[0] + 1, click[1] + 1, 0);
                inputDevice.ClearReceivedCalls();
            }
        }

        [Test]
        public void ClicksCorrectPixelOnGenericControl()
        {
            var inputDevice = Substitute.For<IInputDevice>();
            var control = new Control(Substitute.For<IScreen>(), inputDevice, 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnButton()
        {
            var inputDevice = Substitute.For<IInputDevice>();
            var control = new Button(Substitute.For<IScreen>(), inputDevice, 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnCombobox()
        {
            var inputDevice = Substitute.For<IInputDevice>();
            var control = new Combobox(Substitute.For<IScreen>(), inputDevice, Substitute.For<IOCRReader>(), 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnDatagrid()
        {
            var inputDevice = Substitute.For<IInputDevice>();
            var control = new Datagrid(Substitute.For<IScreen>(), inputDevice, Substitute.For<IOCRReader>(), 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnLabel()
        {
            var inputDevice = Substitute.For<IInputDevice>();
            var control = new Label(Substitute.For<IScreen>(), inputDevice, Substitute.For<IOCRReader>(), 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnRadioButton()
        {
            var inputDevice = Substitute.For<IInputDevice>();
            var control = new RadioButton(Substitute.For<IScreen>(), inputDevice, 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnTextbox()
        {
            var inputDevice = Substitute.For<IInputDevice>();
            var control = new Textbox(Substitute.For<IScreen>(), inputDevice, Substitute.For<IOCRReader>(), 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }

        [Test]
        public void ClicksCorrectPixelOnTreeList()
        {
            var inputDevice = Substitute.For<IInputDevice>();
            var control = new TreeList(Substitute.For<IScreen>(), inputDevice, Substitute.For<IOCRReader>(), 1, 3, 1, 3);
            AssertClicksAreCorrect(control, inputDevice);
        }
    }
}
