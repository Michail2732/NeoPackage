using CheckPackage.DownloadSheet.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Mapping
{
    public interface ILoadlistExcelReader
    {
        Loadlist Read(string filepath, int rowStart);
    }
}
