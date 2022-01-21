using CheckPackage.Core.Conditions;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Selectors;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;

namespace CheckPackage.PackageBuilding.Resource
{
    public class ParameterBuildRuleResource: IEntity<string>
    {
        public string Id { get; }
        public IReadOnlyList<PackageEntityConditionCommand>? Conditions { get; }
        public ParameterSelectCommand? Selector { get; }
        public ParameterExtractCommand Extracter { get; }

        public ParameterBuildRuleResource(string id, IReadOnlyList<PackageEntityConditionCommand>? conditions, 
            ParameterSelectCommand? selector, ParameterExtractCommand extracter)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Conditions = conditions;
            Selector = selector;
            Extracter = extracter ?? throw new ArgumentNullException(nameof(extracter));
        }
    }
}
