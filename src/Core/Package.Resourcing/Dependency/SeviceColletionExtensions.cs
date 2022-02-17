using Microsoft.Extensions.DependencyInjection;
using Package.Repository.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Repository.Dependency
{
    public static class SeviceColletionExtensions
    {
        public static RepositoriesProviderBuilder AddRepositories(this IServiceCollection collection)
        {            
            var provider = collection.BuildServiceProvider();
            RepositoriesProviderBuilder? repositoriesBuilder = provider.GetService<RepositoriesProviderBuilder>();
            if (repositoriesBuilder == null)
            {
                repositoriesBuilder = new RepositoriesProviderBuilder(collection);
                collection.AddSingleton(repositoriesBuilder);
            }
            return repositoriesBuilder;
        }

    }
}
