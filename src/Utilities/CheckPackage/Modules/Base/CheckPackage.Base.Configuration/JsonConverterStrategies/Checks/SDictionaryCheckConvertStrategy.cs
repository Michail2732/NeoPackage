using CheckPackage.Base.Checks;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckPackage.Base.Configuration
{
    public class SDictionaryCheckConvertStrategy : CheckConvertStrategy
        <EntityParameterSDictionaryCheckJson, EntityParameterSDictionaryCheckDto>
    {
        public SDictionaryCheckConvertStrategy() : base(JsonIdentifierKeys.SDictionaryCheckId) { }

        protected override EntityParameterSDictionaryCheckJson ToJson(JObject obj)
        {
            return obj.ToObject<EntityParameterSDictionaryCheckJson>() ??
                 throw new JsonSerializationException();
        }

        protected override EntityParameterSDictionaryCheckDto ToModel(EntityParameterSDictionaryCheckJson a)
        {            
            return new EntityParameterSDictionaryCheckDto(a.ParameterId, a.Message, a.DictionaryName)
            {                        
                Inverse = a.Inverse,
                Logic = a.Logic                
            };
        }
    }
}
