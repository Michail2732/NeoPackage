using CheckPackage.Base.Checks;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckPackage.Base.Configuration
{
    public class LengthCheckConvertStrategy : CheckConvertStrategy
        <EntityParameterLengthCheckJson, EntityParameterLengthCheckDto>
    {

        public LengthCheckConvertStrategy(): base(JsonIdentifierKeys.LengthCheckId){ }

        protected override EntityParameterLengthCheckJson ToJson(JObject obj)
        {
            return obj.ToObject<EntityParameterLengthCheckJson>() ??
                throw new JsonSerializationException();
        }

        protected override EntityParameterLengthCheckDto ToModel(EntityParameterLengthCheckJson a)
        {
            return new EntityParameterLengthCheckDto(a.ParameterId, a.Message, a.MinLength, a.MaxLength)
            {                                
                Inverse = a.Inverse,
                Logic = a.Logic                
            };
        }
    }
}
