using System;

namespace Aurora4xAutomation.Game
{
    public class Dirtyable<T>
    {
        private T _value;
        public T Value {
            get
            {
                if (!_isDirty)
                    return _value;
                _value = _get();
                _isDirty = false;
                return _value;
            }
            set
            {
                _set(value);
                if (!_isReliable)
                    _isDirty = true;
            }
        }

        private bool _isDirty;
        private readonly Func<T> _get;
        private readonly Action<T> _set;
        private readonly bool _isReliable;

        public Dirtyable(Func<T> get, Action<T> set, bool isReliable = false)
        {
            _get = get;
            _set = set;
            _isDirty = true;
            _isReliable = isReliable;
        }
    }
}
