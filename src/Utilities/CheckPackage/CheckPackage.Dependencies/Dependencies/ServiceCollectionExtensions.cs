using CheckPackage.Core.Building;
using CheckPackage.Core.Checks;
using CheckPackage.Core.Condition;
using CheckPackage.Core.Extracts;
using CheckPackage.Core.Regex;
using CheckPackage.Core.Validation;
using Microsoft.Extensions.DependencyInjection;
using Package.Building.Builders;
using Package.Building.Engine;
using Package.Localization;
using Package.Validation.Engine;
using Package.Validation.Validators;
using System;
using System.Collections.Generic;

namespace CheckPackage.Core.Dependencies
{
    public static class ServiceCollectionExtensions
    {
        public static void UseCheckPackage(this IServiceCollection collection, Action<CheckPackageOptions> configuration)
        {
            collection.AddSingleton<IPackageBuilder, PackageBuilder>();
            collection.AddSingleton<IPackageEntityBasisBuilder, PackageEntityBasisBuilder>();
            collection.AddSingleton<IPackageEntityBuilder, PackageEntityBuilder>();
            collection.AddSingleton<IEntityGrouper, PackageEntityGrouper>();
            collection.AddSingleton<IPackageBuildingEngine, PackageBuildingEngine>();

            collection.AddSingleton<IEntityParameterValidator, EntityParameterValidator>();
            collection.AddSingleton<IEntityUserParameterValidator, EntityUserParameterValidator>();
            collection.AddSingleton<IPackageEntityValidator, PackageEntityValidator>();
            collection.AddSingleton<IPackageValidator, PackageValidator>();            
            collection.AddSingleton<IPackageValidationEngine, PackageValidationEngine>();

            collection.AddSingleton<MessagesService>();
            configuration.Invoke(new CheckPackageOptions(collection));            
        }



        public static void UseCheckPackage(this IServiceCollection collection, IEnumerable<ICheckPackageModule> modules)
        {
            collection.AddSingleton<IPackageBuilder, PackageBuilder>();
            collection.AddSingleton<IPackageEntityBasisBuilder, PackageEntityBasisBuilder>();
            collection.AddSingleton<IPackageEntityBuilder, PackageEntityBuilder>();
            collection.AddSingleton<IEntityGrouper, PackageEntityGrouper>();
            collection.AddSingleton<IPackageBuildingEngine, PackageBuildingEngine>();

            collection.AddSingleton<IEntityParameterValidator, EntityParameterValidator>();
            collection.AddSingleton<IEntityUserParameterValidator, EntityUserParameterValidator>();
            collection.AddSingleton<IPackageEntityValidator, PackageEntityValidator>();
            collection.AddSingleton<IPackageValidator, PackageValidator>();
            collection.AddSingleton<IPackageValidationEngine, PackageValidationEngine>();

            collection.AddSingleton<MessagesService>();
            var options = new CheckPackageOptions(collection);
            foreach (var module in modules)
            {
                module.AddDependencies(options);
                module.AddDependencies(collection);
            }            
        }
    }
}
