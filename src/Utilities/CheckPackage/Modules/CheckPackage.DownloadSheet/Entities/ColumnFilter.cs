using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.DownloadSheet.Entities
{
    public class ColumnFilter: IRepositoryItem<string>
    {
        public string Id { get; set; } = string.Empty;
        public readonly IReadOnlyList<string> ColumnsName;

        public ColumnFilter(IReadOnlyList<string> columnName)
        {
            ColumnsName = columnName ?? throw new ArgumentNullException(nameof(columnName));
        }

        public IEnumerable<LoadlistColumn> Filter(IEnumerable<LoadlistColumn> columns)
        {
            if (columns is null)            
                throw new ArgumentNullException(nameof(columns));
            List<LoadlistColumn> filtredColumns = new List<LoadlistColumn>();
            if (columns.Any())            
                filtredColumns.AddRange(columns.Where(a => ColumnsName.Contains(a.ColumnName)));            
            return filtredColumns;
        }
    }
}
