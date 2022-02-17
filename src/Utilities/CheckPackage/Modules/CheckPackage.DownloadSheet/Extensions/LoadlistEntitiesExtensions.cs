using CheckPackage.DownloadSheet.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.DownloadSheet.Extensions
{
    public static class LoadlistEntitiesExtensions
    {
        //public static string GetIdentity(this LoadlistStructureCheckDto checkDto, LoadlistRow row)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (var column in checkDto.IdentificationColumns)
        //    {
        //        if (row.HasColumn(column))
        //            sb.Append(row[column] + "; ");
        //    }
        //    return sb.ToString();
        //}

        public static IEnumerable<LoadlistColumn> FilterOut(this ColumnFilter? filter, IEnumerable<LoadlistColumn> columns)
        {
            return filter == null ? columns : filter.Filter(columns);
        }

        public static IEnumerable<LoadlistRow> FilterOut(this IEnumerable<RowFilter> filters, IEnumerable<LoadlistRow> rows)
        {
            foreach (var rowFilter in filters)
                rows = rowFilter.Filter(rows);
            return rows;
        }

        public static Loadlist? FindLoadlist(this Package_ package)
        {
            var loadlistEntity = new EntityStackEnumerable(package.Entities).FirstOrDefault(a =>
              a.UserParameters.Values.Any(b => b.Value.GetType().IsAssignableFrom(typeof(Loadlist))));
            return loadlistEntity?.UserParameters.First(b =>
                b.Value.GetType().IsAssignableFrom(typeof(Loadlist))).Value.Value as Loadlist;
        }

    }
}
