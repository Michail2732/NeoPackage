using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Package.Abstraction.Extensions
{
    public static class EnumerableResultExtensions
    {
        public static Result ToAggregateResult(this IEnumerable<Result> results, bool logic)
        {
            if (results?.Any() == true)
            {
                bool result = results.First().IsSuccess;
                StringBuilder sb = new StringBuilder();
                if (results.First().Details != null)
                    sb.Append(results.First().Details + "\n");
                foreach (var item in results.Skip(1))
                {
                    result = logic ? result & item.IsSuccess : result | item.IsSuccess;
                    if (item.Details != null)
                        sb.Append(item.Details + "\n");
                }
                return new Result(result, sb.ToString());
            }
            return default;
        }

    }
}
