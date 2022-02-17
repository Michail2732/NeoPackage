using CheckPackage.Core.Dependencies;
using CheckPackage.DownloadSheet.CommandsBinders;
using CheckPackage.DownloadSheet.Entities;
using CheckPackage.DownloadSheet.Repositories;
using CheckPackage.DownloadSheet.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet
{
    public class CheckPackageLoadlistModule : ICheckPackageModule
    {
        public void AddDependencies(CheckPackageOptions options)
        {
            options.CommandBinderBuilder
                .AddParameterCheckBinder<LoadlistLinkParameterCheckBinder>()
                .AddParameterCheckBinder<LoadlistParameterCheckBinder>()
                .AddParameterCheckBinder<LoadlistTreeValuesCheckBinder>()
                .AddPackageCheckBinder<LoadlistStructureCheckBinder>()
                .AddParameterExtractBinder<LoadlistExtractBinder>()
                .AddPackageOutputBinder<PackageLoadlistOuputBinder>();
            options.RepositoriesBuilder
                .AddRepository<ColumnMappingRuleRepository, ColumnMappingRule, string>()
                .AddRepository<RowMappingRuleRepository, RowMappingRule, string>();
        }

        public void AddDependencies(IServiceCollection collection)
        {
            collection.AddSingleton<ILoadlistExcelReader, LoadlistExcelReader>();
            collection.AddSingleton<ILoadlistRowMapper, LoadlistRowMapper>();
        }
    }
}
