using CheckPackage.Core.Abstract;
using CheckPackage.Core.Extracts;
using CheckPackage.Core.Regex;
using CheckPackage.Tests.Core.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Extract
{

    //[TestFixture]
    //public class ExtractProviderTests
    //{        
    //    [Test]        
    //    public void HasCommand_null_false() 
    //    {
    //        var extractProvider = CreateInstance();

    //        bool result = extractProvider.HasCommand(null);

    //        Assert.IsFalse(result);
    //    }

    //    [Test]
    //    public void HasCommand_StubExtractInfo_false()
    //    {
    //        var extractProvider = CreateInstance();

    //        bool result = extractProvider.HasCommand(new StubExtractInfo());

    //        Assert.IsFalse(result);
    //    }

    //    [Test]
    //    public void HasCommand_EmbeddedExtractInfo_false()
    //    {
    //        var extractProvider = CreateInstance();

    //        bool result1 = extractProvider.HasCommand(new EntityParameterExtractInfo(string.Empty));
    //        bool result2 = extractProvider.HasCommand(new RegexTemplateExtractInfo(string.Empty, new List<string>()));
    //        bool result3 = extractProvider.HasCommand(new SubstringExtractInfo(new RegexTemplate(string.Empty, string.Empty), string.Empty, string.Empty));

    //        Assert.IsTrue(result1);
    //        Assert.IsTrue(result2);
    //        Assert.IsTrue(result3);
    //    }

    //    [Test]
    //    public void Indexator_Null_ArgumentNullException()
    //    {
    //        var extractProvider = CreateInstance();

    //        Assert.Catch<ArgumentNullException>(() => { var val = extractProvider[null]; });                        
    //    }

    //    [Test]
    //    public void Indexator_StubExtractInfo_KeyNotFoundException()
    //    {
    //        var extractProvider = CreateInstance();

    //        Assert.Catch<KeyNotFoundException>(() => { var val = extractProvider[new StubExtractInfo()]; });
    //    }

    //    [Test]
    //    public void Indexator_EmbeddedExtractInfo_Extracters()
    //    {
    //        var extractProvider = CreateInstance();
    //        var regexParamInfo = new RegexTemplateExtractInfo(string.Empty, new List<string>());
    //        var substrParamInfo = new SubstringExtractInfo(new RegexTemplate("", ""),"", "" );
    //        var entityParamInfo = new EntityParameterExtractInfo(string.Empty);
    //        var result1 = extractProvider[regexParamInfo];
    //        var result2 = extractProvider[substrParamInfo];
    //        var result3 = extractProvider[entityParamInfo];

    //        Assert.IsNotNull(result1);
    //        Assert.IsNotNull(result2);
    //        Assert.IsNotNull(result3);
    //        Assert.IsInstanceOf<RegexTemplateExtracter>(result1);
    //        Assert.IsInstanceOf<SubstringExtracter>(result2);
    //        Assert.IsInstanceOf<EntityParameterExtracter>(result3);
    //    }


    //    private ExtracterProvider CreateInstance()
    //    {
    //        var serviceCollection = new ServiceCollection();
    //        var repositoryProvider = new RepositoryProviderBuilder(serviceCollection).
    //          AddRepository<MockParameterTemplatesRepository, ParameterTemplate, string>(ServiceLifetime.Singleton).
    //          AddRepository<MockRegexTemplateRepository, RegexTemplate, string>(ServiceLifetime.Singleton).Build();
    //        return serviceCollection.
    //        AddSingleton<IRegexTemplateParcer, RegexTemplateParcer>().
    //        AddSingleton(new LoggerFactory().CreateLogger<RegexTemplateParcer>()).
    //        AddSingleton(new LoggerFactory().CreateLogger<RegexTemplateExtracter>()).
    //        AddSingleton(new LoggerFactory().CreateLogger<SubstringExtracter>()).
    //        AddSingleton(new LoggerFactory().CreateLogger<EntityParameterExtracter>()).
    //        AddSingleton<ContextBuilder<RegexContext>, RegexContextBuilder>().
    //        AddExtracters().Build();
    //    } 

    //}

}
