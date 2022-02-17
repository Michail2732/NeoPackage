using CheckPackage.Base.Binders;
using CheckPackage.Base.Repositories;
using CheckPackage.Configuration.ConfigurationBinders;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Configuration.JsonEntities;
using CheckPackage.Core.Checks;
using CheckPackage.Core.Conditions;
using CheckPackage.Core.Dependencies;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Output;
using CheckPackage.Core.Regexes;
using CheckPackage.Core.Resources;
using CheckPackage.Core.Selectors;
using CheckPackage.PackageBuilding.Builders;
using CheckPackage.PackageBuilding.Rules;
using CheckPackage.PackageOutput.Outputers;
using CheckPackage.PackageOutput.Rules;
using CheckPackage.PackageValidation.Rules;
using CheckPackage.PackageValidation.Validation;
using Microsoft.Extensions.DependencyInjection;
using Package.Abstraction.Services;
using Package.Building.Builders;
using Package.Building.Services;
using Package.Output.Outputers;
using Package.Output.Services;
using Package.Validation.Services;
using Package.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Base
{
    public class CheckPackageBaseModule : ICheckPackageModule
    {
        public void AddDependencies(CheckPackageOptions options)
        {
            options.CommandBinderBuilder
                .AddEntityCheckBinder<EntityParameterValueCheckBinder>()
                .AddEntityCheckBinder<EntityTreeValueCheckBinder>()
                .AddParameterCheckBinder<ParameterLengthCheckBinder>()
                .AddParameterCheckBinder<ParameterTreeValuesCheckBinder>()
                .AddParameterCheckBinder<ParameterValueCheckBinder>()
                .AddEntityConditionBinder<EntityParamValueConditionBinder>()
                .AddEntityConditionBinder<EntityParamValuesConditionBinder>()
                .AddEntityConditionBinder<EntityTreeValuesConditionBinder>()
                .AddParameterExtractBinder<AutoRegexExtractBinder>()
                .AddParameterExtractBinder<RegexExtractBinder>()
                .AddParameterExtractBinder<StaticParameterExtractBinder>()
                .AddParameterExtractBinder<SubstringExtractBinder>()
                .AddParameterExtractBinder<StaticValueExtractBinder>()
                .AddParameterSelectBinder<OneParameterSelectorBinder>();
            options.ConfigurationBinderBuilder
                .AddBinder<AggregateConfigurationJsonBinder, AggregateConfigurationJson>()
                .AddBinder<PackageConfigurationJsonBinder, PackageConfigurationJson>()
                .AddBinder<RepositoryConfigurationJsonBinder, RepositoryConfigurationJson>();
            options.RepositoriesBuilder
                .AddRepository<RegexItemsRepository, RegexItemResource, string>()                
                .AddRepository<StaticParametersRepository, StaticParameterResource, string>()
                .AddRepository<ValueTreesRepository, ValueTreeResource, string>()
                .AddRepository<EntityBuildRulesRepository, EntityBuildRule, string>()
                .AddRepository<EntityCheckRulesRepository, EntityCheckRule, string>()
                .AddRepository<EntityOuputRulesRepository, EntityOutputRule, string>()
                .AddRepository<PackageCheckRulesRepository, PackageCheckRule, string>()
                .AddRepository<PackageOutputRulesRepository, PackageOutputRule, string>()
                .AddRepository<ParameterCheckRulesRepository, ParameterCheckRule, string>()
                .AddRepository<ParameterOuputRulesRepository, ParameterOutputRule, string>();
        }

        public void AddDependencies(IServiceCollection collection)
        {
            // building
            collection.AddSingleton<IPackageBuilder, PackageBuilder>();
            collection.AddSingleton<IEntityBasisBuilder, EntityBasisBuilder>();
            collection.AddSingleton<IEntityBuilder, EntityBuilder>();
            collection.AddSingleton<IEntityGrouper, EntityGrouper>();
            collection.AddSingleton<IPackageBuildingService, PackageBuildingService>();
            // validate
            collection.AddSingleton<IParameterValidator, ParameterValidator>();
            collection.AddSingleton<IUserParameterValidator, UserParameterValidator>();
            collection.AddSingleton<IEntityValidator, EntityValidator>();
            collection.AddSingleton<IPackageValidator, PackageValidator>();
            collection.AddSingleton<IPackageValidationService, PackageValidationService>();
            // outputs
            collection.AddSingleton<IEntityOutputer, EntityOutputer>();
            collection.AddSingleton<IPackageOutputer, PackageOutputer>();
            collection.AddSingleton<IParameterOutputer, ParameterOutputer>();
            collection.AddSingleton<IUserParameterOutputer, UserParameterOutputer>();
            collection.AddSingleton<IPackageOutputService, PackageOutputService>();
            //check_package_core
            collection.AddSingleton<ICheckService, CheckService>();
            collection.AddSingleton<IConditionService, ConditionService>();
            collection.AddSingleton<ISelectorService, SelectorService>();
            collection.AddSingleton<IExtractService, ExtractService>();
            collection.AddSingleton<IOutputService, OutputService>();
            collection.AddSingleton<IRegexService, RegexService>();
            collection.AddSingleton<IPackageContextBuilder, PackageContextBuilder>();
        }
    }
}
