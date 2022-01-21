using CheckPackage.Core.Checks;
using CheckPackage.Core.Validation.Extensions;
using CheckPackage.PackageValidation.Resources;
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
    public class EntityUserParameterValidator : IEntityUserParameterValidator
    {
        private readonly ICheckService _checkService;

        public EntityUserParameterValidator(ICheckService checkService)
        {
            _checkService = checkService ?? throw new ArgumentNullException(nameof(checkService));
        }        

        public EntityStateResult Validate(UserParameter userparameter, PackageContext context)
        {
            var parameterRules = context.ResourceProvider.GetStorage<ParameterCheckRuleResource, string>().Get(a => 
                a.ParameterId == userparameter.Id && a.IsUserParameter);                  
            StringBuilder sb = new StringBuilder();
            State state = State.success;
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
        

        public async Task<EntityStateResult> ValidateAsync(UserParameter userparameter, PackageContext context, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var parameterRules = await context.ResourceProvider.GetStorage<ParameterCheckRuleResource, string>().GetAsync(a =>
                a.ParameterId == userparameter.Id && a.IsUserParameter, ct); 
            StringBuilder sb = new StringBuilder();
            State state = State.success;
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
