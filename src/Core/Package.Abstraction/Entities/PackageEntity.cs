using Package.Abstraction.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Package.Abstraction.Entities
{
    public class PackageEntity: IEntity<string>
    {        
        public string Id { get; }        
        public uint Level { get; private set; }
        public string Name { get; }        

        private readonly List<PackageEntity> _children;
        public IReadOnlyList<PackageEntity> Children => _children;

        private readonly Dictionary<string, string> _parameters;
        public IReadOnlyDictionary<string, string> Parameters => _parameters;

        private readonly Dictionary<string, UserParameter> _userParameters;
        public IReadOnlyDictionary<string, UserParameter> UserParameters => _userParameters;

        public PackageEntity(string id, string name, 
            List<PackageEntity> children, Dictionary<string, string> parameters, 
            Dictionary<string, UserParameter> userParameters)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));            
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _children = children ?? throw new ArgumentNullException(nameof(children));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _userParameters = userParameters ?? throw new ArgumentNullException(nameof(userParameters));
            SetLevels();
        }

        public PackageEntityInfo GetEntityInfo() => new PackageEntityInfo(Id, Name, Level, _children);
        public PackageEntityParametersInfo GetParametersInfo() => new PackageEntityParametersInfo(
            _parameters, _userParameters);

        private void SetLevels()
        {
            uint height = this.GetHeigtEntity() - 1;
            List<PackageEntity> items = new List<PackageEntity> { this};
            IEnumerable<PackageEntity> childs = null;
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
