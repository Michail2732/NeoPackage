using CheckPackage.DownloadSheet.Entities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CheckPackage.DownloadSheet.Service
{
    public class LoadlistExcelReader : ILoadlistExcelReader
    {
        public Loadlist Read(string filepath, int rowStart)
        {   
            Loadlist loadlist = new Loadlist();
            using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                using (var workBook = new XLWorkbook(fs, XLEventTracking.Disabled))
                {                    
                    var ws = workBook.Worksheets.FirstOrDefault();
                    if (ws == null)
                        throw new LoadlistException($"Excel file does not contains worksheets");
                    int columnsCount = ws.ColumnsUsed().Count();
                    int rowCount = ws.RowsUsed().Count();
                    var headerRow = ws.Row(rowStart);
                    for (int i = 1; i <= columnsCount; i++)                    
                        loadlist.AddColumn(headerRow.Cell(i).GetString());
                    for (int i = rowStart; i <= rowCount; i++)
                    {
                        var row = loadlist.AddRow();
                        for (int j = 1; j < columnsCount; j++)
                        {
                            row[loadlist.Columns[j - 1]] = ws.Row(i).Cell(j).GetString();
                        }
                    }                                        
                }
            }
            return loadlist;
        }
    }
}
