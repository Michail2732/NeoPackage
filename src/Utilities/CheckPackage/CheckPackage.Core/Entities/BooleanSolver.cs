using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Core.Entities
{
    internal class BooleanSolver
    {
        public static bool Solve(IReadOnlyList<KeyValuePair<LogicalOperator, Func<bool>>> actions)
        {
            bool result = true;
            int state = 0;
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i].Key == LogicalOperator.or)
                {
                    if (state == 0)
                        if (GetNext(actions, i)?.Key != LogicalOperator.and)
                            result |= actions[i].Value.Invoke();
                        else
                            state = actions[i].Value.Invoke() ? 2 : 1;
                    if (state > 0)
                    {
                        result |= state == 4;
                        if (GetNext(actions, i)?.Key != LogicalOperator.and)
                        {
                            result |= actions[i].Value.Invoke();
                            state = 0;
                        }
                        else
                            state = actions[i].Value.Invoke() ? 2 : 1;
                    }
                }
                else
                {
                    if (state == 2 || state == 4)
                        state = actions[i].Value.Invoke() ? 4 : 3;
                    if (GetNext(actions, i) == null)
                        result |= state == 4;
                }
                if (result && GetNext(actions, i)?.Key != LogicalOperator.or)
                    break;
            }
            return result;
        }

        private static KeyValuePair<LogicalOperator, Func<bool>>? GetNext(IReadOnlyList<KeyValuePair<LogicalOperator, Func<bool>>> actions, int afterIndex)
        {
            if (actions.Count >= afterIndex + 1)
                return null;
            return actions[afterIndex + 1];
        }
    }
}
