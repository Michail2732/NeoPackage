using System.Collections.Generic;
using Package.Domain.Enumerators;
using Package.Domain.Exceptions;

namespace Package.Domain.Factories
{

    public class PackageEntityBuilder
    {
        protected readonly List<PackageItem> _children
            = new List<PackageItem>();

        public IEnumerable<PackageItem> Children => _children;

        public string? Name { get; set; }
        public string? Id { get; set; }

        public void AddChild(PackageItem item)
        {
            if (_children.Count == 0)
            {
                _children.Add(item);
                return;
            }
            var enumerable = new PackageItemStackEnumerable(_children);
            foreach (var packageItem in enumerable)
                if (packageItem == item)
                    throw new AlreadyExistException();
            _children.Add(item);
        }
    }
}