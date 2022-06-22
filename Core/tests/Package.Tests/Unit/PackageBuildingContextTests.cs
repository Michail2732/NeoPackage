using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Package.Building.Context;
using NSubstitute;
using Package.Infrastructure;

namespace Package.Tests.Unit;

[TestFixture]
public class PackageBuildingContextTests
{
    [Test]
    public void Ctor_SomeNull_ArgumentNullException()
    {
        var configuration = Substitute.For<IConfiguration>();
        var messages = Substitute.For<IStringLocalizer<InfrastructureContext>>();
        var logger = Substitute.For<ILogger<InfrastructureContext>>();
        var scopeService = Substitute.For<IServiceScopeFactory>();
        
        for (int i = 0; i < 14; i++)
        {
            var vector = new BitVector32(i);
            Assert.Catch<ArgumentNullException>(() =>
            {
                _ = new PackageBuildingContext(
                    (vector[1] ? configuration : null)!,
                    (vector[2] ? messages : null)!,
                    (vector[4] ? logger : null)!,
                    (vector[8] ? scopeService : null)!
                );
            });
        }
    }
    
    
    [Test]
    public void Ctor_SuccessCase_PackageBuildingContext()
    {
        var configuration = Substitute.For<IConfiguration>();
        var messages = Substitute.For<IStringLocalizer<InfrastructureContext>>();
        var logger = Substitute.For<ILogger<InfrastructureContext>>();
        var scopeService = Substitute.For<IServiceScopeFactory>();
        
        var context = new PackageBuildingContext(
            configuration,
            messages,
            logger,
            scopeService
        );


        Assert.IsNotNull(context.PackageItems);
        Assert.AreEqual(context.PackageItems.Count, 0);
        Assert.IsNotNull(context.UserParameters);
        Assert.AreEqual(context.UserParameters.Count, 0);
        Assert.IsNotNull(context.PackageItemBuilders);
        Assert.AreEqual(context.PackageItemBuilders.Count, 0);
        Assert.IsNotNull(context.Configuration);
        Assert.IsNotNull(context.Logger);
        Assert.IsNotNull(context.Messages);
    }
    
    
    
}