using CheckPackage.Core.Abstract;
using CheckPackage.Core.Condition;
using CheckPackage.Core.Package;
using CheckPackage.Core.Regex;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CheckPackage.Tests.Core.Condition
{

    [TestFixture]
    public class ContainsConditionInfoTests
    {

        [Test]
        public void Validate_null_ArgumentNullException()
        {
            var instance = CreateInstance();

            Assert.Catch<ArgumentException>(() => instance.Validate(null));
        }


        [Test]
        public void Validate_RequiredPropertyEmpty_false()
        {
            var context = CreateContext();
            for (int i = 0; i < 64; i = (((i + 1) & 3) == 3 ? i + 2 : i + 1))
            {
                var instance = CreateInstance();
                instance.ParameterId = (i & 1) == 1 ? "any1" : string.Empty;
                instance.Values = (i & 2) == 2 ? new List<string>() : null;
                instance.Recurse = (i & 4) == 4 ? false : true;
                instance.Inverse = (i & 8) == 8 ? false : true;
                instance.Logic = (i & 16) == 16 ? LogicalOperator.and : LogicalOperator.or;
                if ((i & 32) == 32)
                    instance.SetNext(CreateInstance());

                var result = instance.Validate(context);

                Assert.IsFalse(result.IsSuccess, $"iteration {i}");
            }
        }

        [Test]
        public void Validate_RequiredPropertyFullWithoutEntityParam_false()
        {
            var context = CreateContext();
            for (int i = 0; i < 16; i++)
            {
                var instance = CreateInstance();
                instance.ParameterId = "any1";
                instance.Values = new List<string>();                
                instance.Recurse = (i & 1) == 1 ? false : true;
                instance.Inverse = (i & 2) == 2 ? false : true;
                instance.Logic = (i & 4) == 4 ? LogicalOperator.and : LogicalOperator.or;
                if ((i & 8) == 8)
                    instance.SetNext(CreateInstance());

                var result = instance.Validate(context);

                Assert.IsFalse(result.IsSuccess, $"iteration {i}");
            }
        }


        [Test]
        public void Validate_RequiredPropertyFullWithEntityParam_true()
        {
            var context = CreateContext(new Dictionary<string, string> {
                {"asert1", "any1"}
            });
            for (int i = 0; i < 16; i++)
            {
                var instance = CreateInstance();
                instance.ParameterId = "asert1";
                instance.Values = new List<string>();
                instance.Recurse = (i & 1) == 1 ? false : true;
                instance.Inverse = (i & 2) == 2 ? false : true;
                instance.Logic = (i & 4) == 4 ? LogicalOperator.and : LogicalOperator.or;
                if ((i & 8) == 8)
                    instance.SetNext(CreateInstance());

                var result = instance.Validate(context);

                Assert.IsTrue(result.IsSuccess, $"iteration {i}");
            }

        }


        [Test]
        public void SetNext_null_ArgumentNullException()
        {
            var instance = CreateInstance();

            Assert.Catch<ArgumentNullException>(() => instance.SetNext(null));
        }


        [Test]
        public void SetNext_CicleLink_ArgumentException()
        {
            var instance = CreateInstance();
            var next = CreateInstance();

            instance.SetNext(next);
            var ex = Assert.Catch<ArgumentException>(() => next.SetNext(instance));

            Assert.AreEqual(ex?.Message, "Cicle link");
        }

        [Test]
        public void SetNext_CicleLinkDuplicate_ArgumentException()
        {
            var instance = CreateInstance();
            var next = CreateInstance();

            var ex = Assert.Catch<ArgumentException>(() => next.SetNext(next));

            Assert.AreEqual(ex?.Message, "Cicle link");
        }

        private ContainsCondition CreateInstance(string? paramId = null,
            List<string>? values = null)
        {
            return new ContainsCondition
            {
                ParameterId = paramId,
                Values = values                
            };
        }

        private ConditionContext CreateContext(Dictionary<string, string>? param = null)
        {
            return new ConditionContext(new PackageEntity(0, "test", param ??
                new Dictionary<string, string>()), new Localizer.MessagesService(
                    Substitute.For<IStringLocalizer<Localizer.MessagesService>>()));
        }
    }

}
