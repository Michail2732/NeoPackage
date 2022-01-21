using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using CheckPackage.DownloadSheet.Checks;
using CheckPackage.DownloadSheet.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistStructureCheckConvertStrategy : CheckConvertStrategy
        <LoadlistStructureCheckJson, LoadlistStructureCheckDto>
    {
        public LoadlistStructureCheckConvertStrategy() : base(JsonIdentifierKeys.LoadlistStructCheckId) { }

        protected override LoadlistStructureCheckJson ToJson(JObject obj)
        {
            return obj.ToObject<LoadlistStructureCheckJson>() ??
                throw new JsonSerializationException();
        }

        protected override LoadlistStructureCheckDto ToModel(LoadlistStructureCheckJson obj)
        {
            return new LoadlistStructureCheckDto(obj.ParameterId, obj.Message, obj.IdentifierColumns)
            {                
                ColumnFilter = obj.ColumnsFilter?.Count > 0 ? new ColumnFilter(obj.ColumnsFilter) : null,
                RowFilters = obj.RowsFilter != null ? obj.RowsFilter.Select(a => new
                    RowFilter(a.ColumnName!, a.RegexPattern!)).ToList() : new List<RowFilter>(),
                Inverse = obj.Inverse,
                Logic = obj.Logic,                                   
            };
        }
    }
}
