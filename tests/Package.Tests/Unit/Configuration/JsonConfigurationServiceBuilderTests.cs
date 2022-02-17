using NUnit.Framework;
using NSubstitute;
using Package.Configuration.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Package.Tests.Unit.Configuration
{

    [TestFixture]
    public class JsonConfigurationServiceBuilderTests
    {        

        [Test]        
        public void BuildReader_EmptyBinder_JsonConfigurationService() 
        {
            var builder = Instance();

            var readder = builder.BuildReader(Substitute.For<IServiceProvider>());

            Assert.IsNotNull(readder);
            Assert.IsTrue(readder is JsonConfigurationService);
        }

        [Test]
        public void BuildWriter_EmptyBinder_JsonConfigurationService()
        {
            ServiceCollection collection = new ServiceCollection();
            var builder = Instance(collection);

            var writer = builder.BuildWriter(collection.BuildServiceProvider());
            Assert.IsNotNull(writer);
            Assert.IsTrue(writer is JsonConfigurationService);
        }


        [Test]
        public void BuildReader_OneBinder_JsonConfigurationService()
        {
            ServiceCollection collection = new ServiceCollection();
            var builder = Instance(collection);
            builder.AddBinder<StubJsonConfigurationBinder, string>();

            var writer = builder.BuildReader(collection.BuildServiceProvider());
            Dictionary<Type, object> binders = (Dictionary<Type, object>)writer.GetType().
                GetField("_binders", System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Instance).GetValue(writer);

            Assert.IsNotNull(writer);
            Assert.IsTrue(writer is JsonConfigurationService);
            Assert.IsTrue(binders.Count == 1);
            Assert.IsTrue(binders.First().Key == typeof(string));
            Assert.IsTrue(binders.First().Value.GetType() == typeof(StubJsonConfigurationBinder));
        }


        [Test]
        public void BuildWriter_OneBinder_JsonConfigurationService()
        {
            ServiceCollection collection = new ServiceCollection();
            var builder = Instance(collection);
            builder.AddBinder<StubJsonConfigurationBinder, string>();

            var writer = builder.BuildWriter(collection.BuildServiceProvider());
            Dictionary<Type, object> binders = (Dictionary<Type, object>)writer.GetType().
                GetField("_binders", System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance).GetValue(writer);

            Assert.IsNotNull(writer);
            Assert.IsTrue(writer is JsonConfigurationService);
            Assert.IsTrue(binders.Count == 1);
            Assert.IsTrue(binders.First().Key == typeof(string));
            Assert.IsTrue(binders.First().Value.GetType() == typeof(StubJsonConfigurationBinder));
        }


        private JsonConfigurationServiceBuilder Instance(IServiceCollection collection = null) =>
            new JsonConfigurationServiceBuilder(collection == null ? Substitute.For<IServiceCollection>() : collection);
    }


}
