using CheckPackage.Core.Checks;
using CheckPackage.DownloadSheet.Entities;
using CheckPackage.DownloadSheet.Extensions;
using Package.Abstraction.Entities;
using Package.Validation.Context;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Checks
{
    public class LoadlistLinkCheck : PackageEntityCheckCommand<LoadlistLinkCheckDto>
    {        

        protected override Result CheckProtected(LoadlistLinkCheckDto checkInfo, ValidationContext context)
        {
            var loadlist = context.CurrentEntity.UserParameters.First(a => a.Value.GetType() == typeof(Loadlist)).Value.As<Loadlist>();
            IEnumerable<LoadlistRow> rowsOne = checkInfo.RowFilters.Where(a => a.Id == checkInfo.FilterId).FilterOut(loadlist.Rows);
            IEnumerable<LoadlistRow> rowsTwo = checkInfo.RowFilters.Where(a => a.Id == checkInfo.FilterIdTo).FilterOut(loadlist.Rows);
            IEnumerable<LoadlistColumn> columnns = checkInfo.ColumnFilter.FilterOut(loadlist.Columns);
            
            foreach (var rowOne in rowsOne)
            {
                int count = 0;
                foreach (var rowTwo in rowsTwo)
                {
                    bool isMatch = true;
                    foreach (var column in columnns)
                        if (rowOne[column] != rowTwo[column])
                        {
                            isMatch = false;
                            break;
                        }
                    if (isMatch)
                        count++;
                }
                if (count < checkInfo.MinCountLink)
                    return Result.Error();
            }
            return Result.Success();
        }
    }
}
