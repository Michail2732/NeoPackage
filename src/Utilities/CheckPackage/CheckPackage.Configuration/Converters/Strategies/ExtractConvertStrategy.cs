using CheckPackage.Configuration.Entities;
using CheckPackage.Configuration.Exceptions;
using CheckPackage.Core.Extracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CheckPackage.Configuration.Converters
{
    public interface IExtractConvertStrategy
    {        
        BaseExtractJson ToJsonBase(JObject obj);
        ExtractInfo ToModelBase(BaseExtractJson json);
        string JsonModelAssemblyName { get; }
        string JsonModelTypeName { get; }
        string ExtractorId { get; }
    }

    public abstract class ExtractConvertStrategy<TJson, TModel> : IExtractConvertStrategy
        where TJson : BaseExtractJson
        where TModel : ExtractInfo
    {
        private static SpinLock _lock = new SpinLock();
        private static List<string> _extracterIds =  new List<string>();

        public string JsonModelAssemblyName { get; }
        public string JsonModelTypeName { get; }
        public string ExtractorId { get; }

        public ExtractConvertStrategy(string extractId)
        {
            JsonModelAssemblyName = typeof(TJson).Assembly.GetName().Name.ToLower();
            JsonModelTypeName = typeof(TJson).Name.ToLower();
            if (string.IsNullOrEmpty(extractId)) throw new ArgumentNullException(nameof(extractId));
            ExtractorId = extractId;
            AddIdToRegester(extractId);
        }


        public BaseExtractJson ToJsonBase(JObject obj) => ToJson(obj);
        public ExtractInfo ToModelBase(BaseExtractJson json) 
        {
            if ((json as TJson) == null)
                throw new JsonSerializationException();
            return ToModel((TJson)json);
        }

        protected abstract TJson ToJson(JObject obj);
        protected abstract TModel ToModel(TJson obj);

        private void AddIdToRegester(string extractorId)
        {
            bool isLock = false;
            try
            {
                _lock.Enter(ref isLock);
                if (_extracterIds.Contains(extractorId))
                    throw new ConfigurationException($"ExtractorId conflict: extract id {extractorId} already exists");
                _extracterIds.Add(extractorId);
            }
            finally { if (isLock) _lock.Exit(); }
        }

    }
}
