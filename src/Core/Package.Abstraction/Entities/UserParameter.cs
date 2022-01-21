using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public class UserParameter: IEntity<string>
    {
        private Type _valueType;

        public string Id { get; }
        public object Value { get; }

        public UserParameter(string id, object value)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            _valueType = Value.GetType();
        }

        public bool TryAs<T>(out T val)
        {
            val = default;
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
