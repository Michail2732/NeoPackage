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


    public class EntityParamValueConditionBinder : IEntityConditionCommandBinder
    {
        public EntityConditionCommand Bind(EntityConditionJson json)
        {
            var casted = (EntityParameterValueConditionJson)json;
            return new EntityParameterValueCondition(casted.ParameterId, casted.Value, casted.Operator)
            {
                Inverse = casted.Inverse,
                Logic = casted.Logic,
                Recurse = casted.Recurse
            };
        }

        public bool CanBind(EntityConditionJson json) => json is EntityParameterValueConditionJson;        
    }
}
