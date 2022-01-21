using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.DownloadSheet.Entities
{ 
    public class LoadlistRow 
    {
        private readonly Dictionary<LoadlistColumn, string> _valuesDict;
        public int Index { get; }
        public Loadlist Loadlist { get; }
        public bool IsVirtual { get; set; }

        internal LoadlistRow(Loadlist loadlist, IDictionary<LoadlistColumn, string> valuesDict,
            int rowIndex)
        {
            if (valuesDict == null)
                throw new ArgumentNullException(nameof(valuesDict));
            Loadlist = loadlist ?? throw new ArgumentNullException(nameof(loadlist));
            _valuesDict = new Dictionary<LoadlistColumn, string>(valuesDict);
            Index = rowIndex;
        }

        public bool HasColumn(string key) => _valuesDict.Any(a => a.Key.ColumnName == key);

        public string? this[string key]
        {
            get => HasColumn(key) ? _valuesDict.First(a => a.Key.ColumnName == key).Value : 
                throw new ArgumentException($"column with id \"{key}\" does not exist");
            set 
            {
                if (!HasColumn(key))
                    throw new KeyNotFoundException($"Columns with \"{key}\" not exist");
                var column = _valuesDict.First(a => a.Key.ColumnName == key).Key;                
                column.SetValue(this, Index, value ?? "");
                _valuesDict[column] = value ?? ""; 
            }
        }

        public string this[LoadlistColumn column]
        {
            get => _valuesDict.ContainsKey(column) ? _valuesDict[column] :
                throw new ArgumentException($"column with name \"{column.ColumnName}\" does not exist");
            set
            {
                if (!_valuesDict.ContainsKey(column))
                    throw new KeyNotFoundException($"Columns with \"{column.ColumnName}\" not exist");                
                _valuesDict[column] = value;
                column.SetValue(this, Index, value);
            }
        }        

        public KeyValuePair<LoadlistColumn, string> this[int index]
        {
            get 
            {
                if (index >= _valuesDict.Count || index < 0)
                    throw new IndexOutOfRangeException(nameof(index));
                return _valuesDict.ElementAt(index);
            }
        }
    }
}
