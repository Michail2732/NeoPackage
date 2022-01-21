using CheckPackage.Configuration.Entities;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Core.Condition;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CheckPackage.Configuration.Converters
{
    public interface IConditionConvertStrategy
    {        
        BaseConditionJson ToJsonBase(JObject obj);
        ConditionInfo ToModelBase(BaseConditionJson json);
        string JsonModelAssemblyName { get; }
        string JsonModelTypeName { get; }
        string ConditionId { get; }
    }

    public abstract class ConditionConvertStrategy<TJson, TModel> : IConditionConvertStrategy
        where TJson : BaseConditionJson
        where TModel : ConditionInfo
    {
        private static SpinLock _lock = new SpinLock();
        private static List<string> _conditionIds = new List<string>();

        public string JsonModelAssemblyName { get; }
        public string JsonModelTypeName { get; }
        public string ConditionId { get; }

        public ConditionConvertStrategy(string conditionId)
        {
            JsonModelAssemblyName = typeof(TJson).Assembly.GetName().Name.ToLower();
            JsonModelTypeName = typeof(TJson).Name.ToLower();
            if (string.IsNullOrEmpty(conditionId)) throw new ArgumentNullException(nameof(conditionId));
            ConditionId = conditionId;
            AddIdToRegester(conditionId);
        }

        public BaseConditionJson ToJsonBase(JObject obj) => ToJson(obj);
        public ConditionInfo ToModelBase(BaseConditionJson json)
        {
            if ((json as TJson) == null)
                throw new JsonSerializationException();
            return ToModel((TJson)json);
        }
        protected abstract TJson ToJson(JObject obj);
        protected abstract TModel ToModel(TJson obj);

        private void AddIdToRegester(string conditionId)
        {
            bool isLock = false;
            try
            {
                _lock.Enter(ref isLock);
                if (_conditionIds.Contains(conditionId))
                    throw new ConfigurationException($"ConditionId conflict: condition id {conditionId} already exists");
                _conditionIds.Add(conditionId);
            }
            finally { if (isLock) _lock.Exit(); }
        }

    }
}
