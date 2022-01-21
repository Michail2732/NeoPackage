using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.Core.Regex
{
    public class MatchResult : IReadOnlyDictionary<string, string>
    {
        private readonly string[] _keyValues;

        public MatchResult(IEnumerable<KeyValuePair<string, string>> keyValues)
        {
            if (keyValues is null)            
                throw new ArgumentNullException(nameof(keyValues));
            _keyValues = new string[keyValues.Count()];
            Count = _keyValues.Length / 2;
            int index = 0;
            foreach (var keyValue in keyValues)
            {
                _keyValues[index] = keyValue.Key;
                _keyValues[Count + index] = keyValue.Value;
                index++;
            }
        }


        public string this[string key]
        {            
            get 
            {                
                if (TryGetValue(key, out var result)) return result;
                throw new KeyNotFoundException(key);
            }
        }

        public IEnumerable<string> Keys => _keyValues.Take(Count);
        public IEnumerable<string> Values => _keyValues.Skip(Count);
        public int Count { get; private set; }

        public bool ContainsKey(string key) => _keyValues.Take(Count).Contains(key);

        public bool TryGetValue(string key, out string value)
        {
            value = null;            
            for (int i = 0; i < Count; i++)            
                if (_keyValues[i] == key)
                {
                    value = _keyValues[Count + i];
                    return true;
                }            
            return false;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)            
                yield return new KeyValuePair<string, string>(_keyValues[i], _keyValues[Count + i]);
            yield break;
        }        

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();        
    }
}
