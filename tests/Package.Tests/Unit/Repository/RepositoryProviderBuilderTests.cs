using NUnit.Framework;
using NSubstitute;
using Package.Repository.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Package.Tests.Unit.Repository
{

    [TestFixture]
    public class RepositoryProviderBuilderTests
    {        

        [Test]     
        public void Build_EmptyRepositories_RepositoryProvider() 
        {            
            var builder = Instance();

            var provider = builder.Build(Substitute.For<IServiceProvider>());

            Assert.NotNull(provider);
            Assert.IsTrue(provider is RepositoriesProvider);
        }

        [Test]
        public void Build_OneRepository_RepositoryProvider()
        {
            var collection = new ServiceCollection();
            var builder = Instance(collection);
            builder.AddRepository<StubRepository, StubRepositoryItem, string>();

            var provider = builder.Build(collection.BuildServiceProvider());
            var repository = provider.GetRepository<StubRepositoryItem, string>();

            Assert.NotNull(provider);
            Assert.NotNull(repository);
            Assert.IsTrue(provider is RepositoriesProvider);
            Assert.IsTrue(repository is StubRepository);
            Assert.IsTrue(provider.HasRepository<StubRepositoryItem, string>());
        }


        private RepositoriesProviderBuilder Instance(IServiceCollection collection = null) =>
            new RepositoriesProviderBuilder(collection == null ? Substitute.For<IServiceCollection>() : collection);
        
    }

}
