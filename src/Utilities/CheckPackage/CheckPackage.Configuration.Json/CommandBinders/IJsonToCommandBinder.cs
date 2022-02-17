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
    public interface IEntityCheckCommandBinder        
    {
        bool CanBind(EntityCheckJson json);
        EntityCheckCommand Bind(EntityCheckJson json);
    }

    public interface IPackageCheckCommandBinder
    {
        bool CanBind(PackageCheckJson json);
        PackageCheckCommand Bind(PackageCheckJson json);
    }

    public interface IParameterCheckCommandBinder
    {
        bool CanBind(ParameterCheckJson json);
        ParameterCheckCommand Bind(ParameterCheckJson json);
    }

    public interface IEntityConditionCommandBinder
    {
        bool CanBind(EntityConditionJson json);
        EntityConditionCommand Bind(EntityConditionJson json);
    }

    public interface IPackageConditionCommandBinder
    {
        bool CanBind(PackageConditionJson json);
        PackageConditionCommand Bind(PackageConditionJson json);
    }

    public interface IParameterConditionCommandBinder
    {
        bool CanBind(ParameterConditionJson json);
        ParameterConditionCommand Bind(ParameterConditionJson json);
    }

    public interface IParameterExtractCommandBinder
    {
        bool CanBind(ParameterExtractJson json);
        ParameterExtractCommand Bind(ParameterExtractJson json);
    }

    public interface IParameterSelectCommandBinder
    {
        bool CanBind(ParametersSelectorJson json);
        ParameterSelectCommand Bind(ParametersSelectorJson json);
    }

    public interface IEntityOutputCommandBinder
    {
        bool CanBind(EntityOutputJson json);
        EntityOutputCommand Bind(EntityOutputJson json);
    }

    public interface IPackageOutputCommandBinder
    {
        bool CanBind(PackageOutputJson json);
        PackageOutputCommand Bind(PackageOutputJson json);
    }

    public interface IParameterOutputCommandBinder
    {
        bool CanBind(ParameterOutputJson json);
        ParameterOutputCommand Bind(ParameterOutputJson json);
    }
}
