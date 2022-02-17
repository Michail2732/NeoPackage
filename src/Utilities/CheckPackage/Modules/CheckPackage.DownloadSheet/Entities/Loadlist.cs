using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CheckPackage.DownloadSheet.Entities
{
    public class Loadlist
    {
        private readonly LoadlistBuilder _builder;

        private readonly List<LoadlistColumn> _columns;
        public IReadOnlyList<LoadlistColumn> Columns => _columns;

        private readonly List<LoadlistRow> _rows;
        public IReadOnlyList<LoadlistRow> Rows => _rows;        


        public Loadlist()
        {
            _builder = new LoadlistBuilder(this);
            _columns = new List<LoadlistColumn>();                        
            _rows = new List<LoadlistRow>();
        }        

        public LoadlistRow AddRow()
        {
            var newRow = _builder.AddRowInternal(_columns, Rows.Count);
            _rows.Add(newRow);            
            _columns.ForEach(a => a.AddValue(_builder, string.Empty));
            return newRow;
        }

        public LoadlistColumn AddColumn(string name)
        {
            var newCol = _builder.AddColumnInternal(name, _columns.Count());
            _rows.ForEach(a => newCol.AddValue(_builder, string.Empty));
            _columns.Add(newCol);
            return newCol;
        }

        //public void Remove(LoadlistRow row)
        //{
        //    if (!_rows.Contains(row))
        //        throw new ArgumentException("Rows does not exist in loadlist");
        //    _rows.Remove(row);
        //    for (int i = 0; i < _columns.Count; i++)
        //    {
        //        _columns[i].
        //    }
        //}       
    }
}
