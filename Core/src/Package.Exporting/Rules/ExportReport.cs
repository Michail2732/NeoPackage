using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Package.Exporting.Rules
{
    public class ExportReport : IReadOnlyList<ExportResult>
    {
        private readonly List<ExportResult> _checkResults =
            new List<ExportResult>();

        public bool HasError => _checkResults.Any(a => !a.IsSuccess);
        public IEnumerator<ExportResult> GetEnumerator() => _checkResults.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _checkResults.Count;

        public ExportResult this[int index] => _checkResults[index];

        internal void Add(ExportResult checkResult)
        {
            _checkResults.Add(checkResult);
        }
    }
}
