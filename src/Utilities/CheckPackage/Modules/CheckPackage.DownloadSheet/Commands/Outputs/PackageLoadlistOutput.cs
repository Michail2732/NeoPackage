using CheckPackage.Core.Output;
using CheckPackage.DownloadSheet.Entities;
using CheckPackage.DownloadSheet.Service;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Commands
{
    public class PackageLoadlistOutput : PackageOutputCommand
    {
        public PackageLoadlistOutput(string message): base(message)
        {

        }

        protected override Result InnerOutput(Package_ package, PackageContext context)
        {
            var mapper = context.GetService<ILoadlistRowMapper>();
            Loadlist loadlist = new Loadlist();
            var columnMaps = context.RepositoryProvider.GetRepository<ColumnMappingRule, string>().Get();
            foreach (var colMap in columnMaps)
                loadlist.AddColumn(colMap.Name);
            EntityStackEnumerable entitiesEnumerable = new EntityStackEnumerable(package.Entities);
            foreach (var entity in entitiesEnumerable)
            {                                                
                var mappingResult = mapper.MapToRow(entity, loadlist, context);
                if (!mappingResult.Result.IsSuccess)                                    
                    return Result.Error();                
            }
            return Result.Success();
        }
    }
}
