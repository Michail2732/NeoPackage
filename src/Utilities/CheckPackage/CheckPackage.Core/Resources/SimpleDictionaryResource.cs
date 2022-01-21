using Package.Abstraction.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Resources
{
    public sealed class SimpleDictionaryResource :IEntity<string>, IReadOnlyList<string>
    {
        private readonly List<string> _values;        

        public SimpleDictionaryResource(List<string> values, string name)
        {
            _values = values ?? throw new ArgumentNullException(nameof(values));
            Id = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string this[int index] => _values[index];
        public string Id { get; private set; }
        public int Count => _values.Count;        

        public IEnumerator<string> GetEnumerator() => _values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();
    }
}
