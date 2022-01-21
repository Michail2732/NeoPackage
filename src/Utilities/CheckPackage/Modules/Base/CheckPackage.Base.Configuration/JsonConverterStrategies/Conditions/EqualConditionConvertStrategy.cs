using CheckPackage.Base.Conditions;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckPackage.Base.Configuration
{
    public class EqualConditionConvertStrategy : ConditionConvertStrategy
        <EqualConditionJson, EqualConditionDto>
    {

        public EqualConditionConvertStrategy() : base(JsonIdentifierKeys.EqualConditionId) { }

        protected override EqualConditionJson ToJson(JObject obj)
        {
            return obj.ToObject<EqualConditionJson>() ??
                throw new JsonSerializationException();
        }

        protected override EqualConditionDto ToModel(EqualConditionJson a)
        {            
            return new EqualConditionDto(a.ParameterId)
            {
                Inverse = a.Inverse,
                Logic = a.Logic,                
                Recurse = a.Recurse,
                Value = a.Value
            };
        }
    }
}
