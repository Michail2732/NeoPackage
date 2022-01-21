using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Building.Builders
{
    public readonly struct EntityBuildingResult
    {
        public readonly IReadOnlyDictionary<string, string>? Parameters;
        public readonly IReadOnlyDictionary<string, UserParameter>? UserParameters;
        public readonly string Name;
        public readonly string Id;

        public EntityBuildingResult(IReadOnlyDictionary<string, string> parameters, 
            IReadOnlyDictionary<string, UserParameter> userParameters, string name, string id)
        {
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            UserParameters = userParameters ?? throw new ArgumentNullException(nameof(userParameters));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public EntityBuildingResult(IReadOnlyDictionary<string, string> parameters, 
            string name, string id) : this(name, id)
        {
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));                                                
        }

        public EntityBuildingResult(string name, string id)
        {            
            Name = name ?? throw new ArgumentNullException(nameof(name));            
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Parameters = null;
            UserParameters = null;
        }
    }
}
