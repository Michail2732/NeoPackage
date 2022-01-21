using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.DownloadSheet.Entities
{
    internal class LoadlistBuilder
    {
        private readonly Loadlist _loadlist;
        public Loadlist LoadList => _loadlist;

        internal LoadlistBuilder(Loadlist loadlist)
        {
            _loadlist = loadlist ?? throw new ArgumentNullException(nameof(loadlist));
        }

        internal LoadlistColumn AddColumnInternal(string columnName, int index)
        {
            LoadlistColumn column = new LoadlistColumn(_loadlist, columnName, index);
            return column;
        }

        internal LoadlistRow AddRowInternal(IEnumerable<LoadlistColumn> columns, int rowIndex)
        {
            var dictionary = new Dictionary<LoadlistColumn, string>();
            foreach (var column in columns)
                dictionary.Add(column, string.Empty);
            return new LoadlistRow(_loadlist, dictionary, rowIndex);
        }

    }
}
