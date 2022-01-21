using CheckPackage.Core.Condition;
using CheckPackage.Core.Extensions;
using CheckPackage.Core.Extracts;
using CheckPackage.DownloadSheet.Entities;
using Microsoft.Extensions.Configuration;
using Package.Abstraction.Entities;
using Package.Building.Builders;
using Package.Building.Context;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Mapping
{
    public class LoadlistRowMapper : ILoadlistRowMapper
    {
        private readonly IConditionChainResolver _resolver;
        private readonly IExtractChainExtracter _extracter;
        private readonly IResourceStoragesProvider _resources;
        private readonly MessagesService _messages;
        private readonly IConfiguration _configuration;

        public LoadlistRowMapper(IConditionChainResolver resolver, IExtractChainExtracter extracter,
            IResourceStoragesProvider resource, MessagesService messages, IConfiguration configuration)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            _extracter = extracter ?? throw new ArgumentNullException(nameof(extracter));
            _resources = resource ?? throw new ArgumentNullException(nameof(resource));
            _messages = messages ?? throw new ArgumentNullException(nameof(messages));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        
        public EntityToRowMapResult MapToRow(PackageEntity entity, Loadlist loadlist)
        {
            var rowMaps = _resources.GetStorage<RowMappingResource, string>().Get().OrderBy(a => a.Priority);
            var colMaps = _resources.GetStorage<ColumnMappingResource, string>().Get();
            ConditionContextEditor conditionEditor = new ConditionContextEditor(_messages, _resources);
            BuildingContextEditor buildingEditor = new BuildingContextEditor(_messages, _resources, _configuration);
            conditionEditor.UpdateContext(entity);
            buildingEditor.UpdateContext(new List<PackageEntity> { entity }, 1);
            foreach (var rowMap in rowMaps)
                if (rowMap.EntityConditions == null || _resolver.Resolve(rowMap.EntityConditions, conditionEditor.Context))
                {
                    var row = loadlist.AddRow();
                    row.IsVirtual = rowMap.IsVirtual;
                    foreach (var colMap in colMaps.Where(a => rowMap.ColumnNames?.Contains(a.Name) == true))
                        if (colMap.Conditions == null || _resolver.Resolve(colMap.Conditions, conditionEditor.Context))
                        {
                            ExtractInfo? matchedExtractDto = null;
                            foreach (var parameter in entity.Parameters)
                                if (colMap.Extracters?.ContainsKey(parameter.Key) == true)
                                    matchedExtractDto = colMap.Extracters[parameter.Key];
                            if (matchedExtractDto == null)
                                return EntityToRowMapResult.Error(_messages.Get(MessageKeys.CouldNotExtractParamValueForColumn, entity.Name, colMap.Name));
                            var extractResult = _extracter.Extract(matchedExtractDto, buildingEditor.Context);
                            if (!extractResult.HasParameters)
                                return EntityToRowMapResult.Error(_messages.Get(MessageKeys.CouldNotExtractParamValueForColumn, entity.Name, colMap.Name));
                            else
                                row[colMap.Name!] = extractResult.GetFirstAnyParameterValue();
                        }
                    return EntityToRowMapResult.Success(row);
                }
            return EntityToRowMapResult.Error(_messages.Get(MessageKeys.EntityNotMatchAnyLoadlistRowBuildRule, entity.Name));
        }



        //public EntityToRowMapResult MapToEntity(LoadlistRow row)
        //{
        //    var rowMaps = _resources.GetResource<RowMappingResource, string>().Get().OrderBy(a => a.Priority);
        //    var colMaps = _resources.GetResource<ColumnMappingResource, string>().Get();
        //    ConditionContextEditor conditionEditor = new ConditionContextEditor(_messages, _resources);
        //    BuildingContextEditor buildingEditor = new BuildingContextEditor(_messages, _resources, _configuration);
        //    PackageEntity rowEntity = new PackageEntity("loadlistrow", "loadlistrow", new List<PackageEntity>(),
        //        new Dictionary<string, string>(), new Dictionary<string, UserParameter> { { "row", new UserParameter("row", row) } });
        //    conditionEditor.UpdateContext(rowEntity);
        //    buildingEditor.UpdateContext(new List<PackageEntity> { rowEntity }, 1);
        //    foreach (var rowMap in rowMaps)
        //    {
        //        if (_resolver.Resolve(rowMap.EntityConditions!, conditionEditor.Context))
        //        {
        //            Dictionary<string, string> parameters = new Dictionary<string, string>();
        //            string? name = null;
        //            foreach (var colMap in colMaps.Where(a => rowMap.ColumnNames!.Contains(a.Name!)))
        //            {
        //                var parameterSet = colMap.Extracts.Select(a => _extracter.Extract(a, buildingEditor.Context)).
        //                        FirstOrDefault(a => a.HasParameters);
        //                if (!parameterSet.HasParameters)
        //                    return EntityToRowMapResult.Error(_messages.Get(MessageKeys.CouldNotExtractParamValueForColumn, rowEntity.Name, colMap.Name));
        //                else if (colMap.Name == rowMap.EntityNameColumn)
        //                    name = parameterSet.GetFirstAnyParameterValue();
        //                else
        //                    parameters.Add(parameterSet.GetFirstAnyParameterKey()!, parameterSet.GetFirstAnyParameterValue()!);
        //            }
        //            return EntityToRowMapResult.Success(new EntityBuildingResult(parameters, new Dictionary<string, UserParameter>(),
        //                name ?? "", Guid.NewGuid().ToString()));
        //        }                    
        //    }
        //    return EntityToRowMapResult.Error(_messages.Get(MessageKeys.CouldNotMapLoadlistRowToEntity, row.Index));
        //}        
    }
}
