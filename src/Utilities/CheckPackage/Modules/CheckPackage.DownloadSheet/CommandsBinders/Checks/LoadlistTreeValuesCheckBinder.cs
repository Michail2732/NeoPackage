using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Checks;
using CheckPackage.DownloadSheet.Commands;
using CheckPackage.DownloadSheet.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.DownloadSheet.CommandsBinders
{
    public class LoadlistTreeValuesCheckBinder : IParameterCheckCommandBinder
    {
        public ParameterCheckCommand Bind(ParameterCheckJson json)
        {
            var castedJson = (LoadlistTreeValuesCheckJson)json;
            return new LoadlistTreeValuesParameterCheck(
                castedJson.TreeId,
                castedJson.RowsFilter.Select(a => new Entities.RowFilter(a.ColumnName, a.RegexPattern)).ToList(),
                new Entities.ColumnFilter(castedJson.ColumnsFilter),
                castedJson.Message
            )
            {
                Inverse = castedJson.Inverse,
                Logic = castedJson.Logic                
            };
        }

        public bool CanBind(ParameterCheckJson json) => json is LoadlistTreeValuesCheckJson;
    }
}
