using CheckPackage.Base.Commands;
using CheckPackage.Base.Configuration;
using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Extractors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Binders
{
    public class StaticValueExtractBinder : IParameterExtractCommandBinder
    {
        public ParameterExtractCommand Bind(ParameterExtractJson json)
        {
            var castedJson = (ParameterStaticValueExtractJson)json;
            return new StaticValueExtracter(castedJson.ParameterValue, castedJson.ParameterId);
        }

        public bool CanBind(ParameterExtractJson json) => json is ParameterStaticValueExtractJson;        
    }
}
