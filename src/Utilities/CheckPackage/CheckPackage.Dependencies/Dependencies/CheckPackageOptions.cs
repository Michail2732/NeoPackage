using CheckPackage.Core.Checks;
using CheckPackage.Core.Condition;
using CheckPackage.Core.Extracts;
using System;
using Microsoft.Extensions.DependencyInjection;
using Package.Resourcing.Resources;
using Package.Resourcing.Dependency;

namespace CheckPackage.Core.Dependencies
{
    public class CheckPackageOptions
    {
        public IServiceCollection Services { get; }
        public ConditionResolverBuilder Conditions { get; }
        public ExtractersBuilder Extracters { get; }
        public ChecksBuilder Checks { get; }
        public ResourcesBuilder Resources { get; }        

        public CheckPackageOptions(IServiceCollection collection)
        {            
            Services = collection;
            Conditions = collection.AddConditions();
            Extracters = collection.AddExtracters();
            Checks = collection.AddChecks();
            Resources = collection.AddResources();
        }
    }
}
