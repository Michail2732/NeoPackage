using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Package.Output.Services;
using NSubstitute;
using Package.Output.Outputers;
using Package.Abstraction.Services;
using Package.Abstraction.Entities;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Package.Tests.Unit.Output
{

    [TestFixture]
    public class PackageOutputServiceTests
    {        

        [Test]     
        public void Output_EmptyPackage_PackageReport() 
        {
            var package = new Package_("", "", new List<Entity_>());
            var instance = Instance();

            var result = instance.Output(package);

            Assert.NotNull(result);
            Assert.NotNull(result.EntitiesReports);            
            Assert.IsTrue(result.EntitiesReports.Count == 0);            
        }

        [Test]
        public void Output_PackageWithOneEntity_PackageReport()
        {
            var parameters = new Dictionary<string, string>();
            var userParameters = new Dictionary<string, UserParameter_>();
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(),
                parameters, userParameters) };
            var package = new Package_("", "", entities);
            var instance = Instance();

            var result = instance.Output(package);

            Assert.NotNull(result);
            Assert.NotNull(result.EntitiesReports);
            Assert.IsTrue(result.EntitiesReports.Count == 1);
            Assert.IsTrue(result.EntitiesReports[0].ParametersResult.Count == 0);
            Assert.IsTrue(result.EntitiesReports[0].UserParametersResult.Count == 0);            
        }


        [Test]
        public void Output_PackageWithOneEntityWithParam_PackageReport()
        {
            var parameters = new Dictionary<string, string>() { {"param1", "value1" } };
            var userParameters = new Dictionary<string, UserParameter_>();
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(),
                parameters, userParameters) };
            var package = new Package_("", "", entities);
            var instance = Instance();

            var result = instance.Output(package);

            Assert.NotNull(result);
            Assert.NotNull(result.EntitiesReports);
            Assert.IsTrue(result.EntitiesReports.Count == 1);
            Assert.IsTrue(result.EntitiesReports[0].ParametersResult.Count == 1);
            Assert.IsTrue(result.EntitiesReports[0].UserParametersResult.Count == 0);
        }

        [Test]
        public void Output_PackageWithOneEntityWithParamAndUserParam_PackageReport()
        {
            var parameters = new Dictionary<string, string>() { { "param1", "value1" } };
            var userParameters = new Dictionary<string, UserParameter_>() { { "param2", new UserParameter_("param2", new object())} };
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(),
                parameters, userParameters) };
            var package = new Package_("", "", entities);
            var instance = Instance();

            var result = instance.Output(package);

            Assert.NotNull(result);
            Assert.NotNull(result.EntitiesReports);
            Assert.IsTrue(result.EntitiesReports.Count == 1);
            Assert.IsTrue(result.EntitiesReports[0].ParametersResult.Count == 1);
            Assert.IsTrue(result.EntitiesReports[0].UserParametersResult.Count == 1);
        }        

        [Test]
        public async Task  OutputAsync_EmptyPackage_PackageReport()
        {
            var package = new Package_("", "", new List<Entity_>());
            var instance = Instance();

            var result = await instance.OutputAsync(package, CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotNull(result.EntitiesReports);
            Assert.IsTrue(result.EntitiesReports.Count == 0);
        }

        [Test]
        public async Task OutputAsync_PackageWithOneEntity_PackageReport()
        {
            var parameters = new Dictionary<string, string>();
            var userParameters = new Dictionary<string, UserParameter_>();
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(),
                parameters, userParameters) };
            var package = new Package_("", "", entities);
            var instance = Instance();

            var result = await instance.OutputAsync(package, CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotNull(result.EntitiesReports);
            Assert.IsTrue(result.EntitiesReports.Count == 1);
            Assert.IsTrue(result.EntitiesReports[0].ParametersResult.Count == 0);
            Assert.IsTrue(result.EntitiesReports[0].UserParametersResult.Count == 0);
        }


        [Test]
        public async Task OutputAsync_PackageWithOneEntityWithParam_PackageReport()
        {
            var parameters = new Dictionary<string, string>() { { "param1", "value1" } };
            var userParameters = new Dictionary<string, UserParameter_>();
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(),
                parameters, userParameters) };
            var package = new Package_("", "", entities);
            var instance = Instance();

            var result = await instance.OutputAsync(package, CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotNull(result.EntitiesReports);
            Assert.IsTrue(result.EntitiesReports.Count == 1);
            Assert.IsTrue(result.EntitiesReports[0].ParametersResult.Count == 1);
            Assert.IsTrue(result.EntitiesReports[0].UserParametersResult.Count == 0);
        }

        [Test]
        public async Task OutputAsync_PackageWithOneEntityWithParamAndUserParam_PackageReport()
        {
            var parameters = new Dictionary<string, string>() { { "param1", "value1" } };
            var userParameters = new Dictionary<string, UserParameter_>() { { "param2", new UserParameter_("param2", new object()) } };
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(),
                parameters, userParameters) };
            var package = new Package_("", "", entities);
            var instance = Instance();

            var result = await instance.OutputAsync(package, CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotNull(result.EntitiesReports);
            Assert.IsTrue(result.EntitiesReports.Count == 1);
            Assert.IsTrue(result.EntitiesReports[0].ParametersResult.Count == 1);
            Assert.IsTrue(result.EntitiesReports[0].UserParametersResult.Count == 1);
        }

        [Test]
        public void EntityOutputted_PackageWithOneEntity_void()
        {
            var parameters = new Dictionary<string, string>();
            var userParameters = new Dictionary<string, UserParameter_>();
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(),
                parameters, userParameters) };
            var package = new Package_("", "", entities);
            int count = 0;

            var instance = Instance();
            instance.EntityOutputted += (s, a) => count++;            

            var result = instance.Output(package);

            Assert.NotNull(result);
            Assert.NotNull(result.EntitiesReports);
            Assert.IsTrue(result.EntitiesReports.Count == 1);
            Assert.IsTrue(result.EntitiesReports[0].ParametersResult.Count == 0);
            Assert.IsTrue(result.EntitiesReports[0].UserParametersResult.Count == 0);
            Assert.IsTrue(count == 1);
        }

        [Test]
        public void EntityOutputted_PackageWithTwoEntity_void()
        {
            var parameters = new Dictionary<string, string>();
            var userParameters = new Dictionary<string, UserParameter_>();
            var entities = new List<Entity_> 
            {
                new Entity_("", "", new List<Entity_>(), parameters, userParameters),
                new Entity_("", "", new List<Entity_>(), parameters, userParameters)
            };
            var package = new Package_("", "", entities);
            int count = 0;

            var instance = Instance();
            instance.EntityOutputted += (s, a) => count++;

            var result = instance.Output(package);

            Assert.NotNull(result);
            Assert.NotNull(result.EntitiesReports);
            Assert.IsTrue(result.EntitiesReports.Count == 2);
            Assert.IsTrue(result.EntitiesReports[0].ParametersResult.Count == 0);
            Assert.IsTrue(result.EntitiesReports[0].UserParametersResult.Count == 0);
            Assert.IsTrue(result.EntitiesReports[1].ParametersResult.Count == 0);
            Assert.IsTrue(result.EntitiesReports[1].UserParametersResult.Count == 0);
            Assert.IsTrue(count == 2);
        }


        [Test]
        public async Task EntityOutputtedAsync_PackageWithOneEntity_void()
        {
            var parameters = new Dictionary<string, string>();
            var userParameters = new Dictionary<string, UserParameter_>();
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(),
                parameters, userParameters) };
            var package = new Package_("", "", entities);
            int count = 0;

            var instance = Instance();
            instance.EntityOutputted += (s, a) => count++;

            var result = await instance.OutputAsync(package, CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotNull(result.EntitiesReports);
            Assert.IsTrue(result.EntitiesReports.Count == 1);
            Assert.IsTrue(result.EntitiesReports[0].ParametersResult.Count == 0);
            Assert.IsTrue(result.EntitiesReports[0].UserParametersResult.Count == 0);
            Assert.IsTrue(count == 1);
        }

        [Test]
        public async Task EntityOutputtedAsync_PackageWithTwoEntity_void()
        {
            var parameters = new Dictionary<string, string>();
            var userParameters = new Dictionary<string, UserParameter_>();
            var entities = new List<Entity_>
            {
                new Entity_("", "", new List<Entity_>(), parameters, userParameters),
                new Entity_("", "", new List<Entity_>(), parameters, userParameters)
            };
            var package = new Package_("", "", entities);
            int count = 0;

            var instance = Instance();
            instance.EntityOutputted += (s, a) => count++;

            var result = await instance.OutputAsync(package, CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotNull(result.EntitiesReports);
            Assert.IsTrue(result.EntitiesReports.Count == 2);
            Assert.IsTrue(result.EntitiesReports[0].ParametersResult.Count == 0);
            Assert.IsTrue(result.EntitiesReports[0].UserParametersResult.Count == 0);
            Assert.IsTrue(result.EntitiesReports[1].ParametersResult.Count == 0);
            Assert.IsTrue(result.EntitiesReports[1].UserParametersResult.Count == 0);
            Assert.IsTrue(count == 2);
        }

        [Test]
        public void INVOKES_Output_EmptyPackage()
        {
            var packageOutputer = Substitute.For<IPackageOutputer>();
            var entityOutputer = Substitute.For<IEntityOutputer>();
            var parameterOutputer = Substitute.For<IParameterOutputer>();
            var userParameterOutputer = Substitute.For<IUserParameterOutputer>();
            var contextBuilder = Substitute.For<IPackageContextBuilder>();
            var instance = new PackageOutputService(packageOutputer, entityOutputer, parameterOutputer, userParameterOutputer, contextBuilder);
            var package = new Package_("", "", new  List<Entity_>());

            instance.Output(package);

            contextBuilder.Received(1).Build();
            packageOutputer.Received(1).Output(package, Arg.Any<PackageContext>());
            packageOutputer.DidNotReceive().OutputAsync(package, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            entityOutputer.DidNotReceive().Output(Arg.Any<Entity_>(), Arg.Any<PackageContext>());
            entityOutputer.DidNotReceive().OutputAsync(Arg.Any<Entity_>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            parameterOutputer.DidNotReceive().Output(Arg.Any<KeyValuePair<string, string>>(), Arg.Any<PackageContext>());
            parameterOutputer.DidNotReceive().OutputAsync(Arg.Any<KeyValuePair<string, string>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            userParameterOutputer.DidNotReceive().Output(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>());
            userParameterOutputer.DidNotReceive().OutputAsync(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task INVOKES_OutputAsync_EmptyPackage()
        {
            var packageOutputer = Substitute.For<IPackageOutputer>();
            var entityOutputer = Substitute.For<IEntityOutputer>();
            var parameterOutputer = Substitute.For<IParameterOutputer>();
            var userParameterOutputer = Substitute.For<IUserParameterOutputer>();
            var contextBuilder = Substitute.For<IPackageContextBuilder>();
            var instance = new PackageOutputService(packageOutputer, entityOutputer, parameterOutputer, userParameterOutputer, contextBuilder);
            var package = new Package_("", "", new List<Entity_>());

            await instance.OutputAsync(package, CancellationToken.None);

            contextBuilder.Received(1).Build();
            await packageOutputer.Received(1).OutputAsync(package, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());

            packageOutputer.DidNotReceive().Output(package, Arg.Any<PackageContext>());            
            entityOutputer.DidNotReceive().Output(Arg.Any<Entity_>(), Arg.Any<PackageContext>());
            await entityOutputer.DidNotReceive().OutputAsync(Arg.Any<Entity_>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            parameterOutputer.DidNotReceive().Output(Arg.Any<KeyValuePair<string, string>>(), Arg.Any<PackageContext>());
            await parameterOutputer.DidNotReceive().OutputAsync(Arg.Any<KeyValuePair<string, string>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            userParameterOutputer.DidNotReceive().Output(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>());
            await userParameterOutputer.DidNotReceive().OutputAsync(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
        }


        [Test]
        public void INVOKES_Output_PackageWithOneEntity()
        {
            var parameters = new Dictionary<string, string>();
            var userParameters = new Dictionary<string, UserParameter_>();
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(),parameters, userParameters) };
            var package = new Package_("", "", entities);                                            
            var packageOutputer = Substitute.For<IPackageOutputer>();
            var entityOutputer = Substitute.For<IEntityOutputer>();
            var parameterOutputer = Substitute.For<IParameterOutputer>();
            var userParameterOutputer = Substitute.For<IUserParameterOutputer>();
            var contextBuilder = Substitute.For<IPackageContextBuilder>();
            var instance = new PackageOutputService(packageOutputer, entityOutputer, parameterOutputer, userParameterOutputer, contextBuilder);            

            instance.Output(package);

            contextBuilder.Received(1).Build();
            packageOutputer.Received(1).Output(package, Arg.Any<PackageContext>());
            packageOutputer.DidNotReceive().OutputAsync(package, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            entityOutputer.Received(1).Output(package.Entities[0], Arg.Any<PackageContext>());
            entityOutputer.DidNotReceive().OutputAsync(Arg.Any<Entity_>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            parameterOutputer.DidNotReceive().Output(Arg.Any<KeyValuePair<string, string>>(), Arg.Any<PackageContext>());
            parameterOutputer.DidNotReceive().OutputAsync(Arg.Any<KeyValuePair<string, string>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            userParameterOutputer.DidNotReceive().Output(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>());
            userParameterOutputer.DidNotReceive().OutputAsync(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
        }


        [Test]
        public async Task INVOKES_OutputAsync_PackageWithOneEntity()
        {
            var parameters = new Dictionary<string, string>();
            var userParameters = new Dictionary<string, UserParameter_>();
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(), parameters, userParameters) };
            var package = new Package_("", "", entities);
            var packageOutputer = Substitute.For<IPackageOutputer>();
            var entityOutputer = Substitute.For<IEntityOutputer>();
            var parameterOutputer = Substitute.For<IParameterOutputer>();
            var userParameterOutputer = Substitute.For<IUserParameterOutputer>();
            var contextBuilder = Substitute.For<IPackageContextBuilder>();
            var instance = new PackageOutputService(packageOutputer, entityOutputer, parameterOutputer, userParameterOutputer, contextBuilder);

            await instance.OutputAsync(package, CancellationToken.None);

            contextBuilder.Received(1).Build();
            packageOutputer.DidNotReceive().Output(package, Arg.Any<PackageContext>());
            await packageOutputer.Received(1).OutputAsync(package, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            entityOutputer.DidNotReceive().Output(Arg.Any<Entity_>(), Arg.Any<PackageContext>());
            await entityOutputer.Received(1).OutputAsync(package.Entities[0], Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            parameterOutputer.DidNotReceive().Output(Arg.Any<KeyValuePair<string, string>>(), Arg.Any<PackageContext>());
            await parameterOutputer.DidNotReceive().OutputAsync(Arg.Any<KeyValuePair<string, string>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            userParameterOutputer.DidNotReceive().Output(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>());
            await userParameterOutputer.DidNotReceive().OutputAsync(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public void INVOKES_Output_PackageWithOneEntityWithParameter()
        {
            var parameters = new Dictionary<string, string>() { { "parameter1", "value1" } };
            var userParameters = new Dictionary<string, UserParameter_>();
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(), parameters, userParameters) };
            var package = new Package_("", "", entities);
            var packageOutputer = Substitute.For<IPackageOutputer>();
            var entityOutputer = Substitute.For<IEntityOutputer>();
            var parameterOutputer = Substitute.For<IParameterOutputer>();
            var userParameterOutputer = Substitute.For<IUserParameterOutputer>();
            var contextBuilder = Substitute.For<IPackageContextBuilder>();
            var instance = new PackageOutputService(packageOutputer, entityOutputer, parameterOutputer, userParameterOutputer, contextBuilder);

            instance.Output(package);

            contextBuilder.Received(1).Build();
            packageOutputer.Received(1).Output(package, Arg.Any<PackageContext>());
            packageOutputer.DidNotReceive().OutputAsync(package, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            entityOutputer.Received(1).Output(package.Entities[0], Arg.Any<PackageContext>());
            entityOutputer.DidNotReceive().OutputAsync(Arg.Any<Entity_>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            parameterOutputer.Received(1).Output(parameters.First(), Arg.Any<PackageContext>());
            parameterOutputer.DidNotReceive().OutputAsync(Arg.Any<KeyValuePair<string, string>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            userParameterOutputer.DidNotReceive().Output(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>());
            userParameterOutputer.DidNotReceive().OutputAsync(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task INVOKES_OutputAsync_PackageWithOneEntityWithParameter()
        {
            var parameters = new Dictionary<string, string>() { { "parameter1", "value1" } };
            var userParameters = new Dictionary<string, UserParameter_>();
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(), parameters, userParameters) };
            var package = new Package_("", "", entities);
            var packageOutputer = Substitute.For<IPackageOutputer>();
            var entityOutputer = Substitute.For<IEntityOutputer>();
            var parameterOutputer = Substitute.For<IParameterOutputer>();
            var userParameterOutputer = Substitute.For<IUserParameterOutputer>();
            var contextBuilder = Substitute.For<IPackageContextBuilder>();
            var instance = new PackageOutputService(packageOutputer, entityOutputer, parameterOutputer, userParameterOutputer, contextBuilder);

            await instance.OutputAsync(package, CancellationToken.None);

            contextBuilder.Received(1).Build();
            packageOutputer.DidNotReceive().Output(package, Arg.Any<PackageContext>());
            await packageOutputer.Received(1).OutputAsync(package, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            entityOutputer.DidNotReceive().Output(Arg.Any<Entity_>(), Arg.Any<PackageContext>());
            await entityOutputer.Received(1).OutputAsync(package.Entities[0], Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            parameterOutputer.DidNotReceive().Output(Arg.Any<KeyValuePair<string, string>>(), Arg.Any<PackageContext>());
            await parameterOutputer.Received(1).OutputAsync(parameters.First(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            userParameterOutputer.DidNotReceive().Output(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>());
            await userParameterOutputer.DidNotReceive().OutputAsync(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public void INVOKES_Output_PackageWithOneEntityWithParameterAndUserParameter()
        {
            var parameters = new Dictionary<string, string>() { { "parameter1", "value1" } };
            var userParameters = new Dictionary<string, UserParameter_>() { { "parameter2", new UserParameter_("parameter2", new object())} };
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(), parameters, userParameters) };
            var package = new Package_("", "", entities);
            var packageOutputer = Substitute.For<IPackageOutputer>();
            var entityOutputer = Substitute.For<IEntityOutputer>();
            var parameterOutputer = Substitute.For<IParameterOutputer>();
            var userParameterOutputer = Substitute.For<IUserParameterOutputer>();
            var contextBuilder = Substitute.For<IPackageContextBuilder>();
            var instance = new PackageOutputService(packageOutputer, entityOutputer, parameterOutputer, userParameterOutputer, contextBuilder);

            instance.Output(package);

            contextBuilder.Received(1).Build();
            packageOutputer.Received(1).Output(package, Arg.Any<PackageContext>());
            packageOutputer.DidNotReceive().OutputAsync(package, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            entityOutputer.Received(1).Output(package.Entities[0], Arg.Any<PackageContext>());
            entityOutputer.DidNotReceive().OutputAsync(Arg.Any<Entity_>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            parameterOutputer.Received(1).Output(parameters.First(), Arg.Any<PackageContext>());
            parameterOutputer.DidNotReceive().OutputAsync(Arg.Any<KeyValuePair<string, string>>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            userParameterOutputer.Received(1).Output(userParameters.First().Value, Arg.Any<PackageContext>());
            userParameterOutputer.DidNotReceive().OutputAsync(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
        }


        [Test]
        public async Task INVOKES_OutputAsync_PackageWithOneEntityWithParameterAndUserParameter()
        {
            var parameters = new Dictionary<string, string>() { { "parameter1", "value1" } };
            var userParameters = new Dictionary<string, UserParameter_>() { { "parameter2", new UserParameter_("parameter2", new object()) } };
            var entities = new List<Entity_> { new Entity_("", "", new List<Entity_>(), parameters, userParameters) };
            var package = new Package_("", "", entities);
            var packageOutputer = Substitute.For<IPackageOutputer>();
            var entityOutputer = Substitute.For<IEntityOutputer>();
            var parameterOutputer = Substitute.For<IParameterOutputer>();
            var userParameterOutputer = Substitute.For<IUserParameterOutputer>();
            var contextBuilder = Substitute.For<IPackageContextBuilder>();
            var instance = new PackageOutputService(packageOutputer, entityOutputer, parameterOutputer, userParameterOutputer, contextBuilder);

            await instance.OutputAsync(package, CancellationToken.None);

            contextBuilder.Received(1).Build();
            packageOutputer.DidNotReceive().Output(package, Arg.Any<PackageContext>());
            await packageOutputer.Received(1).OutputAsync(package, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            entityOutputer.DidNotReceive().Output(Arg.Any<Entity_>(), Arg.Any<PackageContext>());
            await entityOutputer.Received(1).OutputAsync(package.Entities[0], Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            parameterOutputer.DidNotReceive().Output(Arg.Any<KeyValuePair<string, string>>(), Arg.Any<PackageContext>());
            await parameterOutputer.Received(1).OutputAsync(parameters.First(), Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
            userParameterOutputer.DidNotReceive().Output(Arg.Any<UserParameter_>(), Arg.Any<PackageContext>());
            await userParameterOutputer.Received(1).OutputAsync(userParameters.First().Value, Arg.Any<PackageContext>(), Arg.Any<CancellationToken>());
        }


        private PackageOutputService Instance() => new PackageOutputService
            (
                Substitute.For<IPackageOutputer>(),
                Substitute.For<IEntityOutputer>(),
                Substitute.For<IParameterOutputer>(),
                Substitute.For<IUserParameterOutputer>(),
                Substitute.For<IPackageContextBuilder>()
            );
    }

}
