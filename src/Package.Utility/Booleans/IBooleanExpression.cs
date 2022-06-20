using System.Collections.Generic;
using Package.Domain;

namespace Package.Utility.Booleans
{
    public interface IBooleanExpression<in TItem, in TContext>
    {
        Logical Logic { get; }
        bool Invoke(TItem item, TContext context);
    }
}