using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Checks;
using CheckPackage.DownloadSheet.Commands;
using CheckPackage.DownloadSheet.Configuration;
using CheckPackage.DownloadSheet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.DownloadSheet.CommandsBinders
{
    public class LoadlistParameterCheckBinder : IParameterCheckCommandBinder
    {
        public ParameterCheckCommand Bind(ParameterCheckJson json)
        {
            var castedJson = (LoadlistCheckJson)json;
            return new LoadlistParameterCheck(
                castedJson.RowsFilter.Select(a => new RowFilter(a.ColumnName, a.RegexPattern)).ToList(),
                new ColumnFilter(castedJson.ColumnsFilter),
                castedJson.CheckType,
                castedJson.Message
            )
            {
                Inverse = castedJson.Inverse,
                Logic = castedJson.Logic
            };
        }

        public bool CanBind(ParameterCheckJson json) => json is LoadlistCheckJson;
    }
}
