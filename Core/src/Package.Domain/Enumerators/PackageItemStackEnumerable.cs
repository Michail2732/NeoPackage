using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Package.Domain.Enumerators
{
    public class PackageItemStackEnumerable : IEnumerable<PackageItem>
    {
        private readonly List<PackageItem> _entities;
        
        public PackageItemStackEnumerable(IEnumerable<PackageItem> entities)
        {
            if (entities is null)            
                throw new ArgumentNullException(nameof(entities));
            _entities = new List<PackageItem>(entities);
        }


        public PackageItemStackEnumerable(PackageItem packageItem)
        {
            if (packageItem is null)            
                throw new ArgumentNullException(nameof(packageItem));            

            _entities = new List<PackageItem>() { packageItem };
        }

        public IEnumerator<PackageItem> GetEnumerator()
        {
            Stack<PackageItem> entities = new Stack<PackageItem>();
            foreach (var entity in _entities)            
                entities.Push(entity);            
            while (entities.Count != 0)
            {
                var lclEntity = entities.Pop();
                yield return lclEntity;
                foreach (var child in lclEntity.Children)                
                    entities.Push(child);                
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Stack<PackageItem> entities = new Stack<PackageItem>();
            foreach (var entity in _entities)
                entities.Push(entity);
            while (entities.Count != 0)
            {
                var lclEntity = entities.Pop();
                yield return lclEntity;
                foreach (var child in lclEntity.Children)
                    entities.Push(child);
            }
            yield break;
        }
    }
}
