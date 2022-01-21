using CheckPackage.Configuration.Entities;
using CheckPackage.Core.Checks;
using CheckPackage.Core.Condition;
using CheckPackage.Core.Extracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CheckPackage.Configuration.Converters
{
    public interface IJsonConverterFacade
    {
        CheckInfo CheckConvert(JObject jsonCheck);
        CheckInfo CheckConvert(BaseCheckJson jsonCheck);
        ConditionInfo ConditionConvert(JObject jsonCondition);
        ConditionInfo ConditionConvert(BaseConditionJson jsonCondition);
        ExtractInfo ExtractConvert(JObject jsonExtract);
        ExtractInfo ExtractConvert(BaseExtractJson jsonExtract);
        JsonConverter[] GetConverters();
    }
}
