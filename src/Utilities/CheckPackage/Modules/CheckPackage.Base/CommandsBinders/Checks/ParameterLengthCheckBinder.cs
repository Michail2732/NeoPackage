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
    public class ParameterLengthCheckBinder : IParameterCheckCommandBinder
    {
        public ParameterCheckCommand Bind(ParameterCheckJson json)
        {
            var castedJson = (ParameterLengthCheckJson)json;
            return new ParameterLengthCheck(castedJson.Message, castedJson.MinLength, castedJson.MaxLength)
            {
                Inverse = castedJson.Inverse,                
                Logic = castedJson.Logic
            };

        }

        public bool CanBind(ParameterCheckJson json) => json is ParameterLengthCheckJson;
    }
}
