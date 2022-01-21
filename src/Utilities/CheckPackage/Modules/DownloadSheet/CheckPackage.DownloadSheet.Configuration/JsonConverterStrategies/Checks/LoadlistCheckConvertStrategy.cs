using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using CheckPackage.DownloadSheet.Checks;
using CheckPackage.DownloadSheet.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistCheckConvertStrategy : CheckConvertStrategy
        <LoadlistCheckJson, LoadlistCheckDto>
    {
        public LoadlistCheckConvertStrategy() : base(JsonIdentifierKeys.LoadlistCheckId){ }

        protected override LoadlistCheckJson ToJson(JObject obj)
        {
            return obj.ToObject<LoadlistCheckJson>() ??
                throw new JsonSerializationException();
        }

        protected override LoadlistCheckDto ToModel(LoadlistCheckJson obj)
        {
            return new LoadlistCheckDto(obj.ParameterId, obj.Message)
            {
                CheckType = obj.CheckType,
                ColumnFilter = obj.ColumnsFilter != null ? new ColumnFilter(obj.ColumnsFilter) : null,                
                Inverse = obj.Inverse,
                Logic = obj.Logic,                
                RowFilters = obj.RowsFilter != null ? obj.RowsFilter.Select(a => new
                    RowFilter(a.ColumnName!, a.RegexPattern!)).ToList() : new List<RowFilter>()
            };
        }
    }
}
