namespace Aurora4xAutomationTests.ScriptFramework.ScriptedInputs
{
    public class ScriptedKeys : IScriptedInput
    {
        public string Keys { get; private set; }

        public ScriptedKeys(string keys)
        {
            Keys = keys;
        }
    }
}