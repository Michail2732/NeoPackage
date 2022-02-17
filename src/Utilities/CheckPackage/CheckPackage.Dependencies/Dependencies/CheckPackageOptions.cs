using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using Package.Configuration.Services;
using Package.Configuration.Dependency;
using Package.Repository.Services;
using Package.Repository.Dependency;

namespace CheckPackage.Core.Dependencies
{
    public class CheckPackageOptions
    {                
        public RepositoriesProviderBuilder RepositoriesBuilder { get; }
        public JsonConfigurationServiceBuilder ConfigurationBinderBuilder { get; }
        public IJsonToCommandBinderBuilder CommandBinderBuilder { get; }

        public CheckPackageOptions(IServiceCollection collection)
        {
            CommandBinderBuilder = collection.AddCommandBinder();
            RepositoriesBuilder = collection.AddRepositories();
            ConfigurationBinderBuilder = collection.AddJsonConfiguration();
        }
    }
}
