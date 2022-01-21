using CheckPackage.Core.Abstract;
using CheckPackage.Core.Extracts;
using CheckPackage.Core.Package;
using CheckPackage.Core.Regex;
using CheckPackage.Tests.Core.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CheckPackage.Tests.Core.Extract
{

    //[TestFixture]
    //public class SubstringExtracterTests
    //{        
    //    [Test]        
    //    public void Extract_NullParameter_ArgumentNullException() 
    //    {
    //        var instance = CreateInstance();
    //        var context = CreateContext();
    //        var stubInfo = new StubExtractInfo();

    //        Assert.Catch<ArgumentNullException>(() => instance.Extract(stubInfo, null));
    //        Assert.Catch<ArgumentNullException>(() => instance.Extract(null, context));
    //        Assert.Catch<ArgumentNullException>(() => instance.Extract(null, null));
    //    }

    //    [Test]
    //    public void Extract_IncorrectInfo_ArgumentException()
    //    {
    //        var instance = CreateInstance();
    //        var context = CreateContext();
    //        var stubInfo = new StubExtractInfo();

    //        var ex = Assert.Catch<ArgumentException>(() => instance.Extract(stubInfo, context));
    //        Assert.IsTrue(ex.Message.Contains("Cant extract value, expected type"));
    //    }

    //    [Test]
    //    public void Extract_InfoEmpty_EmptyExtractValue()
    //    {
    //        for (int i = 0; i < 64; i++)
    //        {
    //            BitVector32 bitMask = new BitVector32(i);
    //            var instance = CreateInstance(regexTemplates: bitMask[1]
    //                    ? new List<RegexTemplate> { new RegexTemplate("template_id", "some text ${parameter_id}") }
    //                    : null,
    //                parameterTemplates: bitMask[2]
    //                    ? new List<ParameterTemplate> { new ParameterTemplate("parameter_id", "zhidkost", "description") }
    //                    : null);
    //            var context = CreateContext();
    //            var info = new SubstringExtractInfo(
    //                new RegexTemplate(
    //                    bitMask[4] ? "template_id": string.Empty,
    //                    bitMask[8] ? "(?<group1>textsome) ${parameter_id}" : string.Empty),
    //                    bitMask[16] ? "group1": string.Empty,
    //                    bitMask[32] ? "parameter_1" : string.Empty);

    //            var resutl = instance.Extract(info, context);

    //            Assert.AreEqual(resutl, ExtractValue.Empty);
    //        }            
    //    }
        

    //    [Test]
    //    public void Extract_ExtractTrmplateWithoutRefParameters_EmptyExtractValue()
    //    {
    //        var instance = CreateInstance();
    //        var context = CreateContext();
    //        var info = new SubstringExtractInfo(
    //            new RegexTemplate("any", "kazhdiy ohotnik (?<group1>zhelaet) znat"), 
    //            "group1", 
    //            "parameter_1")
    //        { SourceValue = new ExtractValue(new Dictionary<string, ParameterValue>() { { "parameter1", 
    //                new  ParameterValue("kazhdiy ohotnik zhelaet znat") } }) };

    //        var result = instance.Extract(info, context);

    //        Assert.AreNotEqual(result, ExtractValue.Empty);
    //        Assert.AreEqual(result.ParameterValues.Count, 1);
    //        Assert.IsTrue(result.ParameterValues.ContainsKey("parameter_1"));
    //        Assert.AreEqual(result.ParameterValues["parameter_1"].As<string>(), "zhelaet");
    //    }


    //    private SubstringExtracter CreateInstance(List<RegexTemplate> regexTemplates = null, List<ParameterTemplate> parameterTemplates = null)
    //    {
    //        var repositoryProvider = new RepositoryProviderBuilder(new ServiceCollection()).
    //            AddRepository<MockParameterTemplatesRepository, ParameterTemplate, string>(ServiceLifetime.Singleton).
    //            AddRepository<MockRegexTemplateRepository, RegexTemplate, string>(ServiceLifetime.Singleton).Build();
    //        var mockParameTempRep = repositoryProvider.GetRepository<ParameterTemplate, string>(nameof(ParameterTemplate));
    //        ((MockParameterTemplatesRepository)mockParameTempRep).SetParameterTemplates(parameterTemplates ??
    //            new List<ParameterTemplate>());
    //        var mockRegexTempRep = repositoryProvider.GetRepository<RegexTemplate, string>(nameof(RegexTemplate));
    //        ((MockRegexTemplateRepository)mockRegexTempRep).SetRegexTemplates(regexTemplates ??
    //            new List<RegexTemplate>());            
    //        return new SubstringExtracter(new RegexTemplateParcer(new Logger<RegexTemplateParcer>(new LoggerFactory())),                   
    //               new RegexContextBuilder(repositoryProvider));
    //    }            


    //    private ExtractContext CreateContext(List<PackageEntity> entities = null)
    //    {
    //        return new ExtractContext(entities ?? new List<PackageEntity>());
    //    }

    //    private PackageEntity CreateEntity(int level = 1,
    //        Dictionary<string, ParameterValue> parameters = null) =>
    //        new PackageEntity(level, new StubEntityAggregator(),
    //            parameters == null ? new Dictionary<string, ParameterValue>() : parameters);
    //}

}
