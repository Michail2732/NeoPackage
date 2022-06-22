using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Package.Utility.Collection;
using Package.Domain;
using Package.Domain.Factories;
using Package.Infrastructure;

namespace Package.Building.Context
{
    public class PackageBuildingContext: InfrastructureContext
    {
        public Dictionary<string, object> UserParameters { get; internal set; } = new Dictionary<string, object>();
        
        
        internal readonly Bag<PackageItem> InternalPackageItems = new Bag<PackageItem>(); 
        internal readonly Bag<PackageItemBuilder> InternalPackageItemBuilders = new Bag<PackageItemBuilder>();

        public ITakeOnlyBag<PackageItem> PackageItems => InternalPackageItems;
        public ITakeOnlyBag<PackageItemBuilder> PackageItemBuilders => InternalPackageItemBuilders;

        public PackageBuildingContext(
            IConfiguration configuration,
            IStringLocalizer<InfrastructureContext> messages,
            ILogger<InfrastructureContext> logger,
            IServiceScopeFactory scopedServicesFact) : 
            base(configuration, messages, logger, scopedServicesFact)
        {
        }
    }
}