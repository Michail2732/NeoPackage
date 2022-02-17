using CheckPackage.Core.Checks;
using CheckPackage.PackageValidation.Rules;
using Package.Abstraction.Entities;
using Package.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheckPackage.PackageValidation.Validation
{
    public class UserParameterValidator : IUserParameterValidator
    {
        private readonly ICheckService _checkService;

        public UserParameterValidator(ICheckService checkService)
        {
            _checkService = checkService ?? throw new ArgumentNullException(nameof(checkService));
        }        

        public EntityStateResult Validate(UserParameter_ userparameter, PackageContext context)
        {
            var parameterRules = context.RepositoryProvider.GetRepository<ParameterCheckRule, string>().Get(a => 
                a.ParameterId == userparameter.Id && a.IsUserParameter);                  
            StringBuilder sb = new StringBuilder();
            Critical state = Critical.notcritical;
            foreach (var parameterRule in parameterRules)
            {
                var result = _checkService.Check(userparameter, parameterRule.Checks);
                if (!result.IsSuccess)
                {
                    if (string.IsNullOrEmpty(result.Details)) sb.Append($"{result.Details}\n");
                    state = parameterRule.State > state ? parameterRule.State : state;
                }
            }
            return new EntityStateResult(userparameter.Id, sb.ToString(), state);
        }
        

        public async Task<EntityStateResult> ValidateAsync(UserParameter_ userparameter, PackageContext context, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var parameterRules = await context.RepositoryProvider.GetRepository<ParameterCheckRule, string>().GetAsync(a =>
                a.ParameterId == userparameter.Id && a.IsUserParameter, ct); 
            StringBuilder sb = new StringBuilder();
            Critical state = Critical.notcritical;
            foreach (var parameterRule in parameterRules)
            {
                ct.ThrowIfCancellationRequested();
                var result = _checkService.Check(userparameter, parameterRule.Checks);
                if (!result.IsSuccess)
                {
                    if (string.IsNullOrEmpty(result.Details)) sb.Append($"{result.Details}\n");
                    state = parameterRule.State > state ? parameterRule.State : state;
                }
            }
            return new EntityStateResult(userparameter.Id, sb.ToString(), state);
        }
    }
}
