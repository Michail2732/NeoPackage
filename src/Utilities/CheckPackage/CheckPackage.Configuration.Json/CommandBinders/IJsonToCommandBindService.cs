using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Checks;
using CheckPackage.Core.Conditions;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Output;
using CheckPackage.Core.Selectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Binder
{
    public interface IJsonToCommandBindService
    {
        EntityCheckCommand Bind(EntityCheckJson json);
        PackageCheckCommand Bind(PackageCheckJson json);
        ParameterCheckCommand Bind(ParameterCheckJson json);

        EntityConditionCommand Bind(EntityConditionJson json);
        PackageConditionCommand Bind(PackageConditionJson json);
        ParameterConditionCommand Bind(ParameterConditionJson json);

        EntityOutputCommand Bind(EntityOutputJson json);
        PackageOutputCommand Bind(PackageOutputJson json);
        ParameterOutputCommand Bind(ParameterOutputJson json);

        ParameterExtractCommand Bind(ParameterExtractJson json);

        ParameterSelectCommand Bind(ParametersSelectorJson json);        
    }
}
