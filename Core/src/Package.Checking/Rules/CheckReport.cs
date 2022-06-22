using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Package.Checking.Rules
{
    public class CheckReport: IReadOnlyList<CheckResult>
    {
        private readonly List<CheckResult> _checkResults =
                new List<CheckResult>();

        public bool HasError => _checkResults.Any(a => !a.IsSuccess);
        public IEnumerator<CheckResult> GetEnumerator() => _checkResults.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _checkResults.Count;

        public CheckResult this[int index] => _checkResults[index];

        internal void Add(CheckResult checkResult)
        {
            _checkResults.Add(checkResult);
        }
    }
}