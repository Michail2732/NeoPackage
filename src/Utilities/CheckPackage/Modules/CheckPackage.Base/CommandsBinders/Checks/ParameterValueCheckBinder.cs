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
    public class ParameterValueCheckBinder : IParameterCheckCommandBinder
    {
        public ParameterCheckCommand Bind(ParameterCheckJson json)
        {
            var castedJson = (ParameterValueCheckJson)json;
            return new ParameterValueCheck(castedJson.Message, castedJson.Value, castedJson.Operator)
            {
                Inverse = castedJson.Inverse,
                Logic = castedJson.Logic
            };
        }

        public bool CanBind(ParameterCheckJson json) => json is ParameterValueCheckJson;
    }
}
