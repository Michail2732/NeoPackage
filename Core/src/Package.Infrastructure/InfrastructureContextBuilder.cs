using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;

namespace Package.Infrastructure
{
    public class InfrastructureContextBuilder : IInfrastructureContextBuilder
    {
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<InfrastructureContext> _messages;
        private readonly ILogger<InfrastructureContext> _logger;
        private readonly IServiceScopeFactory _scopedServicesFact;

        public InfrastructureContextBuilder(
            IConfiguration configuration,
            IStringLocalizer<InfrastructureContext> messages,
            ILogger<InfrastructureContext> logger,
            IServiceScopeFactory scopedServicesFact)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _scopedServicesFact = scopedServicesFact ?? throw new ArgumentNullException(nameof(scopedServicesFact));
        }

        public InfrastructureContext Build() => new InfrastructureContext(_configuration, _messages, _logger, _scopedServicesFact);        
    }
}
