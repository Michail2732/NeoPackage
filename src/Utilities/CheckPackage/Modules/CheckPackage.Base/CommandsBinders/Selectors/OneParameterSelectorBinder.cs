using CheckPackage.Base.Commands;
using CheckPackage.Base.Configuration;
using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Selectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base.Binders
{
    public class OneParameterSelectorBinder : IParameterSelectCommandBinder
    {
        public ParameterSelectCommand Bind(ParametersSelectorJson json)
        {
            var castedJson = (OneParameterSelectorJson)json;
            return new OneParameterSelector(castedJson.ParameterId, castedJson.IsUserParameter);
        }

        public bool CanBind(ParametersSelectorJson json) => json is OneParameterSelectorJson;
    }
}
