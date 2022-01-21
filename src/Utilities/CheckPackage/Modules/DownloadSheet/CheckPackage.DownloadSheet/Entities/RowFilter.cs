using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckPackage.DownloadSheet.Entities
{
    public class RowFilter : IEntity<string>
    {
        public string Id { get; set; } = string.Empty;
        public string FilterColumnName { get; }
        public string FilterRegexPattern { get;}

        public RowFilter(string filterColumnName, string filterRegexPattern)
        {
            FilterColumnName = filterColumnName ?? throw new ArgumentNullException(nameof(filterColumnName));
            FilterRegexPattern = filterRegexPattern ?? throw new ArgumentNullException(nameof(filterRegexPattern));
        }

        public IEnumerable<LoadlistRow> Filter(IEnumerable<LoadlistRow> rows)
        {
            if (rows is null)            
                throw new ArgumentNullException(nameof(rows));            

            List<LoadlistRow> filtredRows = new List<LoadlistRow>();
            if (rows.Any())
            {                
                Regex regex = new Regex(FilterRegexPattern);
                LoadlistColumn? filterCol = rows.First().Loadlist.Columns.FirstOrDefault(
                    a => a.ColumnName == FilterColumnName);                
                if (filterCol != null)                    
                    foreach (var row in rows)                    
                        if (row.HasColumn(filterCol.ColumnName)
                            && regex.IsMatch(row[filterCol]))
                            filtredRows.Add(row);                    
            }
            return filtredRows;
        }
    }
}
