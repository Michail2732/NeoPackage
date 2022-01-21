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
    public class EntityParameterValidator : IEntityParameterValidator
    {
        private readonly ICheckService _checkService;

        public EntityParameterValidator(ICheckService checkService)
        {
            _checkService = checkService ?? throw new ArgumentNullException(nameof(checkService));
        }
        

        public EntityStateResult Validate(KeyValuePair<string, string> parameter, PackageContext context)
        {            
            var parameterRules = context.ResourceProvider.GetStorage<ParameterCheckRuleResource, string>().
                Get(a => a.ParameterId == parameter.Key);
            StringBuilder sb = new StringBuilder();
            State state = State.success;
            foreach (var parameterRule in parameterRules)
            {                
                var result = _checkService.Check(parameter, parameterRule.Checks);
                if (!result.IsSuccess)
                {
                    if (string.IsNullOrEmpty(result.Details)) sb.Append($"{result.Details}\n");
                    state = parameterRule.State > state ? parameterRule.State : state;
                }
            }
            return new EntityStateResult(parameter.Key, sb.ToString(), state);
        }

        public async Task<EntityStateResult> ValidateAsync(KeyValuePair<string, string> parameter, PackageContext context, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var parameterRules = await context.ResourceProvider.GetStorage<ParameterCheckRuleResource, string>().
                GetAsync(a => a.ParameterId == parameter.Key, ct);            
            StringBuilder sb = new StringBuilder();
            State state = State.success;
            foreach (var parameterRule in parameterRules)
            {
                ct.ThrowIfCancellationRequested();                
                var result = _checkService.Check(parameter, parameterRule.Checks);
                if (!result.IsSuccess)
                {
                    if (string.IsNullOrEmpty(result.Details)) sb.Append($"{result.Details}\n");
                    state = parameterRule.State > state ? parameterRule.State : state;
                }
            }
            return new EntityStateResult(parameter.Key, sb.ToString(), state);
        }
        
    }
}
