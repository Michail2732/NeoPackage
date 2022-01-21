using CheckPackage.Base.Checks;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckPackage.Base.Configuration
{
    public class ValueCheckConvertStrategy : CheckConvertStrategy
        <EntityParameterValueCheckJson, EntityParameterCheckDto>
    {
        public ValueCheckConvertStrategy() : base(JsonIdentifierKeys.ValueCheckId) { }


        protected override EntityParameterValueCheckJson ToJson(JObject obj)
        {            
            return obj.ToObject<EntityParameterValueCheckJson>() ??
                throw new JsonSerializationException();
        }

        protected override EntityParameterCheckDto ToModel(EntityParameterValueCheckJson a)
        {            
            return new EntityParameterCheckDto(a.ParameterId, a.Message, a.Operator, a.Value)
            {                       
                Inverse = a.Inverse,
                Logic = a.Logic                
            };
        }
    }
}
