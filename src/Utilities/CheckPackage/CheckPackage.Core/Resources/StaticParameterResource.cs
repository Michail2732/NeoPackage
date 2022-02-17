using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Resources
{
    public class StaticParameterResource : IRepositoryItem<string>
    {
        public string Id { get; }
        public string ParameterId { get; }
        public string Value { get; }

        public StaticParameterResource(string id, string parameterId, string value)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            ParameterId = parameterId ?? throw new ArgumentNullException(nameof(parameterId));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
