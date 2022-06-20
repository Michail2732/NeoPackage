using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Package.Domain;
using Package.Exporting.Exceptions;
using Package.Exporting.Rules;
using Package.Infrastructure;
using Package.Utility.Collection;

namespace Package.Exporting.Services
{
    using ExportRulesList = CollectionsLocator<ExportRule<PackageItem>, ExportRule<RootPackage>>;
    
    public sealed class ExportService : IExportService
    {
        private readonly InfrastructureContextBuilder _contextBuilder;
        private readonly IReadOnlyCollection<IEndpoint>  _endPoints;

        public ExportService(
            InfrastructureContextBuilder contextBuilder,
            IReadOnlyCollection<IEndpoint> endPoints)
        {
            _contextBuilder = contextBuilder ?? throw new ArgumentNullException(nameof(contextBuilder));
            _endPoints = endPoints ?? throw new ArgumentNullException(nameof(endPoints));
        }

        public async Task<ExportReport> ExportAsync(
            RootPackage package,
            ExportRulesList rules,
            CancellationToken ct)
        {
            try
            {
                var context = _contextBuilder.Build();
                var report = new ExportReport();
                foreach (var packageItem in package.GetStackEnumerable())
                {
                    var matchedRules = rules.Items1.Where(a => a.IsMatch(packageItem, context));
                    foreach (var matchedRule in matchedRules)
                    {
                        var endPoint = _endPoints.FirstOrDefault(a => a.Id == matchedRule.EndPointId) 
                                       ?? throw new PackageExportException($"Not found export end point with id {matchedRule.EndPointId}");
                        var exportResult = await endPoint.ExportAsync(packageItem, context, ct);
                        report.Add(exportResult);
                    }
                }

                var matchedPackRules = rules.Items2.Where(a => a.IsMatch(package, context));
                foreach (var matchedPackRule in matchedPackRules)
                {
                    var endPoint = _endPoints.FirstOrDefault(a => a.Id == matchedPackRule.EndPointId) 
                                   ?? throw new PackageExportException($"Not found export end point with id {matchedPackRule.EndPointId}");
                    var exportResult = await endPoint.ExportAsync(package, context, ct);
                    report.Add(exportResult);
                }

                return report;
            }
            catch (Exception e)
            {
                throw new PackageExportException("error of export", e);
            }
        }
    }
}