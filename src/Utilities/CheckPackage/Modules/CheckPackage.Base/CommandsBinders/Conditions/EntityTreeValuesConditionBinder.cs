using CheckPackage.Base.Commands;
using CheckPackage.Base.Configuration;
using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Conditions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Binders
{
    public class EntityTreeValuesConditionBinder : IEntityConditionCommandBinder
    {
        public EntityConditionCommand Bind(EntityConditionJson json)
        {
            var casted = (EntityTreeValuesConditionJson)json;
            return new EntityTreeValuesCondition(casted.TreeValuesId, casted.ParameterIds)
            {
                Inverse = casted.Inverse,
                Logic = casted.Logic,
                Recurse = casted.Recurse
            };
        }

        public bool CanBind(EntityConditionJson json) => json is EntityTreeValuesConditionJson;        
    }
}
