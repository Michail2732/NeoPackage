using CheckPackage.Core.Abstract;
using CheckPackage.Core.Condition;
using CheckPackage.Core.Package;
using CheckPackage.Core.Regex;
using CheckPackage.Tests.Core.Mocks;
using CheckPackage.Tests.Core.Stubs;
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
    public class RegexConditionResolverTests
    { 
        [Test]        
        public void Resolve_AnyParameterNull_ArgumentNullException()
        {
            var instance = CreateInstanceWithStubs();
            var context = CreateContext();
            var conditiion = new RegexMatchCondition();

            Assert.Catch<ArgumentNullException>(() => instance.Resolve(null, context));
            Assert.Catch<ArgumentNullException>(() => instance.Resolve(conditiion, null));
            Assert.Catch<ArgumentNullException>(() => instance.Resolve(null, null));
        }        

        [Test]
        public void Resolve_AnyParameterEmpty_false()
        {            
            for (int i = 0; i < 32; i++)
            {
                BitVector32 bitMask = new BitVector32(i);
                var instance = CreateInstanceWithStubs();
                var condition = new RegexMatchCondition
                {
                    ParameterId = bitMask[1] ? string.Empty : "any1",
                    RegexTemplate = new RegexTemplate(
                        bitMask[2] ? string.Empty : "any2",
                        bitMask[4] ? string.Empty : "any3")                    
                };
                var context = CreateContext(bitMask[8] ? new Dictionary<string, string>
                    {
                        { "any4", (bitMask[16] ? null : "any5") }
                    } : null);
                Assert.IsFalse(instance.Resolve(condition, context));
            }            
        }


        [Test]
        public void Resolve_ConditionWithParam_true()
        {
            MockRegexTemplateParcer? parcer = null;
            MockRegexContextBuilder? cntxtBldr = null;
            var instance = CreateInstanceWithMocks(out parcer, out cntxtBldr);
            parcer.SetParceResult("assert1");                        
            var context = CreateContext(new Dictionary<string, string>{ { "assert2", "assert1" }});
            var condition = new RegexMatchCondition
            {                
                ParameterId = "assert2",
                Logic = LogicalOperator.or,
                RegexTemplate = new RegexTemplate("any1", "assert1")
            };            

            bool result = instance.Resolve(condition, context);
            Assert.IsTrue(result);
        }

        [Test]
        public void Resolve_TestInnerCallsSuccess()
        {            
            MockRegexTemplateParcer? parcer = null;
            MockRegexContextBuilder? cntxtBldr = null;
            var instance = CreateInstanceWithMocks(out parcer, out cntxtBldr);
            var context = CreateContext(new Dictionary<string, string> {
                { "param_assert1", "any"}
            });                        
            var condition = new RegexMatchCondition
            {
                ParameterId = "param_assert1",
                RegexTemplate = new RegexTemplate("any3", "any4")
            };

            instance.Resolve(condition, context);

            Assert.AreEqual(parcer.CountCallParce, 1);
            Assert.AreEqual(parcer.CountCallAnalyzes, 0);
            Assert.AreEqual(cntxtBldr.CountCallBuild, 1);
        }


        [Test]
        public void Resolve_TestInnerCallsUnsuccess()
        {
            MockRegexTemplateParcer? parcer = null;
            MockRegexContextBuilder? cntxtBldr = null;
            var instance = CreateInstanceWithMocks(out parcer, out cntxtBldr);
            var context = CreateContext();
            var condition = new RegexMatchCondition
            {
                ParameterId = "any",
                RegexTemplate = new RegexTemplate("any3", "any4")
            };

            instance.Resolve(condition, context);

            Assert.AreEqual(parcer.CountCallParce, 0);
            Assert.AreEqual(parcer.CountCallAnalyzes, 0);
            Assert.AreEqual(cntxtBldr.CountCallBuild, 0);
        }



        RegexConditionResolver CreateInstanceWithStubs()
        {
            var regexParcer = new StubRegexTemplateParcer();            
            var cntxtBldr = new StubRegexContextBuilder();            
            return new RegexConditionResolver(regexParcer, cntxtBldr);
        }

        RegexConditionResolver CreateInstanceWithMocks(out MockRegexTemplateParcer parcer, out MockRegexContextBuilder cntxtBldr)
        {
            parcer = new MockRegexTemplateParcer();
            cntxtBldr = new MockRegexContextBuilder();
            return new RegexConditionResolver(parcer, cntxtBldr);
        }


        private ConditionContext CreateContext(Dictionary<string, string>? parameters = null)
        {            
            return new ConditionContext(new PackageEntity(1, "test", parameters ?? 
                new Dictionary<string, string>()), new Localizer.MessagesService(Substitute.For<IStringLocalizer<
                    Localizer.MessagesService>>()));
        }
    }

}
