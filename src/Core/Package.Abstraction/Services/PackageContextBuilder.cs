using Microsoft.Extensions.Configuration;
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
        private readonly IResourceStoragesProvider _resourceProvider;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<PackageContext> _messages;
        private readonly ILogger<PackageContext> _logger;

        public PackageContextBuilder(IResourceStoragesProvider resourceProvider, IConfiguration configuration,
            IStringLocalizer<PackageContext> messages, ILogger<PackageContext> logger)
        {
            _resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public PackageContext Build() => new PackageContext(_resourceProvider, _configuration, _messages, _logger);        
    }
}
