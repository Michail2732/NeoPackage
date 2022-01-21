using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Resourcing.Resources
{

    internal class ResourceStorageInternal
    {
        public ResourceStorageItemInfo Info { get; }
        public object Instance { get; }

        public ResourceStorageInternal(ResourceStorageItemInfo info, object instance)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
        }
    }
}
