using System;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.UI
{
    public class AuroraWrapperWindow : Window
    {
        public AuroraWrapperWindow() 
            : base("Aurora Wrapper")
        {
            
        }

        public void OpenBase()
        {
            MakeActive();
            this.Click((Left + Right) / 2, (Top + Bottom) / 2);
        }

        protected override void OpenIfNotFound()
        {
            throw new Exception("Could not find the Aurora Wrapper!");
        }
    }
}
