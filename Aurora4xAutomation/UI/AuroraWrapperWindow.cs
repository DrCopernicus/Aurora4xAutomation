using System;

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
            Click((Dimensions.Left+Dimensions.Right)/2, (Dimensions.Top+Dimensions.Bottom)/2);
        }

        protected override void OpenIfNotFound()
        {
            throw new Exception("Could not find the Aurora Wrapper!");
        }
    }
}
