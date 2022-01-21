using Package.Abstraction.Entities;
using Package.Abstraction.Services;
using Package.Validation.Exceptions;
using Package.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Validation.Services
{
    public class PackageValidationService : IPackageValidationService
    {
        private readonly IPackageValidator _packageValidator;
        private readonly IPackageEntityValidator _packageEntityValidator;
        private readonly IEntityParameterValidator _parameterValidator;
        private readonly IEntityUserParameterValidator _userParameterValidator;
        private readonly IPackageContextBuilder _contextBuilder;

        public PackageValidationService(IPackageValidator packageValidator, 
            IPackageEntityValidator packageEntityValidator, IEntityParameterValidator parameterValidator, 
            IEntityUserParameterValidator userParameterValidator, IPackageContextBuilder contextBuilder)
        {
            _packageValidator = packageValidator ?? throw new ArgumentNullException(nameof(packageValidator));
            _packageEntityValidator = packageEntityValidator ?? throw new ArgumentNullException(nameof(packageEntityValidator));
            _parameterValidator = parameterValidator ?? throw new ArgumentNullException(nameof(parameterValidator));
            _userParameterValidator = userParameterValidator ?? throw new ArgumentNullException(nameof(userParameterValidator));
            _contextBuilder = contextBuilder ?? throw new ArgumentNullException(nameof(contextBuilder));
        }

        public event EventHandler<EntityValidationEventArgs>? EntityValidated;

        public PackageReport Validate(Package_ package)
        {
            try
            {
                PackageContext context = _contextBuilder.Build();
                PackageEntityStackEnumerable entityEnumerable = new PackageEntityStackEnumerable(package.Entities);
                List<PackageEntityReport> entitiesReports = new List<PackageEntityReport>();
                foreach (var entity in entityEnumerable)                
                {                    
                    var entityResult = _packageEntityValidator.Validate(entity, context);
                    List<EntityStateResult> parameterResults = new List<EntityStateResult>(10),
                        userParameterResults = new List<EntityStateResult>(3);
                    foreach (var parameter in entity.Parameters)
                    {
                        var parameterResult = _parameterValidator.Validate(parameter, context);
                        parameterResults.Add(parameterResult);
                    }
                    foreach (var userParameter in entity.UserParameters)
                    {
                        var userParameterResult = _userParameterValidator.Validate(userParameter.Value, context);
                        userParameterResults.Add(userParameterResult);
                    }
                    entitiesReports.Add(new PackageEntityReport(parameterResults, userParameterResults, entityResult));
                    if (OnEntityValidated(entitiesReports.Last())) break;                    
                }
                var packageResult = _packageValidator.Validate(package, context);
                return new PackageReport(entitiesReports, packageResult);
            }
            catch (Exception ex) { throw new PackageValidateException($"Error occurred validating package", ex); }            
        }

        public async Task<PackageReport> ValidateAsync(Package_ package, CancellationToken ct)
        {
            try
            {
                PackageContext context = _contextBuilder.Build();
                PackageEntityStackEnumerable entityEnumerable = new PackageEntityStackEnumerable(package.Entities);
                List<PackageEntityReport> entitiesReports = new List<PackageEntityReport>();
                foreach (var entity in entityEnumerable)
                {
                    ct.ThrowIfCancellationRequested();                                   
                    var entityResult = await _packageEntityValidator.ValidateAsync(entity, context, ct);
                    List<EntityStateResult> parameterResults = new List<EntityStateResult>(10),
                        userParameterResults = new List<EntityStateResult>(3);
                    foreach (var parameter in entity.Parameters)
                    {
                        var parameterResult = await _parameterValidator.ValidateAsync(parameter, context, ct);
                        parameterResults.Add(parameterResult);
                    }
                    foreach (var userParameter in entity.UserParameters)
                    {
                        var userParameterResult = await _userParameterValidator.ValidateAsync(userParameter.Value, context, ct);
                        userParameterResults.Add(userParameterResult);
                    }
                    entitiesReports.Add(new PackageEntityReport(parameterResults, userParameterResults, entityResult));
                    if (OnEntityValidated(entitiesReports.Last())) break;                    
                }
                var packageResult = await _packageValidator.ValidateAsync(package, context, ct);
                return new PackageReport(entitiesReports, packageResult);
            }
            catch (Exception ex) { throw new PackageValidateException($"Error occurred validating package", ex); }
        }

        private bool OnEntityValidated(PackageEntityReport entityReport)
        {
            var evnt = EntityValidated;
            if (evnt != null)
            {
                var args = new EntityValidationEventArgs(entityReport);
                evnt.Invoke(this, args);
                return args.Cancel;
            }
            return false;
        }
    }
}
