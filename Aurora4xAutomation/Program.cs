using System.Threading;
using Aurora4xAutomation.Automation;
using Aurora4xAutomation.REST;

namespace Aurora4xAutomation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var p = new Program();
        }

        public Program()
        {
            new Thread(CommandFlowManager.Begin);
            new RESTManager().Begin();
        }
    }
}
