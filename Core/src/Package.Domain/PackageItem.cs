using System;
using System.Collections.Generic;
using System.Linq;

namespace Package.Domain
{
    public class PackageItem: IPackageEntity
    {        
        public string Id { get; }        
        public uint Level { get; private set; }
        public string Name { get; }        

        private readonly List<PackageItem> _children;
        public IReadOnlyList<PackageItem> Children => _children;

        private readonly Dictionary<string, string> _properties;
        public IReadOnlyDictionary<string, string> Properties => _properties;

        private readonly Dictionary<string, Parameter> _parameters;
        public IReadOnlyDictionary<string, Parameter> Parameters => _parameters;

        internal PackageItem(string id, string name, 
            List<PackageItem> children,
            Dictionary<string, string> properties, 
            Dictionary<string, Parameter> parameters)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));            
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _children = children ?? throw new ArgumentNullException(nameof(children));
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            SetLevels();
        }

        private void SetLevels()
        {
            var height = this.GetHeightEntity() - 1;
            List<PackageItem> items = new List<PackageItem> { this};
            IEnumerable<PackageItem>? childs = null;
            while (items.Count > 0)
            {
                items.ForEach(a => a.Level = height);
                childs = items.SelectMany(a => a.Children);
                items.Clear();
                items.AddRange(childs);
                height--;
            }            
        }
        
        private uint GetHeightEntity()
        {
            if (Children.Count == 0) return 0;
            return 1 + Children.Max(a => a.GetHeightEntity());
        }
        
    }
}
