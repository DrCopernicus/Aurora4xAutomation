using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aurora4xAutomation.UI;
using NUnit.Framework;

namespace Aurora4xAutomation.Tests
{
    [TestFixture]
    public class Sandbox
    {
        [Test]
        public void MyTest()
        {
            var pnw = new PaintNetWindow();
            pnw.MakeActive();
            Thread.Sleep(1000);
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    pnw.Paint(x*10+400, y*10+400);
                }
            }
        }
    }
}
