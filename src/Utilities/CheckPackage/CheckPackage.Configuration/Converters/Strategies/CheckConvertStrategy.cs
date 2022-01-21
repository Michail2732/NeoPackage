using CheckPackage.Configuration.Entities;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Core.Checks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;

namespace CheckPackage.Configuration.Converters
{
    public interface ICheckConvertStrategy
    {                
        BaseCheckJson ToJsonBase(JObject obj);
        CheckInfo ToModelBase(BaseCheckJson json);
        string JsonModelAssemblyName { get; }
        string JsonModelName { get; }
        string CheckId { get; }
    }

    public abstract class CheckConvertStrategy<TJson, TModel> : ICheckConvertStrategy
        where TJson : BaseCheckJson
        where TModel : CheckInfo
    {
        private static SpinLock _lock = new SpinLock();
        private static List<string> _checkIds = new List<string>();

        public string JsonModelAssemblyName { get; }
        public string JsonModelName { get; }
        public string CheckId { get; }

        public CheckConvertStrategy(string checkId)
        {
            if (string.IsNullOrEmpty(checkId))            
                throw new System.ArgumentException(nameof(checkId));            
            JsonModelAssemblyName = typeof(TJson).Assembly.GetName().Name.ToLower();
            JsonModelName = typeof(TJson).Name.ToLower();
            CheckId = checkId;
            AddIdToRegester(checkId);
            
        }

        public BaseCheckJson ToJsonBase(JObject obj) => ToJson(obj);
        public CheckInfo ToModelBase(BaseCheckJson json)
        {
            if ((json as TJson) == null)
                throw new JsonSerializationException();
            return ToModel((TJson)json);
        }
        protected abstract TJson ToJson(JObject obj);
        protected abstract TModel ToModel(TJson obj);

        private void AddIdToRegester(string checkId)
        {
            bool isLock = false;
            try
            {
                _lock.Enter(ref isLock);
                if (_checkIds.Contains(checkId))
                    throw new ConfigurationException($"CheckId conflict: check id {checkId} already exists");
                _checkIds.Add(checkId);
            }
            finally { if (isLock) _lock.Exit(); }
        }
    }
}
