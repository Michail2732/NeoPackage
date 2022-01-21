using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Resourcing.Resources
{
    internal class ResourceStorageItemInfo
    {
        public Type ItemType { get; }
        public Type KeyType { get; }

        public ResourceStorageItemInfo(Type itemType, Type keyType)
        {
            ItemType = itemType ?? throw new ArgumentNullException(nameof(itemType));
            KeyType = keyType ?? throw new ArgumentNullException(nameof(keyType));
        }
    }
}
