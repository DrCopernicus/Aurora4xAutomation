using Aurora4xAutomationTests.ScriptFramework.ScriptedInputs;

namespace Aurora4xAutomationTests.ScriptFramework.AcceptableInputs
{
    public class AcceptableClick : IAcceptableInput
    {
        private readonly int _top;
        private readonly int _bottom;
        private readonly int _left;
        private readonly int _right;

        public AcceptableClick(int top, int bottom, int left, int right)
        {
            _top = top;
            _bottom = bottom;
            _left = left;
            _right = right;
        }

        public bool Accepts(IScriptedInput input)
        {
            var click = input as ScriptedClick;

            if (click == null)
                return false;

            return click.X >= _left && click.X <= _right && click.Y >= _top && click.Y <= _bottom;
        }
    }
}