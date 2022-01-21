using CheckPackage.Core.Abstract;
using CheckPackage.Core.Condition;
using CheckPackage.Core.Regex;
using CheckPackage.Localizer;
using Microsoft.Extensions.Localization;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Condition
{

    [TestFixture]
    public class ConditionResolverProviderTests
    {   
        [Test]
        public void HasCondition_Null_ArgumentNullException() 
        {
            var instance = CreateInstance();

            Assert.Catch<ArgumentNullException>(() => instance.HasCondition(null));
        }

        [Test]
        public void HasCondition_NotExistConditionInfo_ArgumentNullException()
        {
            var instance = CreateInstance();

            bool result = instance.HasCondition(Substitute.For<ConditionInfo>());

            Assert.IsFalse(result);
        }

        [Test]
        public void HasCondition_ExistConditionInfo_ArgumentNullException()
        {
            var instance = CreateInstance();
            List<ConditionInfo> conditions = new List<ConditionInfo>
            {
                new ContainsCondition(),
                new RegexMatchCondition(),
                new EqualCondition()
            };

            for (int i = 0; i < conditions.Count; i++)
            {
                bool result = instance.HasCondition(conditions[i]);

                Assert.IsTrue(result);
            }            
        }


        [Test]
        public void HasCondition_UserResolver_ConditionResolverProvider()
        {
            var instance = CreateInstance(new List<IConditionResolver>
            {
                new MockConditionResolver()
            });

            bool result = instance.HasCondition(new MockConditionInfo());

            Assert.True(result);
        }

        [Test]
        public void HasCondition_UserResolverLongDerived_ConditionResolverProvider()
        {
            var instance = CreateInstance(new List<IConditionResolver>
            {
                new MockLongDerivedConditionResolver()
            });

            bool result = instance.HasCondition(new MockConditionInfo());

            Assert.True(result);
        }

        [Test]
        public void GetCondition_null_ArgumentNullException()
        {
            var instance = CreateInstance();

            Assert.Catch<ArgumentNullException>(() => instance.GetCondition(null));            
        }


        [Test]
        public void GetCondition_NotExistCondition_ArgumentNullException()
        {
            var instance = CreateInstance();

            Assert.Catch<ArgumentException>(() => instance.GetCondition(
                new MockConditionInfo()));
        }

        [Test]
        public void GetCondition_ExistCondition_ArgumentNullException()
        {
            var instance = CreateInstance();

            var result1 = instance.GetCondition(new EqualCondition());
            var result2 = instance.GetCondition(new RegexMatchCondition());
            var result3 = instance.GetCondition(new ContainsCondition());

            Assert.AreEqual(result1.GetType(), typeof(EqualConditionResolver));
            Assert.AreEqual(result2.GetType(), typeof(RegexConditionResolver));
            Assert.AreEqual(result3.GetType(), typeof(ContainsConditionResolver));
        }



        private ConditionResolverProvider CreateInstance()
        {                        
            return new ConditionResolverProvider(new List<IConditionResolver>
            {
                new ContainsConditionResolver(),
                new EqualConditionResolver(),
                new RegexConditionResolver(Substitute.For<IRegexTemplateParcer>(),
                new RegexContextBuilder(Substitute.For<IRepositoryProvider>(), 
                new MessagesService(Substitute.For<IStringLocalizer<MessagesService>>())))
            });
        }
        private ConditionResolverProvider CreateInstance(List<IConditionResolver> reolvers)
        {
            return new ConditionResolverProvider(reolvers);
        }
    }

}
