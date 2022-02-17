using CheckPackage.Core.Checks;
using CheckPackage.Core.Entities;
using CheckPackage.Core.Resources;
using CheckPackage.DownloadSheet.Entities;
using CheckPackage.DownloadSheet.Extensions;
using Package.Abstraction.Entities;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Commands
{
    public class LoadlistTreeValuesParameterCheck : ParameterCheckCommand
    {
        public string TreeValueId { get; }
        public IReadOnlyList<RowFilter> RowFilters { get; }
        public ColumnFilter ColumnFilter { get; }

        public LoadlistTreeValuesParameterCheck(string treeValueId, IReadOnlyList<RowFilter> rowFilters, 
            ColumnFilter columnFilter, string message): base(message)
        {
            TreeValueId = treeValueId ?? throw new ArgumentNullException(nameof(treeValueId));
            RowFilters = rowFilters ?? throw new ArgumentNullException(nameof(rowFilters));
            ColumnFilter = columnFilter ?? throw new ArgumentNullException(nameof(columnFilter));
        }




        // todo: messages
        protected override Result InnerCheck(Parameter parameter, PackageContext context)
        {
            var loadlist = parameter.Value as Loadlist;            
            var tree = context.RepositoryProvider.GetRepository<ValueTreeResource, string>().GetItem(TreeValueId);
            if (loadlist == null)
                return Result.Error(context.Messages[MessageKeys.IncorrectTypeCustomParameter, parameter.Id, typeof(Loadlist)]);
            if (tree == null)
                return Result.Error("todo: messages");            
            IEnumerable<LoadlistRow> rows = RowFilters.FilterOut(loadlist.Rows);
            IEnumerable<LoadlistColumn> columns = ColumnFilter.FilterOut(loadlist.Columns);

            foreach (var row in rows)
                foreach (var column in columns)                
                    if (!tree.Nodes.Any(a => a.Values.Contains(row[column])))
                        return Result.Error();                
            return Result.Success();
        }                        
    }
}
