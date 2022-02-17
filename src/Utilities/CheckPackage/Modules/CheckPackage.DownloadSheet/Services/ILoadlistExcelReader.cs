using CheckPackage.DownloadSheet.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Service
{
    public interface ILoadlistExcelReader
    {
        Loadlist Read(string filepath, int rowStart);
    }
}
