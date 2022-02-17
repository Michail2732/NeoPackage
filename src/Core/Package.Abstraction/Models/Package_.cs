using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public class Package_: IRepositoryItem<string>
    {
        public string Id { get; }
        public string Name { get; private set; }
        public IReadOnlyList<Entity_> Entities { get; private set; }        

        public Package_(string id, string name, IReadOnlyList<Entity_> entities)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Entities = entities ?? throw new ArgumentNullException(nameof(entities));
        }

        public int GetCountEntities()
        {
            int count = 0;            
            foreach (var entity in Entities)            
                foreach (var entityItem in new EntityStackEnumerable(entity))                
                    count++;
            return count;
        }
    }
}
