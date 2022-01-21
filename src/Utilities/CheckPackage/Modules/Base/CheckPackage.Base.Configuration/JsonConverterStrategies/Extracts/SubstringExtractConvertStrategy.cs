using CheckPackage.Base.Extracters;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckPackage.Base.Configuration
{
    public class SubstringExtractConvertStrategy : ExtractConvertStrategy
        <SubstringExtractJson, EntityParameterExtractDto>
    {
        public SubstringExtractConvertStrategy() : base(JsonIdentifierKeys.SubstringExtractId) { }

        protected override SubstringExtractJson ToJson(JObject obj)
        {
            return obj.ToObject<SubstringExtractJson>() ??
                throw new JsonSerializationException();
        }

        protected override EntityParameterExtractDto ToModel(SubstringExtractJson a)
        {            
            var result = new EntityParameterExtractDto(a.EntityParameterId, a.EntityParameterId)
            {                
                InAllEntities = a.InAllEntities                
            };
            result.SetNext(new SubstringExtractDto(a.ParameterId, a.EntityParameterId, a.GroupName, a.RegexTemplate));
            return result;
        }
    }
}
