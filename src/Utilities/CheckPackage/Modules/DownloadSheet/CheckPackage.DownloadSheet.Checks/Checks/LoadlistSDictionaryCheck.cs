using CheckPackage.Core.Checks;
using CheckPackage.DownloadSheet.Entities;
using CheckPackage.DownloadSheet.Extensions;
using Package.Abstraction.Entities;
using Package.Validation.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Checks
{
    public class LoadlistSDictionaryCheck : PackageEntityCheckCommand<LoadlistSDictionaryCheckDto>
    {        

        protected override Result CheckProtected(LoadlistSDictionaryCheckDto checkInfo, ValidationContext context)
        {            
            var dictionary = context.Resources.GetStorage<SimpleDictionaryResource, string>().GetItem(checkInfo.DictionaryId!);
            var loadlist = context.CurrentEntity.UserParameters.First(a => a.Value.GetType() == typeof(Loadlist)).Value.As<Loadlist>();
            IEnumerable<LoadlistRow> rows = checkInfo.RowFilters.FilterOut(loadlist.Rows);
            IEnumerable<LoadlistColumn> columns = checkInfo.ColumnFilter.FilterOut(loadlist.Columns);                            
                
            foreach (var row in rows)            
                foreach (var column in columns)
                {
                    if (!dictionary.Contains(row[column]))
                        return Result.Error();
                }
            return Result.Success();
        }

    }
}
