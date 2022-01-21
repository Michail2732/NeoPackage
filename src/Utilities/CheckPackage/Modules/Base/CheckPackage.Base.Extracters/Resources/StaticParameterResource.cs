using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Extracters
{
    public class StaticParameterResource : IEntity<string>
    {
        public string Id { get; }
        public string Description { get; }
        public string Value { get; }

        public StaticParameterResource(string id, string description, string value)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
