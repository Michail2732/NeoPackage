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
    //public class RegexTemplateExtracterTests
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
    //    public void Extract_IncorrectCombinationParameters_EmptyExtractValue()
    //    {
            
    //        for (int i = 0; i < 16; i++)
    //        {
    //            BitVector32 bitMask = new BitVector32(i);
    //            var instance = CreateInstance(regexTemplates: bitMask[8]
    //                    ? new List<RegexTemplate> { new RegexTemplate("regex_temp_id", "regex ${parameter_id} pattern") }
    //                        : null,
    //                parameterTemplates: bitMask[8]
    //                    ? new List<ParameterTemplate> { new ParameterTemplate("parameter_id", ".*", "description") }
    //                        : null);
    //            var extractInfo = new RegexTemplateExtractInfo(
    //                bitMask[1] ? "regex_temp_id" : string.Empty,
    //                bitMask[2] ? new List<string> { "parameter_id" } : new List<string> { }
    //                )
    //            {
    //                SourceValue = bitMask[4] 
    //                    ? new ExtractValue(new Dictionary<string, ParameterValue> { { "key", 
    //                            new ParameterValue("source_value_yo_pizza") } }) 
    //                        : ExtractValue.Empty
    //            };
    //            var context = CreateContext();

    //            var extractValue = instance.Extract(extractInfo, context);
    //            Assert.AreEqual(extractValue, ExtractValue.Empty);
    //        }            
    //    }               

    //    [Test]
    //    public void Extract_RegexTemplateWithoutParameters_EmptyExtractValue()
    //    {
    //        var instance = CreateInstance(regexTemplates: new List<RegexTemplate> {
    //            new RegexTemplate("some", "penisnaya zhidkost") });
    //        var extractInfo = new RegexTemplateExtractInfo("some",
    //            new List<string> { })
    //        {
    //            SourceValue = new ExtractValue(
    //                new Dictionary<string, ParameterValue>() { { "key",
    //                        new ParameterValue("penisnaya zhidkost") } })
    //        };
    //        var context = CreateContext();

    //        var extractValue = instance.Extract(extractInfo, context);

    //        Assert.AreNotEqual(extractValue, ExtractValue.Empty);            
    //    }

    //    [Test]
    //    public void Extract_RegexTemplateWithoutParamIdAndParamTemp_RegexException()
    //    {
    //        var instance = CreateInstance(regexTemplates: new List<RegexTemplate> {
    //            new RegexTemplate("template_1", "${penisnaya} ${zhidkost}") });
    //        var extractInfo = new RegexTemplateExtractInfo("template_1",
    //            new List<string> { })
    //        {
    //            SourceValue = new ExtractValue(
    //                new Dictionary<string, ParameterValue>() { { "key",
    //                        new ParameterValue("penisnaya zhidkost") } })
    //        };
    //        var context = CreateContext();

    //        Assert.Catch<RegexException>(() => instance.Extract(extractInfo, context));            
    //    }

    //    [Test]
    //    public void Extract_RegexTemplateWithoutParamId_EmptyExtractValue()
    //    {
    //        var instance = CreateInstance(new List<RegexTemplate> {
    //            new RegexTemplate("template_1", "${penisnaya} ${zhidkost}") },
    //            new List<ParameterTemplate> { new ParameterTemplate("penisnaya", "PENISNAYA", "some"),
    //            new ParameterTemplate("zhidkost", "ZHIDKOST", "some")});
    //        var extractInfo = new RegexTemplateExtractInfo("template_1",
    //            new List<string> { })
    //        {
    //            SourceValue = new ExtractValue(
    //                new Dictionary<string, ParameterValue>() { { "key",
    //                        new ParameterValue("PENISNAYA ZHIDKOST") } })
    //        };
    //        var context = CreateContext();

    //        var extractValue = instance.Extract(extractInfo, context);

    //        Assert.AreNotEqual(extractValue, ExtractValue.Empty);
    //    }


    //    [Test]
    //    public void Extract_RegexTemplateWithParameter_EmptyExtractValue()
    //    {
    //        var instance = CreateInstance(new List<RegexTemplate> {
    //            new RegexTemplate("template_1", "${penisnaya} ${zhidkost}") },
    //            new List<ParameterTemplate> { new ParameterTemplate("penisnaya", "PENISNAYA", "some"),
    //            new ParameterTemplate("zhidkost", "ZHIDKOST", "some")});
    //        var extractInfo = new RegexTemplateExtractInfo("template_1",
    //            new List<string> {"penisnaya", "zhidkost" })
    //        {
    //            SourceValue = new ExtractValue(
    //                new Dictionary<string, ParameterValue>() { { "source_key", 
    //                        new  ParameterValue("PENISNAYA ZHIDKOST") } })
    //        };
    //        var context = CreateContext();

    //        var extractValue = instance.Extract(extractInfo, context);

    //        Assert.AreNotEqual(extractValue, ExtractValue.Empty);
    //        Assert.AreEqual(extractValue.ParameterValues.Count, 2);
    //        Assert.IsTrue(extractValue.ParameterValues.ContainsKey("penisnaya"));
    //        Assert.IsTrue(extractValue.ParameterValues.ContainsKey("zhidkost"));
    //        Assert.AreEqual(extractValue.ParameterValues["penisnaya"].As<string>(), "PENISNAYA");
    //        Assert.AreEqual(extractValue.ParameterValues["zhidkost"].As<string>(), "ZHIDKOST");
    //    }                

    //    [Test]
    //    public void Extract_RegexTempAndExtraParam_ExtractValue()
    //    {
    //        var instance = CreateInstance(new List<RegexTemplate> {
    //            new RegexTemplate("some", "${penisnaya} ${zhidkost}") },
    //            new List<ParameterTemplate> { new ParameterTemplate("penisnaya", "U poli", "any"),
    //            new ParameterTemplate("zhidkost", "berezka stoyala", "any")});
    //        var extractInfo = new RegexTemplateExtractInfo("some",
    //            new List<string> { "penisnaya", "zhidkost", "pomidor" })
    //        {
    //            SourceValue = new ExtractValue(
    //                new Dictionary<string, ParameterValue>() { { "key", 
    //                        new ParameterValue("U poli berezka stoyala") } })
    //        };
    //        var context = CreateContext();

    //        var extractValue = instance.Extract(extractInfo, context);

    //        Assert.AreNotEqual(extractValue, ExtractValue.Empty);
    //        Assert.AreEqual(extractValue.ParameterValues.Count, 3);
    //        Assert.IsTrue(extractValue.ParameterValues.ContainsKey("penisnaya"));
    //        Assert.IsTrue(extractValue.ParameterValues.ContainsKey("zhidkost"));
    //        Assert.IsTrue(extractValue.ParameterValues.ContainsKey("pomidor"));
    //        Assert.AreEqual(extractValue.ParameterValues["penisnaya"].As<string>(), "U poli");
    //        Assert.AreEqual(extractValue.ParameterValues["zhidkost"].As<string>(), "berezka stoyala");
    //        Assert.AreEqual(extractValue.ParameterValues["pomidor"].As<string>(), string.Empty);
    //    }


    //    private RegexTemplateExtracter CreateInstance(List<RegexTemplate> regexTemplates = null, 
    //        List<ParameterTemplate> parameterTemplates = null)
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
    //        return new RegexTemplateExtracter(new RegexTemplateParcer(
    //               new Logger<RegexTemplateParcer>(new LoggerFactory())),
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
