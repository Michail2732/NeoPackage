using Microsoft.Extensions.Configuration;
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
        public IResourceStoragesProvider ResourceProvider { get; }
        public IConfiguration Configuration { get; }
        public IStringLocalizer<PackageContext> Messages { get; }
        public ILogger<PackageContext> Logger { get; }

        public PackageContext(IResourceStoragesProvider resourceProvider, IConfiguration configuration,
            IStringLocalizer<PackageContext> messages, ILogger<PackageContext> logger)
        {
            ResourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Messages = messages ?? throw new ArgumentNullException(nameof(messages));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
