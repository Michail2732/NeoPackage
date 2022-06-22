using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Package.Building.Context;
using Package.Building.Pipeline;
using Package.Domain;
using Package.Domain.Factories;
using Package.Infrastructure;

namespace Package.Tests.Unit;

[TestFixture]
public class GroupPackageItemsTests
{
    [Test]
    public void Ctor_Null_ArgumentNullException()
    {
        Assert.Catch<ArgumentNullException>(() =>
        {
            _ = new GroupPackageItems(null!);
        });
    }
    
    [Test]
    public void Ctor_EmptyArray_GroupPackageItems()
    {
        var grouper = new GroupPackageItems(
            Array.Empty<IGroupingRule>());
        
        Assert.IsNull(grouper.Next);
    }
    
    [Test]
    public void Invoke_EmptyBuildingContext_void()
    {
        var grouper = new GroupPackageItems(
            Array.Empty<IGroupingRule>());
        var context = new PackageBuildingContext(
            Substitute.For<IConfiguration>(),
            Substitute.For<IStringLocalizer<InfrastructureContext>>(),
            Substitute.For<ILogger<InfrastructureContext>>(),
            Substitute.For<IServiceScopeFactory>()
        );
        
        grouper.Invoke(context);
        
        Assert.IsNull(grouper.Next);
        Assert.AreEqual(context.PackageItems.Count, 0);
        Assert.AreEqual(context.InternalPackageItems.Count, 0);
        Assert.AreEqual(context.PackageItemBuilders.Count, 0);
        Assert.AreEqual(context.InternalPackageItemBuilders.Count, 0);
        Assert.AreEqual(context.UserParameters.Count, 0);
    }

    [Test]
    public void Invoke_GroupRule_void()
    {
        var groupRule = Substitute.For<IGroupingRule>();
        var groupRules = new[] { groupRule};
        var grouper = new GroupPackageItems(groupRules);
        var context = new PackageBuildingContext(
            Substitute.For<IConfiguration>(),
            Substitute.For<IStringLocalizer<InfrastructureContext>>(),
            Substitute.For<ILogger<InfrastructureContext>>(),
            Substitute.For<IServiceScopeFactory>()
        );
        
        grouper.Invoke(context);
        
        Assert.IsNull(grouper.Next);
        Assert.AreEqual(context.PackageItems.Count, 0);
        Assert.AreEqual(context.InternalPackageItems.Count, 0);
        Assert.AreEqual(context.PackageItemBuilders.Count, 0);
        Assert.AreEqual(context.InternalPackageItemBuilders.Count, 0);
        Assert.AreEqual(context.UserParameters.Count, 0);
        groupRule.DidNotReceive().IsMatch(Arg.Any<PackageItem>(), context);
        groupRule.DidNotReceive().GetGroupIdentity(Arg.Any<IEnumerable<PackageItem>>(), context);
    }
    
    [Test]
    public void Invoke_PackageItem_void()
    {
        var grouper = new GroupPackageItems(Array.Empty<IGroupingRule>());
        var context = new PackageBuildingContext(
            Substitute.For<IConfiguration>(),
            Substitute.For<IStringLocalizer<InfrastructureContext>>(),
            Substitute.For<ILogger<InfrastructureContext>>(),
            Substitute.For<IServiceScopeFactory>()
        );
        var packageItem = new PackageItemBuilder().Build();
        context.InternalPackageItems.Add(packageItem);
        
        grouper.Invoke(context);
        
        Assert.IsNull(grouper.Next);
        Assert.AreEqual(context.PackageItems.Count, 1);
        Assert.AreEqual(context.InternalPackageItems.Count, 1);
        var packageItemFromContext = context.PackageItems.TakeOne(_ => true);
        Assert.NotNull(packageItemFromContext);
        Assert.AreEqual(packageItemFromContext, packageItem);
        Assert.AreEqual(context.PackageItemBuilders.Count, 0);
        Assert.AreEqual(context.InternalPackageItemBuilders.Count, 0);
        Assert.AreEqual(context.UserParameters.Count, 0);
    }
    
    [Test]
    public void Invoke_GroupRuleAndPackageItem_void()
    {
        var groupRule = Substitute.For<IGroupingRule>();
        var groupRules = new[] { groupRule };
        var grouper = new GroupPackageItems(groupRules);
        var context = new PackageBuildingContext(
            Substitute.For<IConfiguration>(),
            Substitute.For<IStringLocalizer<InfrastructureContext>>(),
            Substitute.For<ILogger<InfrastructureContext>>(),
            Substitute.For<IServiceScopeFactory>()
        );
        var packageItem = new PackageItemBuilder().Build();
        context.InternalPackageItems.Add(packageItem);
        
        grouper.Invoke(context);
        
        Assert.IsNull(grouper.Next);
        Assert.AreEqual(context.PackageItems.Count, 1);
        Assert.AreEqual(context.InternalPackageItems.Count, 1);
        var packageItemFromContext = context.PackageItems.TakeOne(_ => true);
        Assert.AreEqual(packageItem, packageItemFromContext);
        Assert.AreEqual(context.PackageItemBuilders.Count, 0);
        Assert.AreEqual(context.InternalPackageItemBuilders.Count, 0);
        Assert.AreEqual(context.UserParameters.Count, 0);
        groupRule.Received(1).IsMatch(packageItem, context);
        groupRule.DidNotReceive().GetGroupIdentity(Arg.Any<IEnumerable<PackageItem>>(), context);
    }


    
    [TestCase("hello")]
    [TestCase("world")]
    [TestCase("bitches")]
    [TestCase("")]
    public void Invoke_GroupRuleStubAndPackageItem_void(string groupValue)
    {
        var groupRule = Substitute.For<IGroupingRule>();
        var groupRules = new[] { groupRule };
        var grouper = new GroupPackageItems(groupRules);
        var context = new PackageBuildingContext(
            Substitute.For<IConfiguration>(),
            Substitute.For<IStringLocalizer<InfrastructureContext>>(),
            Substitute.For<ILogger<InfrastructureContext>>(),
            Substitute.For<IServiceScopeFactory>()
        );
        var packageItem = new PackageItemBuilder().Build();
        context.InternalPackageItems.Add(packageItem);
        groupRule.IsMatch(packageItem, context).Returns(true);
        groupRule.GetGroupIdentity(Arg.Any<IEnumerable<PackageItem>>(), context).Returns(groupValue);

        grouper.Invoke(context);
        
        Assert.IsNull(grouper.Next);
        Assert.AreEqual(context.PackageItems.Count, 0);
        Assert.AreEqual(context.InternalPackageItems.Count, 0);
        Assert.AreEqual(context.PackageItemBuilders.Count, 1);
        Assert.AreEqual(context.InternalPackageItemBuilders.Count, 1);
        var itemBuilder = context.PackageItemBuilders.TakeOne(_ => true);
        Assert.NotNull(itemBuilder);
        Assert.AreEqual(itemBuilder.Children.Count(), 1);
        Assert.AreEqual(itemBuilder.Children.First(), packageItem);
        Assert.IsTrue(itemBuilder.Properties.ContainsKey(GroupPackageItems.GroupIdProperty));
        Assert.AreEqual(itemBuilder.Properties[GroupPackageItems.GroupIdProperty], groupValue);
        Assert.AreEqual(context.UserParameters.Count, 0);
        groupRule.Received(1).IsMatch(packageItem, context);
        groupRule.Received(1).GetGroupIdentity(Arg.Any<IEnumerable<PackageItem>>(), context);
    }

    [Test]
    public void NextSet_CycleLink_ArgumentException()
    {
        var rules = Array.Empty<IGroupingRule>();
        var grouper = new GroupPackageItems(rules);

        Assert.Catch<ArgumentException>(() =>
        {
            grouper.Next = grouper;
        });
    }
    
    [Test]
    public void NextSet_OtherLink_ArgumentException()
    {
        var rules = Array.Empty<IGroupingRule>();
        var grouper = new GroupPackageItems(rules);
        var next = Substitute.For<IBuildPipelineItem>();
        
        grouper.Next = next;
        
        Assert.NotNull(grouper.Next);
        Assert.AreEqual(grouper.Next, next);
    }
}