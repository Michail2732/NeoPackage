using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Building.Builders
{
    public readonly struct PackageBuildingResult
    {
        public readonly string Id;
        public readonly string Name;

        public PackageBuildingResult(string id, string name)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
