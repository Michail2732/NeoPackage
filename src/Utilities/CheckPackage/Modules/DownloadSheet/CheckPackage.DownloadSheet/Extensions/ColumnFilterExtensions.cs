using CheckPackage.DownloadSheet.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Extensions
{
    public static class ColumnFilterExtensions
    {

        public static IEnumerable<LoadlistColumn> FilterOut(this ColumnFilter? filter, IEnumerable<LoadlistColumn> columns)
        {
            return filter == null ? columns : filter.Filter(columns);
        }
    }
}
