using CheckPackage.Core.Conditions;
using CheckPackage.Core.Entities;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.PackageBuilding.Resource
{
    public class EntityBuildRuleResource: IEntity<string>
    {
        public string Id { get; }
        public uint EntityLevel { get; }
        public uint Priority { get; }
        public string GroupBy { get; }
        public IReadOnlyList<PackageEntityConditionCommand> Conditions { get; }
        public IReadOnlyList<ParameterBuildRuleResource> ParameterRules { get; }

        public EntityBuildRuleResource(string id, uint entityLevel, uint priority, 
            string groupBy, IReadOnlyList<PackageEntityConditionCommand> conditions, 
            IReadOnlyList<ParameterBuildRuleResource> parameterRules)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            EntityLevel = entityLevel;
            Priority = priority;
            GroupBy = groupBy ?? throw new ArgumentNullException(nameof(groupBy));
            Conditions = conditions ?? throw new ArgumentNullException(nameof(conditions));
            ParameterRules = parameterRules ?? throw new ArgumentNullException(nameof(parameterRules));
        }
    }
}
