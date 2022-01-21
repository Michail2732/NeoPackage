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
    public class LoadlistSDictionaryCheckConvertStrattegy : CheckConvertStrategy
        <LoadlistSDictionaryCheckJson, LoadlistSDictionaryCheckDto>
    {

        public LoadlistSDictionaryCheckConvertStrattegy() : base(JsonIdentifierKeys.LoadlistSDictCheckId) { }

        protected override LoadlistSDictionaryCheckJson ToJson(JObject obj)
        {
            return obj.ToObject<LoadlistSDictionaryCheckJson>() ??
                throw new JsonSerializationException();
        }

        protected override LoadlistSDictionaryCheckDto ToModel(LoadlistSDictionaryCheckJson obj)
        {
            return new LoadlistSDictionaryCheckDto(obj.ParameterId, obj.Message)
            {
                DictionaryId = obj.DictionaryId,
                ColumnFilter = obj.ColumnsFilter != null ? new ColumnFilter(obj.ColumnsFilter) : null,                
                Inverse = obj.Inverse,
                Logic = obj.Logic,                
                RowFilters = obj.RowsFilter != null ? obj.RowsFilter.Select(a => new
                    RowFilter(a.ColumnName!, a.RegexPattern!)).ToList() : new List<RowFilter>()
            };
        }
    }
}
