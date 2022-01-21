using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Entities
{
    public readonly struct Parameter
    {
        public readonly string Id;
        public readonly object Value;
        public readonly bool IsString;

        public Parameter(string parameterId, object parameterValue)
        {
            if (string.IsNullOrEmpty(parameterId))            
                throw new ArgumentException(nameof(parameterId));            
            Id = parameterId;
            Value = parameterValue ?? throw new ArgumentNullException(nameof(parameterValue));
            IsString = false;
        }

        public Parameter(string parameterId, string parameterValue)
        {
            if (string.IsNullOrEmpty(parameterId))
                throw new ArgumentException(nameof(parameterId));
            Id = parameterId;
            Value = parameterValue ?? throw new ArgumentNullException(nameof(parameterValue));
            IsString = true;
        }


        public static Parameter Empty => new Parameter();

        public override bool Equals(object? obj)
        {
            return obj is Parameter result &&
                   Id == result.Id &&
                   EqualityComparer<object>.Default.Equals(Value, result.Value) &&
                   IsString == result.IsString;
        }

        public override int GetHashCode()
        {
            int hashCode = -2074777601;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + IsString.GetHashCode();
            return hashCode;
        }

    }
}
