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
    public class EqualConditionResolverTests
    {      
        [Test]        
        public void Resolve_AnyParamNull_ArgumentNullException() 
        {
            var instance = CreateInstance();
            var context = CreateContext();
            var condition = new EqualCondition();

            Assert.Catch<ArgumentNullException>(() => instance.Resolve(condition, null));
            Assert.Catch<ArgumentNullException>(() => instance.Resolve(null, context));
            Assert.Catch<ArgumentNullException>(() => instance.Resolve(null, null));
        }        

        [Test]
        public void Resolve_AnyParamEmpty_false()
        {
            var instance = CreateInstance();
            for (int i = 0; i < 16; i++)
            {
                BitVector32 bitMask = new BitVector32(i);                
                var context = CreateContext(
                    bitMask[1] ? new Dictionary<string, string> {
                        { "any1", "any2" } 
                    }: null);
                var condition = new EqualCondition
                {
                    ParameterId = bitMask[2] ? string.Empty : "any3",
                    Value = bitMask[4] ? string.Empty : "any4",
                    Logic = bitMask[8] ? LogicalOperator.or : LogicalOperator.and
                };                    

                var result = instance.Resolve(condition, context);

                Assert.IsFalse(result);
            }                        
        }

        [Test]
        public void Resolve_WithoutEntityParameter_false()
        {
            var instance = CreateInstance();
            var context = CreateContext();
            var condition = new EqualCondition
            {
                ParameterId = "any1",
                Value = "any2"
            };

            bool result = instance.Resolve(condition, context);

            Assert.IsFalse(result);
        }

        [Test]
        public void Resolve_WithEntityParameter_true()
        {
            var instance = CreateInstance();
            var context = CreateContext(new Dictionary<string, string> {
                { "param_assert1", "value_assert1" } 
            });
            var condition = new EqualCondition
            {
                ParameterId = "param_assert1",                
                Value = "value_assert1"
            };            

            bool result = instance.Resolve(condition, context);

            Assert.IsTrue(result);
        }

        [Test]
        public void Resolve_WithEntityNotEqualParameter_false()
        {
            var instance = CreateInstance();
            var context = CreateContext(new Dictionary<string, string> {
                { "param_assert1", "value_assert1" } 
            });
            var condition = new EqualCondition
            {
                ParameterId = "param_assert1",
                Value = "any2"
            };

            bool result = instance.Resolve(condition, context);

            Assert.IsFalse(result);
        }
        

        EqualConditionResolver CreateInstance() => new EqualConditionResolver();        

        private ConditionContext CreateContext(Dictionary<string, string>? parameters = null)
        {
            return new ConditionContext(new PackageEntity(1, "test", parameters ??
                new Dictionary<string, string>()), new Localizer.MessagesService
                (Substitute.For<IStringLocalizer<Localizer.MessagesService>>()));
        }
    }

}
