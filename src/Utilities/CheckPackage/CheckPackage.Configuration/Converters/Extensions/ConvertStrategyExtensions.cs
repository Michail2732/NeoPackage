using CheckPackage.Configuration.Entities;
using Newtonsoft.Json.Linq;
using System;

namespace CheckPackage.Configuration.Converters
{
    public static class ConvertStrategyExtensions
    {        
        internal static bool IsMatch(this IExtractConvertStrategy strategy, BaseExtractJson extract)
        {
            Type extractType = extract.GetType();
            return strategy.JsonModelTypeName == extractType.Name.ToLower() &&
                        strategy.JsonModelAssemblyName == extractType.Assembly.GetName().Name.ToLower();
        }

        internal static bool IsMatch(this ICheckConvertStrategy strategy, BaseCheckJson check)
        {
            Type checkType = check.GetType();
            return strategy.JsonModelName == checkType.Name.ToLower() &&
                        strategy.JsonModelAssemblyName == checkType.Assembly.GetName().Name.ToLower(); 
        }

        internal static bool IsMatch(this IConditionConvertStrategy strategy, BaseConditionJson condition)
        {
            Type conditionType = condition.GetType();
            return strategy.JsonModelTypeName == conditionType.Name.ToLower() &&
                        strategy.JsonModelAssemblyName == conditionType.Assembly.GetName().Name.ToLower(); ;
        }


        internal static bool IsMatch(this IExtractConvertStrategy strategy, JObject jObject)
        {
            if (jObject.Property("extract_id") == null || jObject["extract_id"]?.ToString() != strategy.ExtractorId)
                return false;
            return true;            
        }

        internal static bool IsMatch(this IConditionConvertStrategy strategy, JObject jObject)
        {
            if (jObject.Property("condition_id") == null || jObject["condition_id"]?.ToString() != strategy.ConditionId)
                return false;
            return true;            
        }

        internal static bool IsMatch(this ICheckConvertStrategy strategy, JObject jObject)
        {
            if (jObject.Property("check_id") == null || jObject["check_id"]?.ToString() != strategy.CheckId)
                return false;
            return true;
        }

    }
}
