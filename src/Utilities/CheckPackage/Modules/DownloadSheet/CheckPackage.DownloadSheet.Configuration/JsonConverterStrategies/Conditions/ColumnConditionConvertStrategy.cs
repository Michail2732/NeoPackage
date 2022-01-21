using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using CheckPackage.DownloadSheet.Conditions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class ColumnConditionConvertStrategy : ConditionConvertStrategy
        <ColumnConditionJson, ColumnConditionDto>
    {
        public ColumnConditionConvertStrategy() : base(JsonIdentifierKeys.LoadlistColumnConditionId) { }

        protected override ColumnConditionJson ToJson(JObject obj)
        {
            return obj.ToObject<ColumnConditionJson>() ??
                throw new JsonSerializationException();
        }

        protected override ColumnConditionDto ToModel(ColumnConditionJson obj)
        {
            return new ColumnConditionDto(obj.ParameterId, obj.Column)
            {                
                Inverse = obj.Inverse,
                Logic = obj.Logic,                                
                Value = obj.Value,
                Recurse = obj.Recurse
            };
        }
    }    
}
