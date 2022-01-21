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
    public class ContainsConditionResolverTests
    {        

        [Test]        
        public void Resolve_AnyParameterNull_ArgumentNullException() 
        {
            var instance = CreateInstance();
            var context = CreateContext();
            var condition = new ContainsCondition();

            Assert.Catch<ArgumentNullException>(() => instance.Resolve(condition, null));
            Assert.Catch<ArgumentNullException>(() => instance.Resolve(null, context));
            Assert.Catch<ArgumentNullException>(() => instance.Resolve(null, null));
        }

        [Test]
        public void Resolve_AnyParameterEmpty_false()
        {
            var instance = CreateInstance();
            var values = new List<string>()
                { "any1", "any2", "any3"};
            for (int i = 0; i < 32; i++)
            {
                BitVector32 bitMask = new BitVector32(i);
                var context = CreateContext(
                    bitMask[1] ? null : new Dictionary<string, string>{
                        {"any4", (bitMask[2] ? null : "any5") } });
                var condition = new ContainsCondition
                {
                    ParameterId = bitMask[4] ? string.Empty : "any6",
                    Values = bitMask[8] ? new List<string>() : values,
                    Logic = bitMask[16] ? LogicalOperator.or : LogicalOperator.and
                };

                bool result = instance.Resolve(condition, context);

                Assert.IsFalse(result);
            }                        
        }

        [Test]
        public void Resolve_ValuesEmpty_false()
        {
            var instance = CreateInstance();
            var context = CreateContext(new Dictionary<string, string> {
                { "param_assert1", "any1"} 
            });
            var condition = new ContainsCondition
            {
                ParameterId = "param_assert1",
                Values = new List<string>()
            };

            bool result = instance.Resolve(condition, context);

            Assert.IsFalse(result);
        }

        [Test]
        public void Resolve_ValuesNotContains_false()
        {
            var instance = CreateInstance();
            var context = CreateContext(new Dictionary<string, string> {
                { "param_assert1", "any1" } 
            });
            var condition = new ContainsCondition
            {
                ParameterId = "param_assert1",
                Values = new List<string>()
                    { "any2", "any3", "any4"}
            };

            bool result = instance.Resolve(condition, context);

            Assert.IsFalse(result);
        }

        [Test]
        public void Resolve_ValuesContains_false()
        {
            var instance = CreateInstance();
            var context = CreateContext(new Dictionary<string, string> {
                { "param_assert1", "value_assert1" } 
            });
            var condition = new ContainsCondition
            {
                ParameterId = "param_assert1",
                Values = new List<string>()
                    { "any1", "any2", "value_assert1"}
            };

            bool result = instance.Resolve(condition, context);

            Assert.IsTrue(result);
        }

        private ConditionContext CreateContext(Dictionary<string, string> parameters = null)
        {
            return new ConditionContext(new PackageEntity(1, "test", parameters ??
                new Dictionary<string, string>()), new Localizer.MessagesService(
                    Substitute.For<IStringLocalizer<Localizer.MessagesService>>()));
        }

        ContainsConditionResolver CreateInstance() => new ContainsConditionResolver();
    }

}
