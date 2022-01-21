using Package.Abstraction.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Package.Building.Builders
{
    public class PackageEntitiesGroup : IGrouping<GroupKey, PackageEntity>
    {
        private IEnumerable<PackageEntity> _entities;
        public GroupKey Key { get; }

        public PackageEntitiesGroup(IEnumerable<PackageEntity> entities, GroupKey key)
        {
            _entities = entities ?? throw new ArgumentNullException(nameof(entities));
            Key = key;
        }

        public IEnumerator<PackageEntity> GetEnumerator() => _entities.GetEnumerator();        
        IEnumerator IEnumerable.GetEnumerator() => _entities.GetEnumerator();
    }
}
