using System;
using CheckPackage.Configuration.Converters;
using CheckPackage.DownloadSheet.Checks;
using CheckPackage.DownloadSheet.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using CheckPackage.Configuration.Utilities;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistLinkCheckConvertStrategy : CheckConvertStrategy
        <LoadlistLinkCheckJson, LoadlistLinkCheckDto>
    {
        public LoadlistLinkCheckConvertStrategy() : base(JsonIdentifierKeys.LoadlistLinkCheckId) { }

        protected override LoadlistLinkCheckJson ToJson(JObject obj)
        {
            return obj.ToObject<LoadlistLinkCheckJson>() ??
                throw new JsonSerializationException();
        }

        protected override LoadlistLinkCheckDto ToModel(LoadlistLinkCheckJson obj)
        {
            return new LoadlistLinkCheckDto(obj.ParameterId, obj.Message)
            {
                FilterId = obj.FilterId,
                FilterIdTo = obj.FilterIdTo,
                MinCountLink = obj.MinCountLink,
                ColumnFilter = obj.ColumnsFilter != null ? new ColumnFilter(obj.ColumnsFilter) : null,                
                Inverse = obj.Inverse,
                Logic = obj.Logic,                       
                RowFilters = obj.RowsFilter != null ? obj.RowsFilter.Select(a => new
                    RowFilter(a.ColumnName!, a.RegexPattern!)).ToList() : new List<RowFilter>()
            };
        }
    }
}
