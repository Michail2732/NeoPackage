using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Package.Infrastructure
{
    public class InfrastructureContext
    {
        protected readonly IServiceScopeFactory _scopedServicesFact;
        
        public IConfiguration Configuration { get; }
        public IStringLocalizer<InfrastructureContext> Messages { get; }
        public ILogger<InfrastructureContext> Logger { get; }

        public InfrastructureContext(
            IConfiguration configuration,
            IStringLocalizer<InfrastructureContext> messages,
            ILogger<InfrastructureContext> logger,
            IServiceScopeFactory scopedServicesFact)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Messages = messages ?? throw new ArgumentNullException(nameof(messages));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _scopedServicesFact = scopedServicesFact ?? throw new ArgumentNullException(nameof(scopedServicesFact));
        }

        public TService GetService<TService>() where  TService: class
        {
            using (var scope = _scopedServicesFact.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<TService>();
                if (service == null)
                    throw new ArgumentException($"Service {typeof(TService)} not found");
                return service;
            }
        }


    }
}
