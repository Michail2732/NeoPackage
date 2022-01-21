using CheckPackage.Core.Abstract;
using CheckPackage.Core.Extracts;
using CheckPackage.Core.Package;
using CheckPackage.Core.Regex;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Extract
{

    //[TestFixture]
    //public class EntityParameterExtracterTests
    //{        

    //    [Test]        
    //    public void Extract_null_ArgumentNullException() 
    //    {
    //        var instance = CreateInstance();

    //        Assert.Catch<ArgumentNullException>(() => instance.Extract(null, null));
    //        Assert.Catch<ArgumentNullException>(() => instance.Extract(null, CreateContext()));
    //        Assert.Catch<ArgumentNullException>(() => instance.Extract(new StubExtractInfo(), null));            
    //    }

    //    [Test]
    //    public void Extract_IncorrectTypeExtractInfo_ArgumentException()
    //    {
    //        var instance = CreateInstance();
    //        var stubExtractinfo = new StubExtractInfo();
    //        var context = CreateContext();

    //        var ex = Assert.Catch<ArgumentException>(() => instance.Extract(stubExtractinfo, context));
    //        Assert.IsTrue(ex.Message.Contains("Cant extract value, expected type"));
    //    }

    //    [Test]
    //    public void Extract_InfoEmpty_EmptyExtractValue()
    //    {
    //        var instance = CreateInstance();
    //        var extractInfo = new EntityParameterExtractInfo(string.Empty);
    //        var context = CreateContext();

    //        var extractValue = instance.Extract(extractInfo, context);

    //        Assert.AreEqual(extractValue, ExtractValue.Empty);
    //    }

    //    [Test]
    //    public void Extract_InfoWithParameter_EmptyExtractValue()
    //    {
    //        var instance = CreateInstance();
    //        var extractInfo = new EntityParameterExtractInfo("parameterTest");
    //        var context = CreateContext();
            
    //        var extractValue = instance.Extract(extractInfo, context);
                        
    //        Assert.AreEqual(extractValue, ExtractValue.Empty);
    //    }

    //    [Test]
    //    public void Extract_InfoWithoutParameter_EmptyExtractValue()
    //    {
    //        var instance = CreateInstance();
    //        var extractInfo = new EntityParameterExtractInfo(string.Empty);
    //        var context = CreateContext();

    //        var extractValue = instance.Extract(extractInfo, context);

    //        Assert.AreEqual(extractValue, ExtractValue.Empty);
    //    }

    //    [Test]
    //    public void Extract_InfoWithParameterAndEntity_ExtractValue()
    //    {
    //        var instance = CreateInstance();
    //        var extractInfo = new EntityParameterExtractInfo("parameterTest");            
    //        var entity = CreateEntity(1, new Dictionary<string, ParameterValue> { { "parameterTest",
    //                new ParameterValue("penis") } });
    //        var context = CreateContext(new List<PackageEntity>() { entity });
            
    //        var extractValue = instance.Extract(extractInfo, context);

    //        Assert.AreNotEqual(extractValue, ExtractValue.Empty);
    //        Assert.AreEqual(extractValue.ParameterValues.Count, 1);
    //        Assert.IsTrue(extractValue.ParameterValues.ContainsKey("parameterTest"));
    //        Assert.AreEqual(extractValue.ParameterValues["parameterTest"].As<string>(), "penis");
    //    }

    //    [Test]
    //    public void Extract_InfoWithParameterAndEntitiesDiff_ExtractValue()
    //    {
    //        var instance = CreateInstance();
    //        var extractInfo = new EntityParameterExtractInfo("parameterTest");            
    //        var entity1 = CreateEntity(1, new Dictionary<string, ParameterValue> { { "parameterTest", 
    //                new ParameterValue("penis") } });            
    //        var entity2 = CreateEntity(1, new Dictionary<string, ParameterValue> { { "parameterTest", 
    //                new ParameterValue("derenis") } });
    //        var context = CreateContext(new List<PackageEntity>() { entity1, entity2 });

    //        var extractValue = instance.Extract(extractInfo, context);

    //        Assert.AreNotEqual(extractValue, ExtractValue.Empty);
    //        Assert.AreEqual(extractValue.ParameterValues.Count, 1);
    //        Assert.IsTrue(extractValue.ParameterValues.ContainsKey("parameterTest"));
    //        Assert.AreEqual(extractValue.ParameterValues["parameterTest"].As<string>(), "penis");
    //    }

    //    [Test]
    //    public void Extract_InfoWithParameterAndEntities_ExtractValue()
    //    {
    //        var instance = CreateInstance();
    //        var extractInfo = new EntityParameterExtractInfo("parameterTest");
    //        var entity1 = CreateEntity(1, new Dictionary<string, ParameterValue> { { "parameterTest", 
    //                new ParameterValue("penis") } });
    //        var entity2 = CreateEntity(1, new Dictionary<string, ParameterValue> { { "parameterTest", 
    //                new ParameterValue("penis") } });
    //        var context = CreateContext(new List<PackageEntity>() { entity1, entity2 });

    //        var extractValue = instance.Extract(extractInfo, context);

    //        Assert.AreNotEqual(extractValue, ExtractValue.Empty);
    //        Assert.AreEqual(extractValue.ParameterValues.Count, 1);
    //        Assert.IsTrue(extractValue.ParameterValues.ContainsKey("parameterTest"));
    //        Assert.AreEqual(extractValue.ParameterValues["parameterTest"].As<string>(), "penis");
    //    }

    //    [Test]
    //    public void Extract_InfoWithParameterAndEntityAndInAllEntity_ExtractValue()
    //    {
    //        var instance = CreateInstance();
    //        var extractInfo = new EntityParameterExtractInfo("parameterTest", true);            
    //        var entity = CreateEntity(1, new Dictionary<string, ParameterValue> { { "parameterTest", 
    //                new ParameterValue("penis") } });
    //        var context = CreateContext(new List<PackageEntity>() { entity });

    //        var extractValue = instance.Extract(extractInfo, context);

    //        Assert.AreNotEqual(extractValue, ExtractValue.Empty);
    //        Assert.AreEqual(extractValue.ParameterValues.Count, 1);
    //        Assert.IsTrue(extractValue.ParameterValues.ContainsKey("parameterTest"));
    //        Assert.AreEqual(extractValue.ParameterValues["parameterTest"].As<string>(), "penis");
    //    }

    //    [Test]
    //    public void Extract_InfoWithParameterAndEntitiesAndInAllEntity_ExtractValue()
    //    {
    //        var instance = CreateInstance();
    //        var extractInfo = new EntityParameterExtractInfo("parameterTest", true);            
    //        var entity1 = CreateEntity(1, new Dictionary<string, ParameterValue> { { "parameterTest",
    //                new ParameterValue("penis") } });            
    //        var entity2 = CreateEntity(1, new Dictionary<string, ParameterValue> { { "parameterTest", 
    //                new ParameterValue("penis") } });
    //        var context = CreateContext(new List<PackageEntity>() { entity1, entity2 });

    //        var extractValue = instance.Extract(extractInfo, context);

    //        Assert.AreNotEqual(extractValue, ExtractValue.Empty);
    //        Assert.AreEqual(extractValue.ParameterValues.Count, 1);
    //        Assert.IsTrue(extractValue.ParameterValues.ContainsKey("parameterTest"));
    //        Assert.AreEqual(extractValue.ParameterValues["parameterTest"].As<string>(), "penis");
    //    }

    //    [Test]
    //    public void Extract_InfoWithParameterAndEntitiesDiffAndInAllEntity_ExtractValue()
    //    {
    //        var instance = CreateInstance();
    //        var extractInfo = new EntityParameterExtractInfo("parameterTest", true);            
    //        var entity1 = CreateEntity(1, new Dictionary<string, ParameterValue> { { "parameterTest",
    //                new ParameterValue("penis") } });
    //        var entity2 = CreateEntity(1, new Dictionary<string, ParameterValue> { { "parameterTest", 
    //                new ParameterValue("tenis") } });
    //        var context = CreateContext(new List<PackageEntity>() { entity1, entity2 });

    //        var extractValue = instance.Extract(extractInfo, context);

    //        Assert.AreEqual(extractValue, ExtractValue.Empty);            
    //    }

    //    private EntityParameterExtracter CreateInstance() =>
    //        new EntityParameterExtracter();

    //    private ExtractContext CreateContext(List<PackageEntity> entities = null) =>
    //        new ExtractContext(entities ?? new List<PackageEntity>());

    //    private PackageEntity CreateEntity(int level = 1, 
    //        Dictionary<string, ParameterValue> parameters = null ) =>
    //        new PackageEntity(level, new StubEntityAggregator(), 
    //            parameters == null ?  new Dictionary<string, ParameterValue>(): parameters);
    //}

}
