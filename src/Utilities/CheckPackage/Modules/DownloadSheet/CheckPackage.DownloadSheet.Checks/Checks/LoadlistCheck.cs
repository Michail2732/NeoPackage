using CheckPackage.Core.Checks;
using CheckPackage.DownloadSheet.Entities;
using CheckPackage.DownloadSheet.Extensions;
using Package.Abstraction.Entities;
using Package.Validation.Context;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Checks
{
    public class LoadlistCheck : PackageEntityCheckCommand<LoadlistCheckDto>
    {        

        protected override Result CheckProtected(LoadlistCheckDto checkInfo, ValidationContext context)
        {            
            var loadlist = context.CurrentEntity.UserParameters.First(a => a.Value.GetType() == typeof(Loadlist)).Value.As<Loadlist>();
            IEnumerable<LoadlistRow> rows = checkInfo.RowFilters.FilterOut(loadlist.Rows);
            IEnumerable<LoadlistColumn> columns = checkInfo.ColumnFilter.FilterOut(loadlist.Columns);                            

            bool result = true;
            List<string> valueForCheck = new List<string>(columns.Count());

            foreach (var row in rows)
            {                
                foreach (var column in columns)                
                    valueForCheck.Add(row[column]);
                result &= checkInfo.CheckType.Check(valueForCheck);
                valueForCheck.Clear();
            }            
            return new Result(result, null);
        }        
    }
}
