using CheckPackage.Base.Conditions;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckPackage.Base.Configuration
{
    public class RegexConditionConvertStrategy : ConditionConvertStrategy
        <RegexMatchConditionJson, RegexConditionDto>
    {
        public RegexConditionConvertStrategy() : base(JsonIdentifierKeys.RegexConditionId) { }


        protected override RegexMatchConditionJson ToJson(JObject obj)
        {
            return obj.ToObject<RegexMatchConditionJson>() ??
                throw new JsonSerializationException();
        }

        protected override RegexConditionDto ToModel(RegexMatchConditionJson a)
        {            
            return new RegexConditionDto(a.ParameterId, a.RegexTemplate)
            {
                Inverse = a.Inverse,
                Logic = a.Logic,                
                Recurse = a.Recurse                
            };
        }
    }
}
