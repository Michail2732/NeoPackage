using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Entities
{
    public class LoadlistColumn
    {
        public readonly string ColumnName;
        public readonly int Index;

        private readonly List<string> _values = new List<string>();
        public IReadOnlyList<string> Values { get => _values; }
        
        public Loadlist Loadlist { get; private set; }


        public LoadlistColumn(Loadlist loadlist, string columnName, int index)
        {
            ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            Index = index;
            Loadlist = loadlist ?? throw new ArgumentNullException(nameof(loadlist));
        }

        internal void AddValue(LoadlistBuilder builder, string value)
        {
            if (builder.LoadList != Loadlist)
                throw new ArgumentException("difference loadlist");
            _values.Add(value);
        }
        internal void SetValue(LoadlistRow row, int index, string value)
        {
            if (row.Loadlist != Loadlist)
                throw new ArgumentException("difference loadlist");
            _values[index] = value;
        } 

        public override bool Equals(object obj)
        {
            return obj is LoadlistColumn column &&
                   ColumnName == column.ColumnName &&
                   Index == column.Index;
        }

        public override int GetHashCode()
        {
            int hashCode = -1562678849;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ColumnName);
            hashCode = hashCode * -1521134295 + Index.GetHashCode();                      
            return hashCode;
        }
    }
}
