using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public class EntityStackEnumerable : IEnumerable<Entity_>
    {
        private readonly List<Entity_> _entities;
        
        public EntityStackEnumerable(IEnumerable<Entity_> entities)
        {
            if (entities is null)            
                throw new ArgumentNullException(nameof(entities));
            _entities = new List<Entity_>(entities);
        }


        public EntityStackEnumerable(Entity_ entity)
        {
            if (entity is null)            
                throw new ArgumentNullException(nameof(entity));            

            _entities = new List<Entity_>() { entity };
        }

        public IEnumerator<Entity_> GetEnumerator()
        {
            Stack<Entity_> entities = new Stack<Entity_>();
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
            Stack<Entity_> entities = new Stack<Entity_>();
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
