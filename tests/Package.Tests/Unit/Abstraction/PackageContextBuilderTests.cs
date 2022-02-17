using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Package.Abstraction.Entities;
using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Tests.Unit.Abstraction
{

    [TestFixture]
    public class PackageContextBuilderTests
    {        

        [Test]        
        public void Constructor_anyNull_ArgumentNullException()
        {
            for (int i = 0; i < 31; i++)
            {
                BitVector32 mask = new BitVector32(i);
                Assert.Catch<ArgumentNullException>(() =>
                new PackageContextBuilder(
                    mask[1] ? Substitute.For<IRepositoriesProvider>() : null,
                    mask[2] ? Substitute.For<IConfigurationReader>() : null,
                    mask[4] ? Substitute.For<IStringLocalizer<PackageContext>>() : null,
                    mask[8] ? Substitute.For<ILogger<PackageContext>>() : null,
                    mask[16] ? Substitute.For<IServiceScopeFactory>() : null
                ));
            }
        }

        [Test]
        public void Build_void_PackageContext()
        {
            var instance = CreateInstance();

            var context = instance.Build();

            Assert.NotNull(context);
            Assert.NotNull(context.Configuration);
            Assert.NotNull(context.Logger);
            Assert.NotNull(context.RepositoryProvider);
            Assert.NotNull(context.Messages);            
        }



        private PackageContextBuilder CreateInstance() => new PackageContextBuilder(
            Substitute.For<IRepositoriesProvider>(),
            Substitute.For<IConfigurationReader>(),
            Substitute.For<IStringLocalizer<PackageContext>>(),
            Substitute.For<ILogger<PackageContext>>(),
            Substitute.For<IServiceScopeFactory>()
            );
    }

}
