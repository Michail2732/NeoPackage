using Package.Abstraction.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckPackage.Core.Resources
{
    public sealed class MatrixDictionaryResource : IEntity<string>, IReadOnlyDictionary<string, IReadOnlyList<string>> 
    {
        private readonly Dictionary<string, IReadOnlyList<string>> _matrixDictionary;
        public string Id { get; set; }        

        public MatrixDictionaryResource(Dictionary<string, List<string>> matrixDictionary, string id)
        {
            if (matrixDictionary == null)
                throw new ArgumentNullException(nameof(matrixDictionary));
            _matrixDictionary = new Dictionary<string, IReadOnlyList<string>>();
            foreach (var dictItem in matrixDictionary)            
                _matrixDictionary.Add(dictItem.Key, dictItem.Value);
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }        

        public string Name => Name;        
        public int Count => _matrixDictionary.Count;
        public IEnumerable<string> Keys => _matrixDictionary.Keys;
        public IEnumerable<IReadOnlyList<string>> Values => _matrixDictionary.Values;
        public IReadOnlyList<string> this[string key] => _matrixDictionary[key];
        public bool TryGetValue(string key, out IReadOnlyList<string> value) => _matrixDictionary.TryGetValue(key, out value);
        public bool ContainsKey(string key) => _matrixDictionary.ContainsKey(key);
        public IEnumerator<KeyValuePair<string, IReadOnlyList<string>>> GetEnumerator() => _matrixDictionary.GetEnumerator();       
        IEnumerator IEnumerable.GetEnumerator() => _matrixDictionary.GetEnumerator();               
    }
}
