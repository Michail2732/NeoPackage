using CheckPackage.Base.Extracters;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckPackage.Base.Configuration
{
    public class RegexExtractConvertStrategy : ExtractConvertStrategy
        <RegexExtractJson, EntityParameterExtractDto>
    {
        public RegexExtractConvertStrategy() : base(JsonIdentifierKeys.RegexExtractId) { }

        protected override RegexExtractJson ToJson(JObject obj)
        {
            return obj.ToObject<RegexExtractJson>() ??
                throw new JsonSerializationException();
        }

        protected override EntityParameterExtractDto ToModel(RegexExtractJson a)
        {            
            var result = new EntityParameterExtractDto(a.EntityParameterId,  a.EntityParameterId)
            {                
                InAllEntities = a.InAllEntities                
            };
            result.SetNext(new RegexTemplateExtractDto(a.ParameterId, a.EntityParameterId, a.RegexTemplateId));
            return result;
        }
    }
}
