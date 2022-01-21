using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Building.Builders
{
    public readonly struct GroupKey
    {
        public IReadOnlyDictionary<string, string> Parameters { get; }

        public GroupKey(IReadOnlyDictionary<string, string> parameters)
        {
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }
    }
}
