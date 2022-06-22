using System;

namespace Package.Domain
{
    public class Parameter
    {
        private Type _valueType;

        public string Id { get; }
        public object Value { get; }

        public Parameter(string id, object value)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            _valueType = Value.GetType();
        }

        public bool TryAs<T>(out T val)
        {
            val = default(T)!;
            if (typeof(T) == _valueType)
            {
                val = (T)Value;
                return true;
            }
            return false;
        }

        public T As<T>()
        {
            return (T)Value;
        }
    }
}
