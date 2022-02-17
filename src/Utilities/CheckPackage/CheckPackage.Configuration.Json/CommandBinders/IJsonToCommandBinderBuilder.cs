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
    public interface IJsonToCommandBinderBuilder
    {
        IJsonToCommandBinderBuilder AddEntityCheckBinder<TBinder>()            
            where TBinder: class, IEntityCheckCommandBinder;
        IJsonToCommandBinderBuilder AddPackageCheckBinder<TBinder>()            
            where TBinder : class, IPackageCheckCommandBinder;
        IJsonToCommandBinderBuilder AddParameterCheckBinder<TBinder>( )            
            where TBinder : class, IParameterCheckCommandBinder;

        IJsonToCommandBinderBuilder AddEntityConditionBinder<TBinder>()            
            where TBinder : class, IEntityConditionCommandBinder;
        IJsonToCommandBinderBuilder AddPackageConditionBinder<TBinder>()            
            where TBinder : class, IPackageConditionCommandBinder;
        IJsonToCommandBinderBuilder AddParameterConditionBinder<TJson, TCommand, TBinder>()            
            where TBinder : class, IParameterConditionCommandBinder;

        IJsonToCommandBinderBuilder AddEntityOutputBinder<TBinder>( )            
            where TBinder : class, IEntityOutputCommandBinder;
        IJsonToCommandBinderBuilder AddPackageOutputBinder<TBinder>()            
            where TBinder : class, IPackageOutputCommandBinder;
        IJsonToCommandBinderBuilder AddParameterOutputBinder<TBinder>()            
            where TBinder : class, IParameterOutputCommandBinder;

        IJsonToCommandBinderBuilder AddParameterExtractBinder<TBinder>()            
            where TBinder : class, IParameterExtractCommandBinder;

        IJsonToCommandBinderBuilder AddParameterSelectBinder<TBinder>()            
            where TBinder : class, IParameterSelectCommandBinder;
    }
}
