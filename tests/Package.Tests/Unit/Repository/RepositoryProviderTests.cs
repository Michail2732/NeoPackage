using NUnit.Framework;
using NSubstitute;
using Package.Repository.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Package.Repository.Exceptions;
using Package.Abstraction.Services;

namespace Package.Tests.Unit.Repository
{

    [TestFixture]
    public class RepositoryProviderTests
    {        

        [Test]        
        public void Ctor_IncorrectRepository_ArgumentException() 
        {
            var ex = Assert.Catch<ArgumentException>(() => 
                Instance(new Dictionary<Type, object> { { typeof(AppDomain), new object() } }));
            Assert.AreEqual(ex.Message, "Incorrect repository instance");
        }


        [Test]
        public void Ctor_IncorrectRepositoryRelation_ArgumentException()
        {
            var ex = Assert.Catch<ArgumentException>(() =>
                Instance(new Dictionary<Type, object> { 
                    { typeof(AppDomain),  Substitute.For<IRepository<StubRepositoryItem, string>>() }
                }));
            Assert.AreEqual(ex.Message, "Incorrect relation of type and instance");
        }

        [Test]
        public void Ctor_EmptyRepositories_void()
        {
            var instance = Instance();            
        }


        [Test]
        public void GetRepository_EmptyRepositories_RepositoryNotFoundException()
        {
            var instance = Instance();
            var ex = Assert.Catch<RepositoryNotFoundException>(() =>
                instance.GetRepository<StubRepositoryItem, string>());

            Assert.AreEqual(ex.Message, $"Repository of items {typeof(StubRepositoryItem).Name} not registred");
        }

        [Test]
        public void GetRepository_NotExistRepository_RepositoryNotFoundException()
        {
            var instance = Instance(new Dictionary<Type, object> {
                { typeof(StubRepositoryItem), Substitute.For<IRepository<StubRepositoryItem, string>>() } 
            });
            var ex = Assert.Catch<RepositoryNotFoundException>(() =>
                instance.GetRepository<StubRepositoryItem2, string>());

            Assert.AreEqual(ex.Message, $"Repository of items {typeof(StubRepositoryItem2).Name} not registred");
        }

        [Test]
        public void GetRepository_ExistRepository_RepositoryBase()
        {
            var repository = Substitute.For<IRepository<StubRepositoryItem, string>>();
            var instance = Instance(new Dictionary<Type, object> {
                {typeof(StubRepositoryItem), repository}
            });                
            var result = instance.GetRepository<StubRepositoryItem, string>();

            Assert.NotNull(result);
            Assert.AreEqual(result, repository);
        }

        [Test]
        public void HasRepository_EmptyRepositories_false()
        {            
            var instance = Instance();

            var result = instance.HasRepository<StubRepositoryItem, string>();

            Assert.IsFalse(result);            
        }


        [Test]
        public void HasRepository_NotExistRepository_false()
        {
            var repository = Substitute.For<IRepository<StubRepositoryItem, string>>();
            var instance = Instance(new Dictionary<Type, object> {
                { typeof(StubRepositoryItem), repository}
            });

            var result = instance.HasRepository<StubRepositoryItem2, string>();

            Assert.IsFalse(result);            
        }


        [Test]
        public void HasRepository_ExistRepository_true()
        {
            var repository = Substitute.For<IRepository<StubRepositoryItem, string>>();
            var instance = Instance(new Dictionary<Type, object> {
                { typeof(StubRepositoryItem), repository}
            });

            var result = instance.HasRepository<StubRepositoryItem, string>();

            Assert.IsTrue(result);            
        }

        private RepositoriesProvider Instance(Dictionary<Type, object> items = null) =>
            new RepositoriesProvider(items == null ? new Dictionary<Type, object>() : items);

    }

}
