using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Package.Utility.Booleans
{
    public enum Logical
    {
        Or,
        And
    }
    public static class BooleanSolver
    {
        public static bool Solve<TItem, TContext>(
            IReadOnlyList<IBooleanExpression<TItem, TContext>> booleanBlocks,
            TItem item,
            TContext context
            )
        {
            bool result = true;
            int state = 0;
            for (int i = 0; i < booleanBlocks.Count; i++)
            {
                if (booleanBlocks[i].Logic == Logical.Or)
                {
                    if (state == 0)
                        if (GetNext(booleanBlocks, i)?.Logic != Logical.And)
                            result |= booleanBlocks[i].Invoke(item, context);
                        else
                            state = booleanBlocks[i].Invoke(item, context) ? 2 : 1;
                    if (state > 0)
                    {
                        result |= state == 4;
                        if (GetNext(booleanBlocks, i)?.Logic != Logical.And)
                        {
                            result |= booleanBlocks[i].Invoke(item, context);
                            state = 0;
                        }
                        else
                            state = booleanBlocks[i].Invoke(item, context) ? 2 : 1;
                    }
                }
                else
                {
                    if (state == 2 || state == 4)
                        state = booleanBlocks[i].Invoke(item, context) ? 4 : 3;
                    if (GetNext(booleanBlocks, i) == null)
                        result |= state == 4;
                }
                if (result && GetNext(booleanBlocks, i)?.Logic != Logical.Or)
                    break;
            }
            return result;
        }
        

        private static IBooleanExpression<TItem, TContext>? GetNext<TItem, TContext>(
            IReadOnlyList<IBooleanExpression<TItem, TContext>> actions,
            int afterIndex)
        {
            if (actions.Count >= afterIndex + 1)
                return null;
            return actions[afterIndex + 1];
        }
    }
}
