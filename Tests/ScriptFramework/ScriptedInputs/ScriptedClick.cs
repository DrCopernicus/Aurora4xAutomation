namespace Tests.ScriptFramework.ScriptedInputs
{
    public class ScriptedClick : IScriptedInput
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public ScriptedClick(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}