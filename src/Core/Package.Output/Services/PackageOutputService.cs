using Package.Abstraction.Entities;
using Package.Abstraction.Services;
using Package.Output.Exceptions;
using Package.Output.Outputers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Package.Output.Services
{
    public class PackageOutputService : IPackageOutputService
    {

        private readonly IPackageOutputer _packageOutputer;
        private readonly IEntityOutputer _packageEntityOutputer;
        private readonly IParameterOutputer _parameterOutputer;
        private readonly IUserParameterOutputer _userParameterOutputer;
        private readonly IPackageContextBuilder _contextBuilder;

        public event EventHandler<EntityOutputEventArgs>? EntityOutputted;

        public PackageOutputService(IPackageOutputer packageOutputer, IEntityOutputer packageEntityOutputer, 
            IParameterOutputer parameterOutputer, IUserParameterOutputer userParameterOutputer, 
            IPackageContextBuilder contextBuilder)
        {
            _packageOutputer = packageOutputer ?? throw new ArgumentNullException(nameof(packageOutputer));
            _packageEntityOutputer = packageEntityOutputer ?? throw new ArgumentNullException(nameof(packageEntityOutputer));
            _parameterOutputer = parameterOutputer ?? throw new ArgumentNullException(nameof(parameterOutputer));
            _userParameterOutputer = userParameterOutputer ?? throw new ArgumentNullException(nameof(userParameterOutputer));
            _contextBuilder = contextBuilder ?? throw new ArgumentNullException(nameof(contextBuilder));
        }

        public PackageReport Output(Package_ package)
        {
            try
            {
                PackageContext context = _contextBuilder.Build();
                EntityStackEnumerable entityEnumerable = new EntityStackEnumerable(package.Entities);
                List<EntityReport> entitiesReports = new List<EntityReport>();
                foreach (var entity in entityEnumerable)                
                {                    
                    var entityResult = _packageEntityOutputer.Output(entity, context);
                    List<EntityStateResult> parameterResults = new List<EntityStateResult>(10),
                        userParameterResults = new List<EntityStateResult>(3);
                    foreach (var parameter in entity.Parameters)
                    {
                        var parameterResult = _parameterOutputer.Output(parameter, context);
                        parameterResults.Add(parameterResult);
                    }
                    foreach (var userParameter in entity.UserParameters)
                    {
                        var userParameterResult = _userParameterOutputer.Output(userParameter.Value, context);
                        userParameterResults.Add(userParameterResult);
                    }
                    entitiesReports.Add(new EntityReport(parameterResults, userParameterResults, entityResult));
                    bool isCancel = OnEntityOutputed(entitiesReports.Last());
                    if (isCancel) break;                    
                }
                var packageResult = _packageOutputer.Output(package, context);
                return new PackageReport(entitiesReports, packageResult);
            }
            catch (Exception ex) { throw new PackageOutputException($"Error occurred outputting package", ex); }
        }

        public async Task<PackageReport> OutputAsync(Package_ package, CancellationToken ct)
        {
            try
            {
                PackageContext context = _contextBuilder.Build();
                EntityStackEnumerable entityEnumerable = new EntityStackEnumerable(package.Entities);
                List<EntityReport> entitiesReports = new List<EntityReport>();
                foreach (var entity in entityEnumerable)
                {
                    ct.ThrowIfCancellationRequested();                    
                    var entityResult = await _packageEntityOutputer.OutputAsync(entity, context, ct);
                    List<EntityStateResult> parameterResults = new List<EntityStateResult>(10),
                        userParameterResults = new List<EntityStateResult>(3);
                    foreach (var parameter in entity.Parameters)
                    {
                        var parameterResult = await _parameterOutputer.OutputAsync(parameter, context, ct);
                        parameterResults.Add(parameterResult);
                    }
                    foreach (var userParameter in entity.UserParameters)
                    {
                        var userParameterResult = await _userParameterOutputer.OutputAsync(userParameter.Value, context, ct);
                        userParameterResults.Add(userParameterResult);
                    }
                    entitiesReports.Add(new EntityReport(parameterResults, userParameterResults, entityResult));
                    bool isCancel = OnEntityOutputed(entitiesReports.Last());
                    if (isCancel) break;                    
                }
                var packageResult = await _packageOutputer.OutputAsync(package, context, ct);
                return new PackageReport(entitiesReports, packageResult);
            }
            catch (Exception ex) { throw new PackageOutputException($"Error occurred outputting package", ex); }
        }

        private bool OnEntityOutputed(EntityReport entityReport)
        {
            var evnt = EntityOutputted;
            if (evnt != null)
            {
                var args = new EntityOutputEventArgs(entityReport);
                evnt.Invoke(this, args);
                return args.Cancel;
            }
            return false;
        }
    }
}
