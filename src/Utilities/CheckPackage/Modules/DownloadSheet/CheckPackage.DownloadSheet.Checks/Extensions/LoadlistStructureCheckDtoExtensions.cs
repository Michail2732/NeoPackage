using CheckPackage.DownloadSheet.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Checks.Extensions
{
    public static class LoadlistStructureCheckDtoExtensions
    {

        public static string GetIdentity(this LoadlistStructureCheckDto checkDto, LoadlistRow row)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var column in checkDto.IdentificationColumns)
            {
                if (row.HasColumn(column))
                    sb.Append(row[column] + "; ");
            }
            return sb.ToString();
        }

    }
}
