using CheckPackage.Base.Conditions;
using CheckPackage.Configuration.Converters;
using CheckPackage.Configuration.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckPackage.Base.Configuration
{
    public class ContainsConditionConvertStrategy : ConditionConvertStrategy
        <ContainsConditionJson, ContainsConditionDto>
    {
        public ContainsConditionConvertStrategy() : base(JsonIdentifierKeys.ContainsConditionId) { }

        protected override ContainsConditionJson ToJson(JObject obj)
        {
            return obj.ToObject<ContainsConditionJson>() ??
                throw new JsonSerializationException();
        }

        protected override ContainsConditionDto ToModel(ContainsConditionJson a)
        {            
            return new ContainsConditionDto(a.ParameterId, a.Values)
            {                
                Inverse = a.Inverse,
                Logic = a.Logic,                
                Recurse = a.Recurse                
            };
        }
    }
}
