using CheckPackage.Core.Checks;
using CheckPackage.Core.Entities;
using CheckPackage.DownloadSheet.Entities;
using CheckPackage.DownloadSheet.Extensions;
using Package.Abstraction.Entities;
using Package.Localization;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Commands
{
    public class LoadlistParameterCheck : ParameterCheckCommand
    {
        public IReadOnlyList<RowFilter> RowFilters { get; }
        public ColumnFilter ColumnFilter { get; }
        public CheckType CheckType { get; }

        public LoadlistParameterCheck(IReadOnlyList<RowFilter> rowFilters, ColumnFilter columnFilter, 
            CheckType checkType, string message): base(message)
        {
            if (string.IsNullOrEmpty(message))            
                throw new System.ArgumentException(nameof(message));            
            RowFilters = rowFilters ?? throw new System.ArgumentNullException(nameof(rowFilters));
            ColumnFilter = columnFilter ?? throw new System.ArgumentNullException(nameof(columnFilter));
            CheckType = checkType;
        }        

        protected override Result InnerCheck(Parameter parameter, PackageContext context)
        {

            var loadlist = parameter.Value as Loadlist;
            if (loadlist == null)
                return Result.Error(context.Messages[MessageKeys.IncorrectTypeCustomParameter, parameter.Id, typeof(Loadlist)]);
            IEnumerable<LoadlistRow> rows = RowFilters.FilterOut(loadlist.Rows);
            IEnumerable<LoadlistColumn> columns = ColumnFilter.FilterOut(loadlist.Columns);

            bool result = true;
            List<string> valueForCheck = new List<string>(columns.Count());
            foreach (var row in rows)
            {
                foreach (var column in columns)
                    valueForCheck.Add(row[column]);
                result &= CheckType.Check(valueForCheck);
                valueForCheck.Clear();
            }
            return new Result(result, null);
        }             
    }
}
