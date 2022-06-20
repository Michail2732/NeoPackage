
using System;
using System.Collections.Generic;
using System.Linq;

namespace Package.Domain.Factories
{

    public sealed class PackageItemBuilder : PackageEntityBuilder
    {
        public Dictionary<string, string> Properties { get; private set; }
            = new Dictionary<string, string>();

        public Dictionary<string, Parameter> Parameters { get; private set; }
            = new Dictionary<string, Parameter>();


        public PackageItem Build() => new PackageItem(
            Id ?? "",
            Name ?? "",
            _children.ToList(),
            new Dictionary<string, string>(Properties),
            new Dictionary<string, Parameter>(Parameters));

        public void Clear()
        {
            Parameters.Clear();
            Properties.Clear();
            Id = string.Empty;
            Name = string.Empty;
            _children.Clear();
        }
    }
}