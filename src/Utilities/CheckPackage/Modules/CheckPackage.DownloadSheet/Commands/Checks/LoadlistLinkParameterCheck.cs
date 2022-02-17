using CheckPackage.Core.Checks;
using CheckPackage.Core.Entities;
using CheckPackage.DownloadSheet.Entities;
using CheckPackage.DownloadSheet.Extensions;
using Package.Abstraction.Entities;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Commands
{
    public class LoadlistLinkParameterCheck : ParameterCheckCommand
    {
        RowFilter RowFilterFrom { get; }
        RowFilter RowFilterTo { get; }
        public ColumnFilter ColumnFilter { get;  }        
        public ushort MinCountLink { get; }

        public LoadlistLinkParameterCheck(RowFilter rowFilterFrom, RowFilter rowFilterTo, ColumnFilter columnFilter, 
            ushort minCountLink, string message): base(message)
        {
            RowFilterFrom = rowFilterFrom ?? throw new ArgumentNullException(nameof(rowFilterFrom));
            RowFilterTo = rowFilterTo ?? throw new ArgumentNullException(nameof(rowFilterTo));
            ColumnFilter = columnFilter ?? throw new ArgumentNullException(nameof(columnFilter));
            MinCountLink = minCountLink;
        }

        protected override Result InnerCheck(Parameter parameter, PackageContext context)
        {
            var loadlist = parameter.Value as Loadlist;
            if (loadlist == null)
                return Result.Error(context.Messages[MessageKeys.IncorrectTypeCustomParameter, parameter.Id, typeof(Loadlist)]);

            IEnumerable<LoadlistRow> rowsFrom = RowFilterFrom.Filter(loadlist.Rows);
            IEnumerable<LoadlistRow> rowsTo = RowFilterTo.Filter(loadlist.Rows);
            IEnumerable<LoadlistColumn> columnns = ColumnFilter.FilterOut(loadlist.Columns);

            foreach (var rowOne in rowsFrom)
            {
                int count = 0;
                foreach (var rowTwo in rowsTo)
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
                if (count < MinCountLink)
                    return Result.Error();
            }
            return Result.Success();
        }
    }
}
