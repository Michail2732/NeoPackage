using CheckPackage.Core.Abstract;
using CheckPackage.Core.Condition;
using CheckPackage.Core.Package;
using CheckPackage.Core.Regex;
using CheckPackage.Loadlist.Common;
using CheckPackage.Loadlist.Conditions;
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
    public class ColumnConditionInfoTests
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
            for (int i = 0; i < 128; i = (((i + 1) & 3) == 3 ? i + 2 : i + 1))
            {
                var instance = CreateInstance();
                instance.ParameterId = (i & 1) == 1 ? "any1" : string.Empty;
                instance.Column = (i & 2) == 2 ? "any2" : string.Empty;
                instance.Recurse = (i & 4) == 4 ? false : true;
                instance.Inverse = (i & 8) == 8 ? false : true;
                instance.Value = (i & 64) == 64 ? "any3" : string.Empty;
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
            for (int i = 0; i < 32; i++)
            {
                var instance = CreateInstance();
                instance.ParameterId = "any1";
                instance.Column = "any2";
                instance.Value = (i & 16) == 16 ? "any3" : string.Empty; ;
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
        public void Validate_RequiredPropertyFullWithEntityParamIncorrect_false()
        {
            var context = CreateContext(new List<CustomParameter> {
                new CustomParameter("asert1", AppDomain.CurrentDomain)
            });
            for (int i = 0; i < 32; i++)
            {
                var instance = CreateInstance();
                instance.ParameterId = "asert1";
                instance.Column = "any2";
                instance.Value = (i & 16) == 16 ? "any3" : string.Empty; 
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
        public void Validate_RequiredPropertyFullWithEntityParamMissColumn_false()
        {
            var context = CreateContext(new List<CustomParameter> { 
                new CustomParameter("asert1", CreateRow())
            });
            for (int i = 0; i < 32; i++)
            {
                var instance = CreateInstance();
                instance.ParameterId = "asert1";
                instance.Column = "any2";
                instance.Value = (i & 16) == 16 ? "any3" : string.Empty;
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
            var context = CreateContext(new List<CustomParameter> {
                new CustomParameter("asert1", CreateRow(new Dictionary<string, string>
                    { {"assert2", "any1" } }))
            });
            for (int i = 0; i < 32; i++)
            {
                var instance = CreateInstance();
                instance.ParameterId = "asert1";
                instance.Column = "assert2";
                instance.Value = (i & 16) == 16 ? "any2" : string.Empty;
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

        private ColumnCondition CreateInstance(string? paramId = null,
            string? column = null)
        {
            return new ColumnCondition
            {
                ParameterId = paramId,
                Column = column
            };
        }

        private LoadlistRow CreateRow(Dictionary<string, string>? columnValues = null)
        {
            columnValues = columnValues ?? new Dictionary<string, string>();
            var loadlist = new Loadlist.Common.Loadlist();
            foreach (var columnValue in columnValues)            
                loadlist.AddColumn(columnValue.Key);
            var row = loadlist.AddRow();
            foreach (var columnValue in columnValues)
                row[columnValue.Key] = columnValue.Value;
            return row;
        }

        private ConditionContext CreateContext(List<CustomParameter> customs = null,
            Dictionary<string, string>? param = null)
        {
            return new ConditionContext(
                new PackageEntity(0, "test", new List<PackageEntity>(),
                    new ParametersSet(
                        customs ?? new List<CustomParameter>(),
                        param ??new Dictionary<string, string>(), 
                        null)),
                new Localizer.MessagesService(
                    Substitute.For<IStringLocalizer<Localizer.MessagesService>>()));
        }
    }

}
