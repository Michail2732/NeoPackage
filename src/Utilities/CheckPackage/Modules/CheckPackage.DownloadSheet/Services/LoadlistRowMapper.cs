using CheckPackage.Core.Conditions;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Selectors;
using CheckPackage.DownloadSheet.Entities;
using Package.Abstraction.Entities;
using Package.Localization;
using System;
using System.Linq;

namespace CheckPackage.DownloadSheet.Service
{
    public class LoadlistRowMapper : ILoadlistRowMapper
    {
        private readonly ISelectorService _selectorService;
        private readonly IExtractService _extracterService;
        private readonly IConditionService _conditionService;

        public LoadlistRowMapper(ISelectorService selectorService, IExtractService extracterService, 
            IConditionService conditionService)
        {
            _selectorService = selectorService ?? throw new ArgumentNullException(nameof(selectorService));
            _extracterService = extracterService ?? throw new ArgumentNullException(nameof(extracterService));
            _conditionService = conditionService ?? throw new ArgumentNullException(nameof(conditionService));
        }

        public EntityToRowMapResult MapToRow(Entity_ entity, Loadlist loadlist, PackageContext context)
        {
            var rowMaps = context.RepositoryProvider.GetRepository<RowMappingRule, string>().Get().OrderBy(a => a.Priority);
            var colMaps = context.RepositoryProvider.GetRepository<ColumnMappingRule, string>().Get();                                    
            foreach (var rowMap in rowMaps)
                if (rowMap.EntityConditions == null || _conditionService.Resolve(entity, rowMap.EntityConditions))
                {
                    var row = loadlist.AddRow();
                    row.IsVirtual = rowMap.IsVirtual;
                    foreach (var colMap in colMaps.Where(a => rowMap.ColumnIds?.Contains(a.Id) == true))                        
                    {
                        var parameters = _selectorService.Select(new[] { entity }, colMap.Selector);
                        var resultParameters = _extracterService.Extract(parameters, colMap.Extracter);                                                                                    
                        row[colMap.Name] = string.Join(" ", resultParameters.Select(a => a.Value.ToString()));
                    }
                    return EntityToRowMapResult.Success(row);
                }
            return EntityToRowMapResult.Error(context.Messages[MessageKeys.EntityNotMatchAnyLoadlistRowBuildRule, entity.Name]);
        }

  
    }
}
