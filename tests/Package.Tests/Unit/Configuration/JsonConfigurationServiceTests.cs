using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Package.Configuration.Services;
using NSubstitute;
using Package.Configuration.Exceptions;
using Newtonsoft.Json.Linq;

namespace Package.Tests.Unit.Configuration
{

    [TestFixture]
    public class JsonConfigurationServiceTests
    {        

        [Test]     
        public void Ctor_IncorrectBinder_ArgumentException() 
        {
            var ex = Assert.Catch<ArgumentException>(() => CreateInstance(new Dictionary<Type, object>
            { { typeof(string), new object() } }));
            Assert.AreEqual(ex.Message, "Incorrect binder instance");
        }

        [Test]
        public void Ctor_IncorrectBinderRelation_ArgumentException()
        {
            var ex = Assert.Catch<ArgumentException>(() => CreateInstance(new Dictionary<Type, object>
            { { typeof(string), Substitute.For<IJsonConfigurationBinder<object>>()} }));
            Assert.AreEqual(ex.Message, "Incorrect relation of type and instance");
        }

        [Test]
        public void Get_NotExistsBinder_ConfigurationException()
        {
            var configService = CreateInstance(new Dictionary<Type, object>
            { { typeof(string), Substitute.For<IJsonConfigurationBinder<string>>()} });
            var ex = Assert.Catch<ConfigurationException>(() => configService.Get<AppDomain>());
            Assert.AreEqual(ex.Message, $"Not found configuration binder for type {typeof(AppDomain)}");
        }

        [Test]
        public void Get_EmptyBinders_ConfigurationException()
        {
            var configService = CreateInstance(new Dictionary<Type, object>());
            var ex = Assert.Catch<ConfigurationException>(() => configService.Get<AppDomain>());
            Assert.AreEqual(ex.Message, $"Not found configuration binder for type {typeof(AppDomain)}");

        }

        [Test]
        public void Get_ExistsBinder_StringEmpty()
        {                        
            var configService = CreateInstance(new Dictionary<Type, object>
            { { typeof(string), Substitute.For<IJsonConfigurationBinder<string>>()} });

            var result =  configService.Get<string>();

            Assert.IsEmpty(result);
        }

        [Test]
        public void Set_NotExistsBinder_ConfigurationException()
        {
            var configService = CreateInstance(new Dictionary<Type, object>
            { { typeof(string), Substitute.For<IJsonConfigurationBinder<string>>()} });
            var ex = Assert.Catch<ConfigurationException>(() => configService.Set(AppDomain.CurrentDomain));
            Assert.AreEqual(ex.Message, $"Not found configuration binder for type {typeof(AppDomain)}");
        }

        [Test]
        public void Set_EmptyBinders_ConfigurationException()
        {
            var configService = CreateInstance(new Dictionary<Type, object>());
            var ex = Assert.Catch<ConfigurationException>(() => configService.Set(AppDomain.CurrentDomain));
            Assert.AreEqual(ex.Message, $"Not found configuration binder for type {typeof(AppDomain)}");
        }

        [Test]
        public void Set_ExistsBinder_void()
        {
            var configService = CreateInstance(new Dictionary<Type, object>
            { { typeof(string), Substitute.For<IJsonConfigurationBinder<string>>()} });

            configService.Set("string");            
        }

        [Test]
        public void INVOKES_Get_NotExistsBinder()
        {
            var provider = Substitute.For<IJsonConfigurationProvider>();
            var binder = Substitute.For<IJsonConfigurationBinder<string>>();            
            var configService = new JsonConfigurationService(provider, new Dictionary<Type, object>()
            { {typeof(string), binder } });

            var ex = Assert.Catch<ConfigurationException>(() => configService.Get<AppDomain>());

            provider.DidNotReceive().Get();
            provider.DidNotReceive().Set(Arg.Any<JObject>());
            binder.DidNotReceive().Get(Arg.Any<JObject>());
            binder.DidNotReceive().Set(Arg.Any<string>(), Arg.Any<JObject>());
            Assert.AreEqual(ex.Message, $"Not found configuration binder for type {typeof(AppDomain)}");
        }


        [Test]
        public void INVOKES_Get_EmptyBinders()
        {
            var provider = Substitute.For<IJsonConfigurationProvider>();            
            var configService = new JsonConfigurationService(provider, new Dictionary<Type, object>());

            var ex = Assert.Catch<ConfigurationException>(() => configService.Get<AppDomain>());

            provider.DidNotReceive().Get();
            provider.DidNotReceive().Set(Arg.Any<JObject>());
            Assert.AreEqual(ex.Message, $"Not found configuration binder for type {typeof(AppDomain)}");
        }

        [Test]
        public void INVOKES_Get_ExistsBinder()
        {
            var provider = Substitute.For<IJsonConfigurationProvider>();
            var binder = Substitute.For<IJsonConfigurationBinder<string>>();            
            var configService = new JsonConfigurationService(provider, new Dictionary<Type, object>()
            { {typeof(string), binder } });

            var result = configService.Get<string>();

            provider.Received(1).Get();
            binder.Received(1).Get(Arg.Any<JObject>());
            provider.DidNotReceive().Set(Arg.Any<JObject>());
            binder.DidNotReceive().Set(Arg.Any<string>(), Arg.Any<JObject>());
            Assert.IsEmpty(result);
        }

        // this

        [Test]
        public void INVOKES_Set_NotExistsBinder()
        {
            var provider = Substitute.For<IJsonConfigurationProvider>();
            var binder = Substitute.For<IJsonConfigurationBinder<string>>();            
            var configService = new JsonConfigurationService(provider, new Dictionary<Type, object>()
            { {typeof(string), binder } });

            var ex = Assert.Catch<ConfigurationException>(() => configService.Set(AppDomain.CurrentDomain));

            provider.DidNotReceive().Get();
            provider.DidNotReceive().Set(Arg.Any<JObject>());
            binder.DidNotReceive().Get(Arg.Any<JObject>());
            binder.DidNotReceive().Set(Arg.Any<string>(), Arg.Any<JObject>());
            Assert.AreEqual(ex.Message, $"Not found configuration binder for type {typeof(AppDomain)}");
        }

        [Test]
        public void INVOKES_Set_EmptyBinders()
        {
            var provider = Substitute.For<IJsonConfigurationProvider>();
            var binder = Substitute.For<IJsonConfigurationBinder<string>>();            
            var configService = new JsonConfigurationService(provider, new Dictionary<Type, object>()
            { {typeof(string), binder } });

            var ex = Assert.Catch<ConfigurationException>(() => configService.Set(AppDomain.CurrentDomain));

            provider.DidNotReceive().Get();
            provider.DidNotReceive().Set(Arg.Any<JObject>());
            binder.DidNotReceive().Get(Arg.Any<JObject>());
            binder.DidNotReceive().Set(Arg.Any<string>(), Arg.Any<JObject>());
            Assert.AreEqual(ex.Message, $"Not found configuration binder for type {typeof(AppDomain)}");
        }

        [Test]
        public void INVOKES_Set_ExistsBinder()
        {
            var provider = Substitute.For<IJsonConfigurationProvider>();
            var binder = Substitute.For<IJsonConfigurationBinder<string>>();            
            var configService = new JsonConfigurationService(provider, new Dictionary<Type, object>()
            { {typeof(string), binder } });            

            configService.Set("string");

            provider.Received(1).Get();
            provider.Received(1).Set(Arg.Any<JObject>());
            binder.DidNotReceive().Get(Arg.Any<JObject>());
            binder.Received(1).Set(Arg.Any<string>(), Arg.Any<JObject>());
        }


        private JsonConfigurationService CreateInstance(Dictionary<Type, object> binders) =>
            new JsonConfigurationService(Substitute.For<IJsonConfigurationProvider>(), binders);

    }

}
