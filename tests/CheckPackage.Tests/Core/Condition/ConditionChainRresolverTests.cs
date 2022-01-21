using CheckPackage.Core.Abstract;
using CheckPackage.Core.Condition;
using CheckPackage.Core.Package;
using CheckPackage.Core.Regex;
using CheckPackage.Tests.Core.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Condition
{
    //[TestFixture]
    //public class ConditionChainRresolverTests
    //{        
    //    [Test]        
    //    public void Resolve_AnyParameterNull_ArgumentNullException()
    //    {
    //        var instance = CreateInstance();
    //        var condition = new StubConditionInfo();
    //        var context = CreateContext();

    //        Assert.Catch<ArgumentNullException>(() => instance.Resolve(condition, null));
    //        Assert.Catch<ArgumentNullException>(() => instance.Resolve(null, context));
    //        Assert.Catch<ArgumentNullException>(() => instance.Resolve(null, null));
    //    }

    //    [Test]
    //    public void Resolve_OneConditionTrue_true()
    //    {
    //        var instance = CreateInstance();
    //        var condition = new EqualCondition("parameter_id", "one param", LogicalOperator.and);
    //        var context = CreateContext(new PackageEntity(1, new StubEntityAggregator(), 
    //            new Dictionary<string, ParameterValue> { { "parameter_id", new ParameterValue("one param") } }));

    //        bool result = instance.Resolve(condition, context);

    //        Assert.IsTrue(result);
    //    }

    //    [Test]
    //    public void Resolve_OneConditionFalse_true()
    //    {
    //        var instance = CreateInstance();
    //        var condition = new EqualCondition("parameter_id", "one param", LogicalOperator.and);
    //        var context = CreateContext(new PackageEntity(1, new StubEntityAggregator(),
    //            new Dictionary<string, ParameterValue> { { "parameter_id", new ParameterValue("one parameter") } }));

    //        bool result = instance.Resolve(condition, context);

    //        Assert.IsFalse(result);
    //    }

    //    [Test]
    //    public void Resolve_TwoConditionOr_bool()
    //    {
    //        var instance = CreateInstance();
    //        var condition1 = new EqualCondition("parameter_id1", "one param1", LogicalOperator.and);
    //        var condition2 = new EqualCondition("parameter_id2", "one param2", LogicalOperator.or);
    //        condition1.SetNext(condition2);
    //        for (int i = 0; i < 4; i++)
    //        {                                
    //            var context = CreateContext(new PackageEntity(1, new StubEntityAggregator(),
    //                new Dictionary<string, ParameterValue> {
    //                    { "parameter_id1", new ParameterValue((i & 1) == 1 ? "one param1": "not one param1") },
    //                    { "parameter_id2", new ParameterValue((i & 2) == 2 ? "one param2": "not one param2") }
    //                }));

    //            bool result = instance.Resolve(condition1, context);

    //            Assert.AreEqual(result, (i & 1) == 1 || (i & 2) == 2);
    //        }            
    //    }


    //    [Test]
    //    public void Resolve_TwoConditionAnd_bool()
    //    {
    //        var instance = CreateInstance();
    //        var condition1 = new EqualCondition("parameter_id1", "one param1", LogicalOperator.and);
    //        var condition2 = new EqualCondition("parameter_id2", "one param2", LogicalOperator.and);
    //        condition1.SetNext(condition2);
    //        for (int i = 0; i < 4; i++)
    //        {
    //            var context = CreateContext(new PackageEntity(1, new StubEntityAggregator(),
    //                new Dictionary<string, ParameterValue> {
    //                    { "parameter_id1", new ParameterValue((i & 1) == 1 ? "one param1": "not one param1") },
    //                    { "parameter_id2", new ParameterValue((i & 2) == 2 ? "one param2": "not one param2") }
    //                }));

    //            bool result = instance.Resolve(condition1, context);

    //            Assert.AreEqual(result, (i & 1) == 1 && (i & 2) == 2);
    //        }
    //    }


    //    [Test]
    //    public void Resolve_ThreeConditionAndOr_bool()
    //    {
    //        var instance = CreateInstance();
    //        var condition1 = new EqualCondition("parameter_id1", "one param1", LogicalOperator.and);
    //        var condition2 = new EqualCondition("parameter_id2", "one param2", LogicalOperator.and);
    //        var condition3 = new EqualCondition("parameter_id3", "one param3", LogicalOperator.or);
    //        condition1.SetNext(condition2);
    //        condition1.Next.SetNext(condition3);
    //        for (int i = 0; i < 8; i++)
    //        {
    //            var context = CreateContext(new PackageEntity(1, new StubEntityAggregator(),
    //                new Dictionary<string, ParameterValue> {
    //                    { "parameter_id1", new ParameterValue((i & 1) == 1 ? "one param1": "not one param1") },
    //                    { "parameter_id2", new ParameterValue((i & 2) == 2 ? "one param2": "not one param2") },
    //                    { "parameter_id3", new ParameterValue((i & 4) == 4 ? "one param3": "not one param3") }
    //                }));

    //            bool result = instance.Resolve(condition1, context);

    //            Assert.AreEqual(result, (i & 1) == 1 && (i & 2) == 2 || (i & 4) == 4);
    //        }
    //    }

    //    [Test]
    //    public void Resolve_ThreeConditionOrAnd_bool()
    //    {
    //        var instance = CreateInstance();
    //        var condition1 = new EqualCondition("parameter_id1", "one param1", LogicalOperator.and);
    //        var condition2 = new EqualCondition("parameter_id2", "one param2", LogicalOperator.or);
    //        var condition3 = new EqualCondition("parameter_id3", "one param3", LogicalOperator.and);
    //        condition1.SetNext(condition2);
    //        condition1.Next.SetNext(condition3);
    //        for (int i = 0; i < 8; i++)
    //        {
    //            var context = CreateContext(new PackageEntity(1, new StubEntityAggregator(),
    //                new Dictionary<string, ParameterValue> {
    //                    { "parameter_id1", new ParameterValue((i & 1) == 1 ? "one param1": "not one param1") },
    //                    { "parameter_id2", new ParameterValue((i & 2) == 2 ? "one param2": "not one param2") },
    //                    { "parameter_id3", new ParameterValue((i & 4) == 4 ? "one param3": "not one param3") }
    //                }));

    //            bool result = instance.Resolve(condition1, context);

    //            Assert.AreEqual(result, (i & 1) == 1 || (i & 2) == 2 && (i & 4) == 4, $"iteration {i}");
    //        }
    //    }

    //    [Test]
    //    public void Resolve_ThreeConditionOrOr_bool()
    //    {
    //        var instance = CreateInstance();
    //        var condition1 = new EqualCondition("parameter_id1", "one param1", LogicalOperator.and);
    //        var condition2 = new EqualCondition("parameter_id2", "one param2", LogicalOperator.or);
    //        var condition3 = new EqualCondition("parameter_id3", "one param3", LogicalOperator.or);
    //        condition1.SetNext(condition2);
    //        condition1.Next.SetNext(condition3);
    //        for (int i = 0; i < 8; i++)
    //        {
    //            var context = CreateContext(new PackageEntity(1, new StubEntityAggregator(),
    //                new Dictionary<string, ParameterValue> {
    //                    { "parameter_id1", new ParameterValue((i & 1) == 1 ? "one param1": "not one param1") },
    //                    { "parameter_id2", new ParameterValue((i & 2) == 2 ? "one param2": "not one param2") },
    //                    { "parameter_id3", new ParameterValue((i & 4) == 4 ? "one param3": "not one param3") }
    //                }));

    //            bool result = instance.Resolve(condition1, context);

    //            Assert.AreEqual(result, (i & 1) == 1 || (i & 2) == 2 || (i & 4) == 4);
    //        }
    //    }

    //    [Test]
    //    public void Resolve_ThreeConditionAndAnd_bool()
    //    {
    //        var instance = CreateInstance();
    //        var condition1 = new EqualCondition("parameter_id1", "one param1", LogicalOperator.and);
    //        var condition2 = new EqualCondition("parameter_id2", "one param2", LogicalOperator.and);
    //        var condition3 = new EqualCondition("parameter_id3", "one param3", LogicalOperator.and);
    //        condition1.SetNext(condition2);
    //        condition1.Next.SetNext(condition3);
    //        for (int i = 0; i < 8; i++)
    //        {
    //            var context = CreateContext(new PackageEntity(1, new StubEntityAggregator(),
    //                new Dictionary<string, ParameterValue> {
    //                    { "parameter_id1", new ParameterValue((i & 1) == 1 ? "one param1": "not one param1") },
    //                    { "parameter_id2", new ParameterValue((i & 2) == 2 ? "one param2": "not one param2") },
    //                    { "parameter_id3", new ParameterValue((i & 4) == 4 ? "one param3": "not one param3") }
    //                }));

    //            bool result = instance.Resolve(condition1, context);

    //            Assert.AreEqual(result, (i & 1) == 1 && (i & 2) == 2 && (i & 4) == 4);
    //        }
    //    }

    //    [Test]
    //    public void Resolve_FourConditionAndOrAnd_bool()
    //    {
    //        var instance = CreateInstance();
    //        var condition1 = new EqualCondition("parameter_id1", "one param1", LogicalOperator.and);
    //        var condition2 = new EqualCondition("parameter_id2", "one param2", LogicalOperator.and);
    //        var condition3 = new EqualCondition("parameter_id3", "one param3", LogicalOperator.or);
    //        var condition4 = new EqualCondition("parameter_id4", "one param4", LogicalOperator.and);
    //        condition1.SetNext(condition2);
    //        condition1.Next.SetNext(condition3);
    //        condition1.Next.Next.SetNext(condition4);
    //        for (int i = 0; i < 16; i++)
    //        {
    //            var context = CreateContext(new PackageEntity(1, new StubEntityAggregator(),
    //                new Dictionary<string, ParameterValue> {
    //                    { "parameter_id1", new ParameterValue((i & 1) == 1 ? "one param1": "not one param1") },
    //                    { "parameter_id2", new ParameterValue((i & 2) == 2 ? "one param2": "not one param2") },
    //                    { "parameter_id3", new ParameterValue((i & 4) == 4 ? "one param3": "not one param3") },
    //                    { "parameter_id4", new ParameterValue((i & 8) == 8 ? "one param4": "not one param4") }
    //                }));

    //            bool result = instance.Resolve(condition1, context);

    //            Assert.AreEqual(result, (i & 1) == 1 && (i & 2) == 2 || (i & 4) == 4 && (i & 8) == 8, $"iteration {i}");
    //        }
    //    }


    //    [Test]
    //    public void Resolve_FourConditionOrAndOr_bool()
    //    {
    //        var instance = CreateInstance();
    //        var condition1 = new EqualCondition("parameter_id1", "one param1", LogicalOperator.and);
    //        var condition2 = new EqualCondition("parameter_id2", "one param2", LogicalOperator.or);
    //        var condition3 = new EqualCondition("parameter_id3", "one param3", LogicalOperator.and);
    //        var condition4 = new EqualCondition("parameter_id4", "one param4", LogicalOperator.or);
    //        condition1.SetNext(condition2);
    //        condition1.Next.SetNext(condition3);
    //        condition1.Next.Next.SetNext(condition4);
    //        for (int i = 0; i < 16; i++)
    //        {
    //            var context = CreateContext(new PackageEntity(1, new StubEntityAggregator(),
    //                new Dictionary<string, ParameterValue> {
    //                    { "parameter_id1", new ParameterValue((i & 1) == 1 ? "one param1": "not one param1") },
    //                    { "parameter_id2", new ParameterValue((i & 2) == 2 ? "one param2": "not one param2") },
    //                    { "parameter_id3", new ParameterValue((i & 4) == 4 ? "one param3": "not one param3") },
    //                    { "parameter_id4", new ParameterValue((i & 8) == 8 ? "one param4": "not one param4") }
    //                }));

    //            bool result = instance.Resolve(condition1, context);

    //            Assert.AreEqual(result, (i & 1) == 1 || (i & 2) == 2 && (i & 4) == 4 || (i & 8) == 8, $"iteration {i}");
    //        }
    //    }

    //    [Test]
    //    public void Resolve_FiveConditionAndOrAndOr_bool()
    //    {
    //        var instance = CreateInstance();
    //        var condition1 = new EqualCondition("parameter_id1", "one param1", LogicalOperator.and);
    //        var condition2 = new EqualCondition("parameter_id2", "one param2", LogicalOperator.and);
    //        var condition3 = new EqualCondition("parameter_id3", "one param3", LogicalOperator.or);
    //        var condition4 = new EqualCondition("parameter_id4", "one param4", LogicalOperator.and);
    //        var condition5 = new EqualCondition("parameter_id5", "one param5", LogicalOperator.or);
    //        condition1.SetNext(condition2);
    //        condition1.Next.SetNext(condition3);
    //        condition1.Next.Next.SetNext(condition4);
    //        condition1.Next.Next.Next.SetNext(condition5);
    //        for (int i = 0; i < 32; i++)
    //        {
    //            var context = CreateContext(new PackageEntity(1, new StubEntityAggregator(),
    //                new Dictionary<string, ParameterValue> {
    //                    { "parameter_id1", new ParameterValue((i & 1) == 1 ? "one param1": "not one param1") },
    //                    { "parameter_id2", new ParameterValue((i & 2) == 2 ? "one param2": "not one param2") },
    //                    { "parameter_id3", new ParameterValue((i & 4) == 4 ? "one param3": "not one param3") },
    //                    { "parameter_id4", new ParameterValue((i & 8) == 8 ? "one param4": "not one param4") },
    //                    { "parameter_id5", new ParameterValue((i & 16) == 16 ? "one param5": "not one param5") }
    //                }));

    //            bool result = instance.Resolve(condition1, context);

    //            Assert.AreEqual(result, (i & 1) == 1 && (i & 2) == 2 || (i & 4) == 4 && (i & 8) == 8 || (i & 16) == 16, $"iteration {i}");
    //        }
    //    }

    //    private ConditionContext CreateContext(PackageEntity entity = null) => 
    //        new ConditionContext(entity ?? new PackageEntity(1,new StubEntityAggregator(), new Dictionary<string, ParameterValue>() ));

    //    private ConditionChainResolver CreateInstance()
    //    {

    //        new ConditionChainResolver().

    //        var serviceColl = new ServiceCollection();
    //        var provider = new RepositoryProviderBuilder(serviceColl).
    //            AddRepository<MockRegexTemplateRepository, RegexTemplate, string>(ServiceLifetime.Singleton).
    //            AddRepository<MockParameterTemplatesRepository, ParameterTemplate, string>(ServiceLifetime.Singleton);
    //        return new ConditionChainResolver(
    //            serviceColl.
    //        AddSingleton<IRegexTemplateParcer, RegexTemplateParcer>().
    //        AddSingleton(new LoggerFactory().CreateLogger<RegexTemplateParcer>()).
    //        AddSingleton(new LoggerFactory().CreateLogger<EqualConditionResolver>()).
    //        AddSingleton(new LoggerFactory().CreateLogger<ContainsConditionResolver>()).
    //        AddSingleton(new LoggerFactory().CreateLogger<RegexConditionResolver>()).
    //        AddSingleton<IRegexTemplateParcer, RegexTemplateParcer>().
    //        AddSingleton<ContextBuilder<RegexContext>, RegexContextBuilder>().
    //        AddConditions().Build(), new LoggerFactory().CreateLogger<ConditionChainResolver>());
    //    }            
    //}

}
