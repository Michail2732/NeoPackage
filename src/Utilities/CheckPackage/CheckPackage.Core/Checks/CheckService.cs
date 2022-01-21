using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using Package.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.Core.Checks
{
    public class CheckService : ICheckService
    {
        private readonly IPackageContextBuilder _contextBuilder;

        public CheckService(IPackageContextBuilder contextBuilder)
        {
            _contextBuilder = contextBuilder ?? throw new ArgumentNullException(nameof(contextBuilder));
        }

        public Result Check(PackageEntity entity, PackageEntityCheckCommand check)
            => CheckPrivate(entity, check);

        public Result Check(Package_ package, PackageCheckCommand check)
            => CheckPrivate(package, check);

        public Result Check(KeyValuePair<string, string> parameter, ParameterCheckCommand check)
            => CheckPrivate(new Parameter(parameter.Key, parameter.Value), check);

        public Result Check(UserParameter parameter, ParameterCheckCommand check)
            => CheckPrivate(new Parameter(parameter.Id, parameter.Value), check);

        public Result Check(PackageEntity entity, IReadOnlyList<PackageEntityCheckCommand> checks)
            => CheckPrivate( entity, checks);

        public Result Check(Package_ package, IReadOnlyList<PackageCheckCommand> checks)
            => CheckPrivate( package, checks);

        public Result Check(KeyValuePair<string, string> parameter, IReadOnlyList<ParameterCheckCommand> checks) 
            => CheckPrivate( new Parameter(parameter.Key, parameter.Value), checks);

        public Result Check(UserParameter parameter, IReadOnlyList<ParameterCheckCommand> checks) 
            => CheckPrivate(new Parameter(parameter.Id, parameter.Value), checks);


        private Result CheckPrivate<T>(T obj, ICheckCommand<T> check)
        {
            var context = _contextBuilder.Build();
            StringBuilder errorSb = new StringBuilder();
            Result localResult = check.Check(obj, context);
            if (!localResult.IsSuccess)
                errorSb.Append((string.IsNullOrEmpty(localResult.Details) ? check.Message : localResult.Details) + "\n");
            return new Result(localResult.IsSuccess, errorSb.ToString());
        }

        private Result CheckPrivate<T>(T obj, IReadOnlyList<ICheckCommand<T>> checks)
        {
            var context = _contextBuilder.Build();
            StringBuilder errorSb = new StringBuilder();
            if (checks.Count > 0) checks.First().Logic = LogicalOperator.or;
            bool executedResult = BooleanSolver.Solve(checks.Select(a => new KeyValuePair<LogicalOperator, Func<bool>>
            (
                a.Logic,
                () =>
                {
                    Result localResult = a.Check(obj, context);
                    if (!localResult.IsSuccess)
                        errorSb.Append((string.IsNullOrEmpty(localResult.Details) ? a.Message : localResult.Details) + "\n");
                    return localResult.IsSuccess;
                }
                )).ToList());

            return new Result(executedResult, errorSb.ToString());
        }
       
    }
}
