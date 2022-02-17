using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Entities
{
    public class PackageContext
    {
        private readonly IServiceScopeFactory _scopedServicesFact;

        public IRepositoriesProvider RepositoryProvider { get; }
        public IConfigurationReader Configuration { get; }
        public IStringLocalizer<PackageContext> Messages { get; }
        public ILogger<PackageContext> Logger { get; }

        public PackageContext(IRepositoriesProvider resourceProvider, IConfigurationReader configuration,
            IStringLocalizer<PackageContext> messages, ILogger<PackageContext> logger,
            IServiceScopeFactory scopedServicesFact)
        {
            RepositoryProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Messages = messages ?? throw new ArgumentNullException(nameof(messages));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _scopedServicesFact = scopedServicesFact ?? throw new ArgumentNullException(nameof(scopedServicesFact));
        }

        public TService GetService<TService>()
        {
            using (var scope = _scopedServicesFact.CreateScope())
            {
                return scope.ServiceProvider.GetService<TService>();
            }
        }


    }
}
