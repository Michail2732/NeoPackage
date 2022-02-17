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

        public Result Check(Entity_ entity, EntityCheckCommand command)
            => CheckPrivate(entity, command);

        public Result Check(Package_ package, PackageCheckCommand command)
            => CheckPrivate(package, command);

        public Result Check(KeyValuePair<string, string> parameter, ParameterCheckCommand command)
            => CheckPrivate(new Parameter(parameter.Key, parameter.Value), command);

        public Result Check(UserParameter_ parameter, ParameterCheckCommand command)
            => CheckPrivate(new Parameter(parameter.Id, parameter.Value), command);

        public Result Check(Entity_ entity, IReadOnlyList<EntityCheckCommand> commands)
            => CheckPrivate( entity, commands);

        public Result Check(Package_ package, IReadOnlyList<PackageCheckCommand> commands)
            => CheckPrivate( package, commands);

        public Result Check(KeyValuePair<string, string> parameter, IReadOnlyList<ParameterCheckCommand> commands) 
            => CheckPrivate( new Parameter(parameter.Key, parameter.Value), commands);

        public Result Check(UserParameter_ parameter, IReadOnlyList<ParameterCheckCommand> commands) 
            => CheckPrivate(new Parameter(parameter.Id, parameter.Value), commands);


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
            if (checks.Count > 0) checks.First().Logic = Logical.or;
            bool executedResult = BooleanSolver.Solve(checks.Select(a => new KeyValuePair<Logical, Func<bool>>
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
