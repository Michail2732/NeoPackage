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
using System.Text;

namespace CheckPackage.Tests.Core.Extract
{

    //[TestFixture]
    //public class ExtractChainExtracterTests
    //{        
    //    [Test]        
    //    public void Extract_SomeNull_ArgumentNullException() 
    //    {
    //        ExtractChainExtracter instance = CreateInstance();
    //        ExtractContext context = CreateContext();
    //        StubExtractInfo extractInfo = new StubExtractInfo();
    //        Assert.Catch<ArgumentNullException>(() => instance.Extract(null, context));
    //        Assert.Catch<ArgumentNullException>(() => instance.Extract(extractInfo, null));
    //        Assert.Catch<ArgumentNullException>(() => instance.Extract(null, null));
    //    }        



    //    private ExtractContext CreateContext(List<PackageEntity> entities = null,
    //        List<RegexTemplate> regexTemplates = null, List<ParameterTemplate> parameterTemplates = null)
    //    {            
    //        return new ExtractContext(entities ?? new List<PackageEntity>());
    //    }

    //    private ExtractChainExtracter CreateInstance()
    //    {
    //        var serviceCollection = new ServiceCollection();
    //        var repositoryProvider = new RepositoryProviderBuilder(serviceCollection).
    //          AddRepository<MockParameterTemplatesRepository, ParameterTemplate, string>(ServiceLifetime.Singleton).
    //          AddRepository<MockRegexTemplateRepository, RegexTemplate, string>(ServiceLifetime.Singleton).Build();
    //        return new ExtractChainExtracter(
    //            serviceCollection.
    //        AddSingleton<IRegexTemplateParcer, RegexTemplateParcer>().
    //        AddSingleton(new LoggerFactory().CreateLogger<RegexTemplateParcer>()).
    //        AddSingleton(new LoggerFactory().CreateLogger<RegexTemplateExtracter>()).
    //        AddSingleton(new LoggerFactory().CreateLogger<SubstringExtracter>()).
    //        AddSingleton(new LoggerFactory().CreateLogger<EntityParameterExtracter>()).
    //        AddSingleton<ContextBuilder<RegexContext>, RegexContextBuilder>().
    //        AddExtracters().Build(), new LoggerFactory().CreateLogger<ExtractChainExtracter>());
    //    }            
    //}

}
