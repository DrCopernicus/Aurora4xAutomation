using Tests.ScriptFramework.ScriptedInputs;

namespace Tests.ScriptFramework.AcceptableInputs
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