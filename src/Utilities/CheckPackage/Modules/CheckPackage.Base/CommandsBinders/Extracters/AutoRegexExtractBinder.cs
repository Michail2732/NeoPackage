using System;
using System.Collections.Generic;
using System.Text;
using CheckPackage.Base.Commands;
using CheckPackage.Base.Configuration;
using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Extractors;

namespace CheckPackage.Base.Binders
{
    public class AutoRegexExtractBinder : IParameterExtractCommandBinder
    {
        public ParameterExtractCommand Bind(ParameterExtractJson json)
        {
            var castedJson = (ParameterAutoRegexExtractJson)json;
            return new AutoCompositeRegexExtractor(castedJson.ParameterId);
        }

        public bool CanBind(ParameterExtractJson json) => json is ParameterAutoRegexExtractJson;
    }
}
