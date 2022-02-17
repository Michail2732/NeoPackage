using CheckPackage.Core.Conditions;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Selectors;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;

namespace CheckPackage.PackageBuilding.Rules
{
    public class ParameterBuildRule: IRepositoryItem<string>
    {
        public string Id { get; }
        public IReadOnlyList<EntityConditionCommand>? Conditions { get; }
        public ParameterSelectCommand? Selector { get; }
        public ParameterExtractCommand Extracter { get; }

        public ParameterBuildRule(string id, IReadOnlyList<EntityConditionCommand>? conditions, 
            ParameterSelectCommand? selector, ParameterExtractCommand extracter)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Conditions = conditions;
            Selector = selector;
            Extracter = extracter ?? throw new ArgumentNullException(nameof(extracter));
        }
    }
}
