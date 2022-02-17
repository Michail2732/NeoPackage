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
    public class SubstringExtractBinder : IParameterExtractCommandBinder
    {
        public ParameterExtractCommand Bind(ParameterExtractJson json)
        {
            var castedJson = (ParameterSubstringExtractJson)json;
            return new NamedGroupRegexExtracter(castedJson.RegexTemplate, castedJson.GroupName);
        }

        public bool CanBind(ParameterExtractJson json) => json is ParameterSubstringExtractJson;        
    }
}
