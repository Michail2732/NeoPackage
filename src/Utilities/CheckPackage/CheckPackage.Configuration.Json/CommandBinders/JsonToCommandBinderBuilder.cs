using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Checks;
using CheckPackage.Core.Conditions;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Output;
using CheckPackage.Core.Selectors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Binder
{
    public class JsonToCommandBinderBuilder : IJsonToCommandBinderBuilder
    {
        private readonly IServiceCollection _services;        

        public JsonToCommandBinderBuilder(IServiceCollection services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _services.AddSingleton(Build);
        }

        IJsonToCommandBinderBuilder IJsonToCommandBinderBuilder.AddEntityCheckBinder<TBinder>()
        {
            _services.AddSingleton<IEntityCheckCommandBinder, TBinder>();
            return this;
        }

        IJsonToCommandBinderBuilder IJsonToCommandBinderBuilder.AddEntityConditionBinder<TBinder>()
        {
            _services.AddSingleton<IEntityConditionCommandBinder, TBinder>();
            return this;
        }

        IJsonToCommandBinderBuilder IJsonToCommandBinderBuilder.AddEntityOutputBinder<TBinder>()
        {
            _services.AddSingleton<IEntityOutputCommandBinder, TBinder>();
            return this;
        }

        IJsonToCommandBinderBuilder IJsonToCommandBinderBuilder.AddPackageCheckBinder<TBinder>()
        {
            _services.AddSingleton<IPackageCheckCommandBinder, TBinder>();
            return this;
        }

        IJsonToCommandBinderBuilder IJsonToCommandBinderBuilder.AddPackageConditionBinder<TBinder>()
        {
            _services.AddSingleton<IPackageConditionCommandBinder, TBinder>();
            return this;
        }

        IJsonToCommandBinderBuilder IJsonToCommandBinderBuilder.AddPackageOutputBinder<TBinder>()
        {
            _services.AddSingleton<IPackageOutputCommandBinder, TBinder>();
            return this;
        }

        IJsonToCommandBinderBuilder IJsonToCommandBinderBuilder.AddParameterCheckBinder<TBinder>()
        {
            _services.AddSingleton<IParameterCheckCommandBinder, TBinder>();
            return this;
        }

        IJsonToCommandBinderBuilder IJsonToCommandBinderBuilder.AddParameterConditionBinder<TJson, TCommand, TBinder>()
        {
            _services.AddSingleton<IParameterConditionCommandBinder, TBinder>();
            return this;
        }

        IJsonToCommandBinderBuilder IJsonToCommandBinderBuilder.AddParameterExtractBinder<TBinder>()
        {
            _services.AddSingleton<IParameterExtractCommandBinder, TBinder>();
            return this;
        }

        IJsonToCommandBinderBuilder IJsonToCommandBinderBuilder.AddParameterOutputBinder<TBinder>()
        {
            _services.AddSingleton<IParameterOutputCommandBinder, TBinder>();
            return this;
        }

        IJsonToCommandBinderBuilder IJsonToCommandBinderBuilder.AddParameterSelectBinder<TBinder>()
        {
            _services.AddSingleton<IParameterSelectCommandBinder, TBinder>();
            return this;
        }

        private IJsonToCommandBindService Build(IServiceProvider provider)
        {            
            var entityCheckBinders = provider.GetServices<IEntityCheckCommandBinder>();
            var packageCheckBinders = provider.GetServices<IPackageCheckCommandBinder>();
            var parameterCheckBinders = provider.GetServices<IParameterCheckCommandBinder>();
            var entityConditionBinders = provider.GetServices<IEntityConditionCommandBinder>();
            var packageConditionsBinders = provider.GetServices<IPackageConditionCommandBinder>();
            var parameterConditionBinders = provider.GetServices<IParameterConditionCommandBinder>();
            var parameterExtractBinders = provider.GetServices<IParameterExtractCommandBinder>();
            var entityOutputBinders = provider.GetServices<IEntityOutputCommandBinder>();
            var packageOutputBinders = provider.GetServices<IPackageOutputCommandBinder>();
            var parameterOutputBinders = provider.GetServices<IParameterOutputCommandBinder>();
            var parameterSelectBinders = provider.GetServices<IParameterSelectCommandBinder>();
            BinderServiceBuildArgs binders = new  BinderServiceBuildArgs(entityCheckBinders, packageCheckBinders, parameterCheckBinders,
                entityConditionBinders, packageConditionsBinders, parameterConditionBinders, parameterExtractBinders,
                entityOutputBinders, packageOutputBinders, parameterOutputBinders, parameterSelectBinders);
            return new JsonToCommandBindService(binders);
        }
    }
}
