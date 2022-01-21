using CheckPackage.Base.Extracters;
using CheckPackage.Configuration.Dependencies;
using CheckPackage.Core.Dependencies;
using CheckPackage.DownloadSheet.Checks;
using CheckPackage.DownloadSheet.Conditions;
using CheckPackage.DownloadSheet.Configuration;
using CheckPackage.DownloadSheet.Extracters;
using CheckPackage.DownloadSheet.Mapping;
using CheckPackage.DownloadSheet.Resources;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CheckPackage.DownloadSheet.Module
{
    public class CheckPackageDownloadSheetModule : ICheckPackageModule
    {
        public void AddDependencies(CheckPackageOptions options)
        {
            // checks
            options.Checks.Add<LoadlistCheck, LoadlistCheckDto>();
            options.Checks.Add<LoadlistLinkCheck, LoadlistLinkCheckDto>();
            options.Checks.Add<LoadlistSDictionaryCheck, LoadlistSDictionaryCheckDto>();
            options.Checks.Add<LoadlistStructureCheck, LoadlistStructureCheckDto>();
            // conditions
            options.Conditions.Add<ColumnConditionResolver, ColumnConditionDto>();
            // extracters
            options.Extracters.Add<LoadlistColumnExtracter, LoadlistColumnExtractDto>();
            options.Extracters.Add<LoadlistExistExtracter, LoadlistExistExtractDto>();
            options.Extracters.Add<LoadlistExtracter, LoadlistExtractDto>();
            // resources
            options.Resources.AddResource<ColumnMapResource, ColumnMappingResource, string>(ServiceLifetime.Singleton);
            options.Resources.AddResource<RowMapResource, RowMappingResource, string>(ServiceLifetime.Singleton);
        }

        public void AddDependencies(IServiceCollection collection)
        {
            collection.AddConfigurationConvertStrategies(builder =>
            {
                builder.AddCheckStrategy<LoadlistCheckConvertStrategy, LoadlistCheckJson, LoadlistCheckDto>();
                builder.AddCheckStrategy<LoadlistLinkCheckConvertStrategy, LoadlistLinkCheckJson, LoadlistLinkCheckDto>();
                builder.AddCheckStrategy<LoadlistSDictionaryCheckConvertStrattegy, LoadlistSDictionaryCheckJson, LoadlistSDictionaryCheckDto>();
                builder.AddCheckStrategy<LoadlistStructureCheckConvertStrategy, LoadlistStructureCheckJson, LoadlistStructureCheckDto>();

                builder.AddConditionStrategy<ColumnConditionConvertStrategy, ColumnConditionJson, ColumnConditionDto>();                

                builder.AddExtractStrategy<ColumnExtractConvertStrategy, LoadlistColumnExtractJson, EntityParameterExtractDto>();
                builder.AddExtractStrategy<LoadlistExistExtractConvertStrategy, LoadlistExistExtractJson, EntityParameterExtractDto>();
                builder.AddExtractStrategy<LoadlistExtractConvertStrategy, LoadlistExtractJson, EntityParameterExtractDto>();                
            });            
            collection.AddSingleton<ILoadlistRowMapper, LoadlistRowMapper>();
            collection.AddSingleton<MappingContextBuilder>();
            collection.AddSingleton<ILoadlistExcelReader, LoadlistExcelReader>();
        }
    }
}
