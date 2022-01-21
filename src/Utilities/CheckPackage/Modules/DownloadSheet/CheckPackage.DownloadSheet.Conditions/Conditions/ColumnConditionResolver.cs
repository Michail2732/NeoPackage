using CheckPackage.Core.Condition;
using CheckPackage.DownloadSheet.Entities;
using System.Linq;

namespace CheckPackage.DownloadSheet.Conditions
{
    public class ColumnConditionResolver : PackageEntityCondition<ColumnConditionDto>
    {
        protected override bool ResolveProtected(ColumnConditionDto condition, ConditionContext context)
        {
            var loadlistRow = context.CurrentEntity.UserParameters.First(a => 
                    a.Key == condition.ParameterId).Value.As<LoadlistRow>();            
            return loadlistRow[condition.Column!] == condition.Value;
        }
    }
}
