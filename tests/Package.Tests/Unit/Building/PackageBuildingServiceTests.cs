using NSubstitute;
using NUnit.Framework;
using Package.Abstraction.Entities;
using Package.Abstraction.Services;
using Package.Building.Builders;
using Package.Building.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Tests.Unit.Building
{

    [TestFixture]
    public class PackageBuildingServiceTests
    {        
        [Test]        
        public void BuildInvokeTests_Stubs() 
        {

            ServiceArgsStubs stubsArgs = new ServiceArgsStubs();            
            PackageBuildingService service = new PackageBuildingService(
                stubsArgs.BasisBuilder, stubsArgs.EntityBuilder, stubsArgs.PackageBuilder,
                stubsArgs.EntityGrouper, stubsArgs.ContextBuilder);
            // mock, because otherwise exception
            stubsArgs.PackageBuilder.Build(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>())
                .ReturnsForAnyArgs(new PackageBuildingResult("", ""));
            service.Create();

            stubsArgs.BasisBuilder.Received(1).Build(Arg.Any<PackageContext>());
            stubsArgs.ContextBuilder.Received(1).Build();                        
            stubsArgs.PackageBuilder.Received(1).Build(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>());
            stubsArgs.EntityGrouper.DidNotReceive().Group(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<uint>(), Arg.Any<PackageContext>());
            stubsArgs.EntityBuilder.DidNotReceive().Build(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<uint>(), Arg.Any<PackageContext>());

        }

        [Test]
        public void BuildInvokeTests_MoackBasisBuilderAndCancel()
        {

            ServiceArgsStubs stubsArgs = new ServiceArgsStubs();
            PackageBuildingService service = new PackageBuildingService(
                stubsArgs.BasisBuilder, stubsArgs.EntityBuilder, stubsArgs.PackageBuilder,
                stubsArgs.EntityGrouper, stubsArgs.ContextBuilder);
            // mock, because otherwise exception
            stubsArgs.PackageBuilder.Build(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>())
                .ReturnsForAnyArgs(new PackageBuildingResult("", ""));
            var basisEntities = new List<EntityBuildingResult>() {
                    new EntityBuildingResult("", ""),
                    new EntityBuildingResult("", ""),
                    new EntityBuildingResult("", ""),
                };
            stubsArgs.BasisBuilder.Build(Arg.Any<PackageContext>()).ReturnsForAnyArgs(basisEntities);
            service.LevelBuilded += (s, a) => a.Cancel = true;
            service.Create();

            stubsArgs.BasisBuilder.Received(1).Build(Arg.Any<PackageContext>());
            stubsArgs.ContextBuilder.Received(1).Build();            
            stubsArgs.PackageBuilder.Received(1).Build(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>());
            stubsArgs.EntityGrouper.DidNotReceive().Group(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<uint>(), Arg.Any<PackageContext>());
            stubsArgs.EntityBuilder.DidNotReceive().Build(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<uint>(), Arg.Any<PackageContext>());
        }


        [Test]
        public void BuildInvokeTests_MockBasisBuilder()
        {

            ServiceArgsStubs stubsArgs = new ServiceArgsStubs();
            PackageBuildingService service = new PackageBuildingService(
                stubsArgs.BasisBuilder, stubsArgs.EntityBuilder, stubsArgs.PackageBuilder,
                stubsArgs.EntityGrouper, stubsArgs.ContextBuilder);
            // mock, because otherwise exception
            stubsArgs.PackageBuilder.Build(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>())
                .ReturnsForAnyArgs(new PackageBuildingResult("", ""));
            var basisEntities = new List<EntityBuildingResult>() {
                    new EntityBuildingResult("", ""),
                    new EntityBuildingResult("", ""),
                    new EntityBuildingResult("", ""),
                };
            stubsArgs.BasisBuilder.Build(Arg.Any<PackageContext>()).ReturnsForAnyArgs(basisEntities);
            service.Create();

            stubsArgs.BasisBuilder.Received(1).Build(Arg.Any<PackageContext>());            
            stubsArgs.EntityGrouper.Received(1).Group(Arg.Any<IEnumerable<Entity_>>(), 0, Arg.Any<PackageContext>());
            stubsArgs.ContextBuilder.Received(1).Build();
            stubsArgs.PackageBuilder.Received(1).Build(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>());            
            stubsArgs.EntityBuilder.DidNotReceive().Build(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<uint>(), Arg.Any<PackageContext>());
        }

        [Test]
        public void BuildInvokeTests_MockBasisBuilderAndGrouper()
        {

            ServiceArgsStubs stubsArgs = new ServiceArgsStubs();
            PackageBuildingService service = new PackageBuildingService(
                stubsArgs.BasisBuilder, stubsArgs.EntityBuilder, stubsArgs.PackageBuilder,
                stubsArgs.EntityGrouper, stubsArgs.ContextBuilder);
            // mock, because otherwise exception
            stubsArgs.PackageBuilder.Build(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>()).ReturnsForAnyArgs(new PackageBuildingResult("", ""));                        
            var groupingResult = new List<PackageEntitiesGroup> 
            { 
                new PackageEntitiesGroup(
                    new List<Entity_>()
                    {
                        new Entity_("", "", new List<Entity_>(), new Dictionary<string, string>(), new Dictionary<string, UserParameter_>()),
                        new Entity_("", "", new List<Entity_>(), new Dictionary<string, string>(), new Dictionary<string, UserParameter_>())
                    },
                new GroupKey(new Dictionary<string, string>())) 
            };
            var basisEntities = new List<EntityBuildingResult>() {
                    new EntityBuildingResult("", ""),
                    new EntityBuildingResult("", ""),
                    new EntityBuildingResult("", ""),
                };
            stubsArgs.BasisBuilder.Build(Arg.Any<PackageContext>()).ReturnsForAnyArgs(basisEntities);
            stubsArgs.EntityGrouper.Group(Arg.Any<IEnumerable<Entity_>>(), 0, Arg.Any<PackageContext>()).Returns(groupingResult);
            stubsArgs.EntityBuilder.Build(Arg.Any<IEnumerable<Entity_>>(), 1, Arg.Any<PackageContext>()).Returns(new EntityBuildingResult("", ""));
            service.Create();

            stubsArgs.BasisBuilder.Received(1).Build(Arg.Any<PackageContext>());
            stubsArgs.EntityGrouper.Received(1).Group(Arg.Any<IEnumerable<Entity_>>(), 1, Arg.Any<PackageContext>());
            stubsArgs.EntityBuilder.Received(1).Build(groupingResult[0], 1, Arg.Any<PackageContext>());
            stubsArgs.ContextBuilder.Received(1).Build();
            stubsArgs.PackageBuilder.Received(1).Build(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>());
        }


        //async tests

        [Test]
        public async Task BuildAsyncInvokeTests_Stubs()
        {

            ServiceArgsStubs stubsArgs = new ServiceArgsStubs();
            PackageBuildingService service = new PackageBuildingService(
                stubsArgs.BasisBuilder, stubsArgs.EntityBuilder, stubsArgs.PackageBuilder,
                stubsArgs.EntityGrouper, stubsArgs.ContextBuilder);
            // mock, because otherwise exception
            stubsArgs.PackageBuilder.BuildAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>())
                .ReturnsForAnyArgs(Task.FromResult( new PackageBuildingResult("", "")));
            await service.CreateAsync(CancellationToken.None);

            stubsArgs.BasisBuilder.Received(1).BuildAsync(Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            stubsArgs.ContextBuilder.Received(1).Build();
            stubsArgs.PackageBuilder.Received(1).BuildAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            stubsArgs.EntityGrouper.DidNotReceive().GroupAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<uint>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            stubsArgs.EntityBuilder.DidNotReceive().BuildAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<uint>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());

        }

        [Test]
        public async Task BuildAsyncInvokeTests_MoackBasisBuilderAndCancel()
        {

            ServiceArgsStubs stubsArgs = new ServiceArgsStubs();
            PackageBuildingService service = new PackageBuildingService(
                stubsArgs.BasisBuilder, stubsArgs.EntityBuilder, stubsArgs.PackageBuilder,
                stubsArgs.EntityGrouper, stubsArgs.ContextBuilder);
            // mock, because otherwise exception
            stubsArgs.PackageBuilder.BuildAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>())
                .ReturnsForAnyArgs(Task.FromResult(new PackageBuildingResult("", "")));
            var basisEntities = new List<EntityBuildingResult>() {
                    new EntityBuildingResult("", ""),
                    new EntityBuildingResult("", ""),
                    new EntityBuildingResult("", ""),
                };
            stubsArgs.BasisBuilder.BuildAsync(Arg.Any<PackageContext>(), Arg.Any<CancellationToken>()).ReturnsForAnyArgs(Task.FromResult((IEnumerable<EntityBuildingResult>)basisEntities));
            service.LevelBuilded += (s, a) => a.Cancel = true;
            await service.CreateAsync(CancellationToken.None);

            stubsArgs.BasisBuilder.Received(1).BuildAsync(Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            stubsArgs.ContextBuilder.Received(1).Build();
            stubsArgs.PackageBuilder.Received(1).BuildAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            stubsArgs.EntityGrouper.DidNotReceive().GroupAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<uint>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            stubsArgs.EntityBuilder.DidNotReceive().BuildAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<uint>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
        }


        [Test]
        public async Task BuildAsyncInvokeTests_MockBasisBuilder()
        {
            ServiceArgsStubs stubsArgs = new ServiceArgsStubs();
            PackageBuildingService service = new PackageBuildingService(
                stubsArgs.BasisBuilder, stubsArgs.EntityBuilder, stubsArgs.PackageBuilder,
                stubsArgs.EntityGrouper, stubsArgs.ContextBuilder);
            // mock, because otherwise exception
            stubsArgs.PackageBuilder.BuildAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>())
                .ReturnsForAnyArgs(Task.FromResult(new PackageBuildingResult("", "")));
            var basisEntities = new List<EntityBuildingResult>() {
                    new EntityBuildingResult("", ""),
                    new EntityBuildingResult("", ""),
                    new EntityBuildingResult("", ""),
                };
            stubsArgs.BasisBuilder.BuildAsync(Arg.Any<PackageContext>(), Arg.Any<CancellationToken>()).ReturnsForAnyArgs(Task.FromResult((IEnumerable<EntityBuildingResult>)basisEntities));
            await service.CreateAsync(CancellationToken.None);

            stubsArgs.BasisBuilder.Received(1).BuildAsync(Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            stubsArgs.EntityGrouper.Received(1).GroupAsync(Arg.Any<IEnumerable<Entity_>>(), 0, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            stubsArgs.ContextBuilder.Received(1).Build();
            stubsArgs.PackageBuilder.Received(1).BuildAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            stubsArgs.EntityBuilder.DidNotReceive().BuildAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<uint>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task BuildAsyncInvokeTests_MockBasisBuilderAndGrouper()
        {

            ServiceArgsStubs stubsArgs = new ServiceArgsStubs();
            PackageBuildingService service = new PackageBuildingService(
                stubsArgs.BasisBuilder, stubsArgs.EntityBuilder, stubsArgs.PackageBuilder,
                stubsArgs.EntityGrouper, stubsArgs.ContextBuilder);
            // mock, because otherwise exception
            stubsArgs.PackageBuilder.BuildAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>()).ReturnsForAnyArgs(Task.FromResult( new PackageBuildingResult("", "")));
            var groupingResult = new List<PackageEntitiesGroup>
            {
                new PackageEntitiesGroup(
                    new List<Entity_>()
                    {
                        new Entity_("", "", new List<Entity_>(), new Dictionary<string, string>(), new Dictionary<string, UserParameter_>()),
                        new Entity_("", "", new List<Entity_>(), new Dictionary<string, string>(), new Dictionary<string, UserParameter_>())
                    },
                new GroupKey(new Dictionary<string, string>()))
            };
            var basisEntities = new List<EntityBuildingResult>() {
                    new EntityBuildingResult("", ""),
                    new EntityBuildingResult("", ""),
                    new EntityBuildingResult("", ""),
                };
            stubsArgs.BasisBuilder.BuildAsync(Arg.Any<PackageContext>(), Arg.Any<CancellationToken>()).ReturnsForAnyArgs(Task.FromResult((IEnumerable<EntityBuildingResult>)basisEntities));
            stubsArgs.EntityGrouper.GroupAsync(Arg.Any<IEnumerable<Entity_>>(), 0, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult((IEnumerable<IGrouping<GroupKey, Entity_>>)groupingResult));
            stubsArgs.EntityBuilder.BuildAsync(Arg.Any<IEnumerable<Entity_>>(), 1, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(new EntityBuildingResult("", "")));
            await service.CreateAsync(CancellationToken.None);

            stubsArgs.BasisBuilder.Received(1).BuildAsync(Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            stubsArgs.EntityGrouper.Received(1).GroupAsync(Arg.Any<IEnumerable<Entity_>>(), 1, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            stubsArgs.EntityBuilder.Received(1).BuildAsync(groupingResult[0], 1, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            stubsArgs.ContextBuilder.Received(1).Build();
            stubsArgs.PackageBuilder.Received(1).BuildAsync(Arg.Any<IEnumerable<Entity_>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
        }


        private class ServiceArgsStubs
        {
            public readonly IEntityBasisBuilder BasisBuilder;
            public readonly IEntityBuilder EntityBuilder;
            public readonly IPackageBuilder PackageBuilder;
            public readonly IEntityGrouper EntityGrouper;
            public readonly IPackageContextBuilder ContextBuilder;

            public ServiceArgsStubs()
            {
                 BasisBuilder= Substitute.For<IEntityBasisBuilder>();
                 EntityBuilder = Substitute.For<IEntityBuilder>();
                 PackageBuilder = Substitute.For<IPackageBuilder>();
                 EntityGrouper = Substitute.For<IEntityGrouper>();
                 ContextBuilder = Substitute.For<IPackageContextBuilder>();
            }
        }        
    }

}
