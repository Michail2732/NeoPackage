using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Package.Building.Context;
using Package.Building.Pipeline;
using Package.Domain.Factories;
using Package.Infrastructure;



namespace Package.Tests.Unit;

[TestFixture]
public class BuildPackageItemsTests
{
    [Test]
    public void Ctor_Null_ArgumentNullException()
    {
        Assert.Catch<ArgumentNullException>(() =>
        {
            _ = new BuildPackageItems(null!);
        });
    }

    [Test]
    public void Ctor_EmptyFillingRules_BuildPackageItems()
    {
        var itemsBuilder = new BuildPackageItems(Array.Empty<IFillingRule>());
        
        Assert.IsNull(itemsBuilder.Next);
    }

    [Test]
    public void Invoke_EmptyBuildingContext_void()
    {
        var rules = Array.Empty<IFillingRule>();
        var context = new PackageBuildingContext(
            Substitute.For<IConfiguration>(),
            Substitute.For<IStringLocalizer<InfrastructureContext>>(),
            Substitute.For<ILogger<InfrastructureContext>>(),
            Substitute.For<IServiceScopeFactory>()
        );
        var itemsBuilder = new BuildPackageItems(rules);
        
        itemsBuilder.Invoke(context);
        
        Assert.IsNull(itemsBuilder.Next);
        Assert.AreEqual(context.PackageItems.Count, 0);
        Assert.AreEqual(context.InternalPackageItems.Count, 0);
        Assert.AreEqual(context.PackageItemBuilders.Count, 0);
        Assert.AreEqual(context.InternalPackageItemBuilders.Count, 0);
        Assert.AreEqual(context.UserParameters.Count, 0);
    }
    
    [Test]
    public void Invoke_PackageItemBuilder_void()
    {
        var rules = Array.Empty<IFillingRule>();
        var context = new PackageBuildingContext(
            Substitute.For<IConfiguration>(),
            Substitute.For<IStringLocalizer<InfrastructureContext>>(),
            Substitute.For<ILogger<InfrastructureContext>>(),
            Substitute.For<IServiceScopeFactory>()
            );
        var itemBuilder = new PackageItemBuilder();
        context.InternalPackageItemBuilders.Add( itemBuilder);
        var itemsBuilder = new BuildPackageItems(rules);
        
        itemsBuilder.Invoke(context);
        
        Assert.IsNull(itemsBuilder.Next);
        Assert.AreEqual(context.InternalPackageItems.Count, 0);
        Assert.AreEqual(context.PackageItems.Count, 0);
        Assert.AreEqual(context.InternalPackageItemBuilders.Count, 1);
        Assert.AreEqual(context.PackageItemBuilders.Count, 1);
        Assert.AreEqual(context.UserParameters.Count, 0);
        Assert.AreEqual(context.InternalPackageItemBuilders.TakeOne(_ => true), itemBuilder);
    }
    
    [Test]
    public void Invoke_FillingRule_void()
    {
        var rules = new []{ Substitute.For<IFillingRule>()};
        var context = new PackageBuildingContext(
            Substitute.For<IConfiguration>(),
            Substitute.For<IStringLocalizer<InfrastructureContext>>(),
            Substitute.For<ILogger<InfrastructureContext>>(),
            Substitute.For<IServiceScopeFactory>()
        );
        var itemsBuilder = new BuildPackageItems(rules);
        
        itemsBuilder.Invoke(context);
        
        Assert.IsNull(itemsBuilder.Next);
        Assert.AreEqual(context.InternalPackageItems.Count, 0);
        Assert.AreEqual(context.PackageItems.Count, 0);
        Assert.AreEqual(context.InternalPackageItemBuilders.Count, 0);
        Assert.AreEqual(context.PackageItemBuilders.Count, 0);
        Assert.AreEqual(context.UserParameters.Count, 0);
    }
    
    [Test]
    public void Invoke_FillingRuleAndPackageItemBuilder_void()
    {
        var rules = new []{ Substitute.For<IFillingRule>()};
        var context = new PackageBuildingContext(
            Substitute.For<IConfiguration>(),
            Substitute.For<IStringLocalizer<InfrastructureContext>>(),
            Substitute.For<ILogger<InfrastructureContext>>(),
            Substitute.For<IServiceScopeFactory>()
        );
        var itemBuilder = new PackageItemBuilder();
        context.InternalPackageItemBuilders.Add(itemBuilder);
        var itemsBuilder = new BuildPackageItems(rules);

        
        itemsBuilder.Invoke(context);
        
        
        Assert.IsNull(itemsBuilder.Next);
        Assert.AreEqual(context.InternalPackageItems.Count, 0);
        Assert.AreEqual(context.PackageItems.Count, 0);
        Assert.AreEqual(context.InternalPackageItemBuilders.Count, 1);
        Assert.AreEqual(context.PackageItemBuilders.Count, 1);
        Assert.AreEqual(context.UserParameters.Count, 0);
        var itemBuilderFromContext = context.InternalPackageItemBuilders.TakeOne(_ => true);
        Assert.AreEqual(itemBuilderFromContext, itemBuilder);
        rules[0].Received(1).IsMatch(itemBuilder, context);
        rules[0].DidNotReceive().Fill(
            Arg.Any<PackageItemBuilder>(),
            Arg.Any<PackageBuildingContext>());
    }

    [Test]
    public void Invoke_FillingRuleStubAndPackageItemBuilder_void()
    {
        var rules = new []{ Substitute.For<IFillingRule>()};
        var context = new PackageBuildingContext(
            Substitute.For<IConfiguration>(),
            Substitute.For<IStringLocalizer<InfrastructureContext>>(),
            Substitute.For<ILogger<InfrastructureContext>>(),
            Substitute.For<IServiceScopeFactory>()
        );
        var itemBuilder = new PackageItemBuilder();
        context.InternalPackageItemBuilders.Add(itemBuilder);
        rules[0].IsMatch(Arg.Any<PackageItemBuilder>(),
            Arg.Any<PackageBuildingContext>()).Returns(true);
        rules[0].
            When(a => a.Fill(
                Arg.Any<PackageItemBuilder>(),
            Arg.Any<PackageBuildingContext>())).
            Do(_ => itemBuilder.Properties["prop"] = "value");
        var itemsBuilder = new BuildPackageItems(rules);
        
        
        itemsBuilder.Invoke(context);

        
        Assert.IsNull(itemsBuilder.Next);
        Assert.AreEqual(context.PackageItems.Count, 1);
        Assert.AreEqual(context.InternalPackageItems.Count, 1);
        Assert.AreEqual(context.UserParameters.Count, 0);
        Assert.AreEqual(context.PackageItemBuilders.Count, 0);
        Assert.AreEqual(context.InternalPackageItemBuilders.Count, 0);
        var newPackItem = context.InternalPackageItems.TakeOne(_ => true);
        Assert.NotNull(newPackItem);
        Assert.AreEqual(newPackItem.Properties.Count, 1);
        Assert.IsTrue(newPackItem.Properties.ContainsKey("prop"));
        Assert.AreEqual(newPackItem.Properties["prop"], "value");
        rules[0].Received(1).IsMatch(itemBuilder, context);
        rules[0].Received(1).Fill(itemBuilder, context);
    }

    [Test]
    public void NextSet_CycleLink_ArgumentException()
    {
        var rules = Array.Empty<IFillingRule>();
        var itemsBuilder = new BuildPackageItems(rules);

        Assert.Catch<ArgumentException>(() =>
        {
            itemsBuilder.Next = itemsBuilder;
        });
    }
    
    [Test]
    public void NextSet_OtherLink_ArgumentException()
    {
        var rules = Array.Empty<IFillingRule>();
        var itemsBuilder = new BuildPackageItems(rules);
        var next = Substitute.For<IBuildPipelineItem>();
        
        itemsBuilder.Next = next;
        
        Assert.NotNull(itemsBuilder.Next);
        Assert.AreEqual(itemsBuilder.Next, next);
    }
}