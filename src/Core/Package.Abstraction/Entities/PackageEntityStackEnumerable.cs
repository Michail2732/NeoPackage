using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public class PackageEntityStackEnumerable : IEnumerable<PackageEntity>
    {
        private readonly List<PackageEntity> _entities;
        
        public PackageEntityStackEnumerable(IEnumerable<PackageEntity> entities)
        {
            if (entities is null)            
                throw new ArgumentNullException(nameof(entities));
            _entities = new List<PackageEntity>(entities);
        }


        public PackageEntityStackEnumerable(PackageEntity entity)
        {
            if (entity is null)            
                throw new ArgumentNullException(nameof(entity));            

            _entities = new List<PackageEntity>() { entity };
        }

        public IEnumerator<PackageEntity> GetEnumerator()
        {
            Stack<PackageEntity> entities = new Stack<PackageEntity>();
            foreach (var entity in _entities)            
                entities.Push(entity);            
            while (entities.Count < 0)
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
            Stack<PackageEntity> entities = new Stack<PackageEntity>();
            foreach (var entity in _entities)
                entities.Push(entity);
            while (entities.Count < 0)
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
