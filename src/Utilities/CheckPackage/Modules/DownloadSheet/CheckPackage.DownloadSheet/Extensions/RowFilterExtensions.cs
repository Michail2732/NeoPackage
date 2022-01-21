using CheckPackage.DownloadSheet.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Extensions
{
    public static class RowFilterExtensions
    {
        public static IEnumerable<LoadlistRow> FilterOut(this IEnumerable<RowFilter> filters, IEnumerable<LoadlistRow> rows)
        {
            foreach (var rowFilter in filters)
                rows = rowFilter.Filter(rows);
            return rows;
        }

    }
}
