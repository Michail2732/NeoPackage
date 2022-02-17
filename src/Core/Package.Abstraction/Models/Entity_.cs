using Package.Abstraction.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Package.Abstraction.Entities
{
    public class Entity_: IRepositoryItem<string>
    {        
        public string Id { get; }        
        public uint Level { get; private set; }
        public string Name { get; }        

        private readonly List<Entity_> _children;
        public IReadOnlyList<Entity_> Children => _children;

        private readonly Dictionary<string, string> _parameters;
        public IReadOnlyDictionary<string, string> Parameters => _parameters;

        private readonly Dictionary<string, UserParameter_> _userParameters;
        public IReadOnlyDictionary<string, UserParameter_> UserParameters => _userParameters;

        public Entity_(string id, string name, 
            List<Entity_> children, Dictionary<string, string> parameters, 
            Dictionary<string, UserParameter_> userParameters)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));            
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _children = children ?? throw new ArgumentNullException(nameof(children));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _userParameters = userParameters ?? throw new ArgumentNullException(nameof(userParameters));
            SetLevels();
        }

        public EntityInfo GetEntityInfo() => new EntityInfo(Id, Name, Level, _children);
        public EntityParametersInfo GetParametersInfo() => new EntityParametersInfo(
            _parameters, _userParameters);

        private void SetLevels()
        {
            uint height = this.GetHeigtEntity() - 1;
            List<Entity_> items = new List<Entity_> { this};
            IEnumerable<Entity_> childs = null;
            while (items.Count > 0)
            {
                items.ForEach(a => a.Level = height);
                childs = items.SelectMany(a => a.Children);
                items.Clear();
                items.AddRange(childs);
                height--;
            }            
        }
    }
}
