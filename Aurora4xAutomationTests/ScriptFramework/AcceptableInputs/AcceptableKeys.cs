using Aurora4xAutomationTests.ScriptFramework.ScriptedInputs;

namespace Aurora4xAutomationTests.ScriptFramework.AcceptableInputs
{
    public class AcceptableKeys : IAcceptableInput
    {
        private string _keys;

        public AcceptableKeys(string keys)
        {
            _keys = keys;
        }

        public bool Accepts(IScriptedInput input)
        {
            var key = input as ScriptedKeys;

            if (key == null)
                return false;

            return key.Keys == _keys;
        }
    }
}