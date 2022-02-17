using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Checks;
using CheckPackage.DownloadSheet.Commands;
using CheckPackage.DownloadSheet.Configuration;
using CheckPackage.DownloadSheet.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.CommandsBinders
{
    public class LoadlistLinkParameterCheckBinder : IParameterCheckCommandBinder
    {
        public ParameterCheckCommand Bind(ParameterCheckJson json)
        {
            var castedJson = (LoadlistLinkCheckJson)json;
            return new LoadlistLinkParameterCheck(
                new RowFilter(castedJson.RowsFilterFrom.ColumnName, castedJson.RowsFilterFrom.RegexPattern),
                new RowFilter(castedJson.RowsFilterTo.ColumnName, castedJson.RowsFilterTo.RegexPattern),
                new ColumnFilter(castedJson.ColumnsFilter),
                castedJson.MinCountLink,
                castedJson.Message
            )
            {
                Inverse = castedJson.Inverse,
                Logic = castedJson.Logic
            };
        }

        public bool CanBind(ParameterCheckJson json) => json is LoadlistLinkCheckJson;        
    }
}
