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
    public class EntityTreeValueCheckBinder : IEntityCheckCommandBinder
    {
        public EntityCheckCommand Bind(EntityCheckJson json)
        {
            var castedJson = (EntityTreeValuesCheckJson)json;
            return new EntityTreeValuesCheck(castedJson.TreeValuesId, castedJson.Message,
                castedJson.ParameterIds, castedJson.ChildChecks == null ? null : new ChildEntitiesCheck(castedJson.ChildChecks.Levels, castedJson.ChildChecks.Logic))
            {
                Inverse = castedJson.Inverse,
                Logic = castedJson.Logic
            };
        }

        public bool CanBind(EntityCheckJson json) => json is EntityTreeValuesCheckJson;
    }
}
