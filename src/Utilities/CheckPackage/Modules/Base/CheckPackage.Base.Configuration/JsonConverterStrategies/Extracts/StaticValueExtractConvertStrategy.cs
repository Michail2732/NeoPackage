using CheckPackage.Base.Extracters;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckPackage.Base.Configuration
{
    public class StaticValueExtractConvertStrategy : ExtractConvertStrategy
        <StaticValueExtractJson, EntityParameterExtractDto>
    {
        public StaticValueExtractConvertStrategy() : base(JsonIdentifierKeys.StaticValueExtractId) { }

        protected override StaticValueExtractJson ToJson(JObject obj)
        {
            return obj.ToObject<StaticValueExtractJson>() ??
                throw new JsonSerializationException();
        }

        protected override EntityParameterExtractDto ToModel(StaticValueExtractJson a)
        {            
            var result = new EntityParameterExtractDto(a.EntityParameterId, a.EntityParameterId)
            {                
                InAllEntities = a.InAllEntities                
            };
            result.SetNext(new StaticParameterValueExtractDto(a.ParameterId, a.EntityParameterId, a.ParameterValue));
            return result;
        }
    }
}
