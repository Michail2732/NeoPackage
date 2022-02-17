using System;
using System.Collections.Generic;
using System.Text;
using CheckPackage.Base.Commands;
using CheckPackage.Base.Configuration;
using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Conditions;

namespace CheckPackage.Base.Binders
{
    public class EntityParamValuesConditionBinder : IEntityConditionCommandBinder
    {
        public EntityConditionCommand Bind(EntityConditionJson json)
        {
            var casted = (EntityParameterValuesConditionJson)json;
            return new EntityParameterValuesCondition(casted.ParameterId, casted.Values, casted.IsContains)
            {
                Inverse = casted.Inverse,
                Logic = casted.Logic,
                Recurse = casted.Recurse
            };
        }

        public bool CanBind(EntityConditionJson json) => json is EntityParameterValuesConditionJson;
    }
}
