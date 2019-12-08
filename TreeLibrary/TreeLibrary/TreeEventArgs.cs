using System;

namespace TreeLibrary
{
    public class TreeEventArgs<T> : EventArgs
    {
        public string Message { get; }
        public T Value { get; }

        public TreeEventArgs(string message, T value)
        {
            this.Message = message;
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format(Value.ToString());
        }
    }
}
