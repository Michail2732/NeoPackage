using CheckPackage.Core.Conditions;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;

namespace CheckPackage.DownloadSheet.Entities
{
    public class RowMappingRule: IRepositoryItem<string>
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public int Priority { get; }
        public bool IsVirtual { get; }
        public int EntityLevel { get; }
        public IReadOnlyList<EntityConditionCommand>? EntityConditions { get; set; }        
        public IReadOnlyList<string> ColumnIds { get; set; }

        public RowMappingRule(int priority, bool isVirtual, int entityLevel,
            IReadOnlyList<EntityConditionCommand>? entityConditions, IReadOnlyList<string> columnIds)
        {
            Priority = priority;
            IsVirtual = isVirtual;
            EntityLevel = entityLevel;
            EntityConditions = entityConditions;
            ColumnIds = columnIds ?? throw new ArgumentNullException(nameof(columnIds));
        }
    }    
}
