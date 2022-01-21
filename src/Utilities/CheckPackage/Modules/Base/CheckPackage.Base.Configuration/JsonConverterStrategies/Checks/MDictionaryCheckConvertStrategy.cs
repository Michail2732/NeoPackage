using CheckPackage.Base.Checks;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckPackage.Base.Configuration
{
    public class MDictionaryCheckConvertStrategy :  CheckConvertStrategy
            <EntityParameterMDictionaryCheckJson, EntityParameterMDictionaryCheckDto>
    {
        public MDictionaryCheckConvertStrategy() : base(JsonIdentifierKeys.MDictionaryCheckId) { }

        protected override EntityParameterMDictionaryCheckJson ToJson(JObject obj)
        {
            return obj.ToObject<EntityParameterMDictionaryCheckJson>() ??
                throw new JsonSerializationException();
        }

        protected override EntityParameterMDictionaryCheckDto ToModel(EntityParameterMDictionaryCheckJson a)
        {            
            return new EntityParameterMDictionaryCheckDto(a.ParameterId, a.Message, 
                        a.DictionaryName, a.KeyParameterId)
            {                       
                Inverse = a.Inverse,                
                Logic = a.Logic                
            };
        }
    }
}
