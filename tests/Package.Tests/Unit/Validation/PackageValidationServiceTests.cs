using NUnit.Framework;
using NSubstitute;
using Package.Validation.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Package.Validation.Validators;
using Package.Abstraction.Services;

namespace Package.Tests.Unit.Validation
{

    [TestFixture]
    public class PackageValidationServiceTests
    {        
        [Test]        
        public void Validate_EmptyPackage_PackageReport() 
        {

        }

        private PackageValidationService Instance() =>
            new PackageValidationService(
                Substitute.For<IPackageValidator>(),
                Substitute.For<IEntityValidator>(),
                Substitute.For<IParameterValidator>(),
                Substitute.For<IUserParameterValidator>(),
                Substitute.For<IPackageContextBuilder>());
    }

}
