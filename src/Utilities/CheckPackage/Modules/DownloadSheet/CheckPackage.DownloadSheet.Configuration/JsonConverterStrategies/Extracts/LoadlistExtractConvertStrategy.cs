using CheckPackage.Configuration.Converters;
using CheckPackage.DownloadSheet.Extracters;
using CheckPackage.Base.Extracters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using CheckPackage.Configuration.Utilities;

namespace CheckPackage.DownloadSheet.Configuration
{
    public class LoadlistExtractConvertStrategy : ExtractConvertStrategy
        <LoadlistExtractJson, EntityParameterExtractDto>
    {
        public LoadlistExtractConvertStrategy() : base(JsonIdentifierKeys.LoadlistExtractId) { }


        protected override LoadlistExtractJson ToJson(JObject obj)
        {
            return obj.ToObject<LoadlistExtractJson>() ??
                throw new JsonSerializationException();
        }

        protected override EntityParameterExtractDto ToModel(LoadlistExtractJson obj)
        {
            var result = new EntityParameterExtractDto(obj.EntityParameterId, obj.EntityParameterId)
            {                
                InAllEntities = obj.InAllEntities,                
            };
            result.SetNext(new LoadlistExtractDto(obj.ParameterId, obj.EntityParameterId));
            return result;
        }
    }
}
