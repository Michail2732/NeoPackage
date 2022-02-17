using CheckPackage.Base.Commands;
using CheckPackage.Base.Configuration;
using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Checks;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Binders
{
    public class EntityParameterValueCheckBinder : IEntityCheckCommandBinder
    {
        public EntityCheckCommand Bind(EntityCheckJson json)
        {
            var castedJson = (EntityParameterValueCheckJson)json;
            return new EntityParameterValueCheck(castedJson.ParameterId, castedJson.Message, castedJson.Value,
                castedJson.Operator, castedJson.ChildChecks == null ? null : new ChildEntitiesCheck(castedJson.ChildChecks.Levels, castedJson.ChildChecks.Logic))
            {
                Inverse = castedJson.Inverse,
                Logic = castedJson.Logic
            };
        }

        public bool CanBind(EntityCheckJson json) => json is EntityParameterValueCheckJson;        
    }
}
