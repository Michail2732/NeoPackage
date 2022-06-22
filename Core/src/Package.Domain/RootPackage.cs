using System;
using System.Collections.Generic;
using System.Linq;
using Package.Domain.Enumerators;

namespace Package.Domain
{
    public class RootPackage: IPackageEntity
    {
        public string Id { get; }
        public string Name { get; private set; }
        public IReadOnlyList<PackageItem> Items { get; private set; }        

        internal RootPackage(
            string id,
            string name,
            IReadOnlyList<PackageItem> entities)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Items = entities ?? throw new ArgumentNullException(nameof(entities));
        }

        public int GetCountEntities()
        {
            return Items.SelectMany(entity => 
                new PackageItemStackEnumerable(entity)).Count();
        }

        public IEnumerable<PackageItem> GetStackEnumerable() => new PackageItemStackEnumerable(Items);
    }
}
