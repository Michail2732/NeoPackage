using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Package.Abstraction.Services
{
    public class PackageContextBuilder : IPackageContextBuilder
    {
        private readonly IRepositoriesProvider _repositoriesProvider;
        private readonly IConfigurationReader _configuration;
        private readonly IStringLocalizer<PackageContext> _messages;
        private readonly ILogger<PackageContext> _logger;
        private readonly IServiceScopeFactory _scopedServicesFact;

        public PackageContextBuilder(IRepositoriesProvider repositoriesProvider, IConfigurationReader configuration,
            IStringLocalizer<PackageContext> messages, ILogger<PackageContext> logger, IServiceScopeFactory scopedServicesFact)
        {
            _repositoriesProvider = repositoriesProvider ?? throw new ArgumentNullException(nameof(repositoriesProvider));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _scopedServicesFact = scopedServicesFact ?? throw new ArgumentNullException(nameof(scopedServicesFact));
        }

        public PackageContext Build() => new PackageContext(_repositoriesProvider, _configuration, _messages, _logger, _scopedServicesFact);        
    }
}
