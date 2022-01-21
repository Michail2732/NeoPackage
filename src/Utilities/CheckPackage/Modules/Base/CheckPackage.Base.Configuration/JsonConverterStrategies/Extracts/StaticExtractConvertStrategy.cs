using CheckPackage.Base.Extracters;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckPackage.Base.Configuration
{
    public class StaticExtractConvertStrategy : ExtractConvertStrategy
        <StaticExtractJson, EntityParameterExtractDto>
    {
        public StaticExtractConvertStrategy() : base(JsonIdentifierKeys.StaticExtractId) { }

        protected override StaticExtractJson ToJson(JObject obj)
        {
            return obj.ToObject<StaticExtractJson>() ??
                throw new JsonSerializationException();
        }

        protected override EntityParameterExtractDto ToModel(StaticExtractJson a)
        {            
            var result = new EntityParameterExtractDto(a.EntityParameterId, a.EntityParameterId)
            {                
                InAllEntities = a.InAllEntities                
            };
            result.SetNext(new StaticParameterExtractDto(a.ParameterId, a.EntityParameterId, a.StaticParameterId));
            return result;
        }
    }
}
