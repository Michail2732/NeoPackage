using CheckPackage.Core.Abstract;
using CheckPackage.Core.Condition;
using CheckPackage.Core.Package;
using CheckPackage.Core.Regex;
using Microsoft.Extensions.Localization;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Condition
{

    [TestFixture]
    public class RegexConditionTests
    {        

        [Test]     
        public void Validate_null_ArgumentNullException()
        {
            var instance = CreateInstance();

            Assert.Catch<ArgumentNullException>(() => instance.Validate(null));
        }

        [Test]
        public void Validate_AllPropertyEmpty_false()
        {
            var instance = CreateInstance("", "", "");
            var context = CreateContext();

            Result result = instance.Validate(context);

            Assert.IsFalse(result.IsSuccess);
        }        

        [Test]
        public void Validate_RequiredPropertyMiss_false()
        {
            var context = CreateContext();
            for (int i = 0; i < 64; i = (((i+1) & 3) == 3 ? i + 2 : i + 1))
            {
                var instance = CreateInstance();
                instance.ParameterId = (i & 1) == 1 ? "any1" : string.Empty;
                instance.RegexTemplate = (i & 2) == 2 ? new RegexTemplate("any2", "any3") : null;
                instance.Recurse = (i & 4) == 4 ? false : true;
                instance.Inverse = (i & 8) == 8 ? false : true;
                instance.Logic = (i & 16) == 16 ? LogicalOperator.and : LogicalOperator.or;
                if ((i & 32) == 32)
                    instance.SetNext(CreateInstance());                

                Result result = instance.Validate(context);

                Assert.IsFalse(result.IsSuccess);
            }
        }

        [Test]
        public void Validate_RequiredPropertyInputWithoutEntityParam_false()
        {
            var context = CreateContext();
            for (int i = 0; i < 16; i++)
            {
                var instance = CreateInstance();
                instance.ParameterId = "any1";
                instance.RegexTemplate = new RegexTemplate("any2", "any3");
                instance.Recurse = (i & 1) == 1 ? false : true;
                instance.Inverse = (i & 2) == 2 ? false : true;
                instance.Logic = (i & 4) == 4 ? LogicalOperator.and : LogicalOperator.or;
                if ((i & 8) == 8)
                    instance.SetNext(CreateInstance());                

                Result result = instance.Validate(context);

                Assert.IsFalse(result.IsSuccess);
            }
        }

        [Test]
        public void Validate_RequiredPropertyInputWithEntityParam_true()
        {
            var context = CreateContext(new Dictionary<string, string> {
                    { "assert1", "some_val"}
                });
            for (int i = 0; i < 16; i++)
            {
                var instance = CreateInstance();
                instance.ParameterId = "assert1";
                instance.RegexTemplate = new RegexTemplate("any2", "any3");
                instance.Recurse = (i & 1) == 1 ? false : true;
                instance.Inverse = (i & 2) == 2 ? false : true;
                instance.Logic = (i & 4) == 4 ? LogicalOperator.and : LogicalOperator.or;
                if ((i & 8) == 8)
                    instance.SetNext(CreateInstance());                

                Result result = instance.Validate(context);

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
        public void SetNext_RegexMatchCondition_void()
        {
            var instance = CreateInstance();
            var next = CreateInstance();

            instance.SetNext(next);

            Assert.AreEqual(instance.Next, next);
            Assert.IsNull(instance.Next.Next);
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


        private RegexMatchCondition CreateInstance(string? paramId = null,
            string? regexId = null, string? regex = null)
        {
            return new RegexMatchCondition
            {
                ParameterId = paramId,
                RegexTemplate = new RegexTemplate(regexId ?? "any1", regex ?? "any2")
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
