using CheckPackage.Core.Abstract;
using CheckPackage.Core.Condition;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Tests.Core.Condition
{
    public class MockConditionResolver : ConditionResolver<MockConditionInfo>
    {
        private bool _result = true;

        public MockConditionResolver SetReturnedResult(bool result)
        {
            _result = result;
            return this;
        }

        protected override bool ResolveProtected(MockConditionInfo condition, ConditionContext context)
        {
            return _result;
        }
    }

    public class MockConditionInfo : ConditionInfo
    {
        private Result _result = Result.Success();

        public MockConditionInfo SetReturnedResult(Result result)
        {
            _result = result;
            return this;
        }

        public override Result Validate(ConditionContext contexet)
        {
            return _result;
        }
    }
}
