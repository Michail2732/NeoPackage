using CheckPackage.Core.Condition;
using Package.Abstraction.Entities;
using System.Collections.Generic;

namespace CheckPackage.DownloadSheet.Mapping
{
    public class RowMappingResource: IEntity<string>
    {
        public string Id { get; set; }
        public int Priority { get; set; }
        public bool IsVirtual { get; set; }
        public int EntityLevel { get; set; }
        public ConditionInfo? EntityConditions { get; set; }
        public string? EntityNameColumn { get; set; }
        public List<string>? ColumnNames { get; set; }
    }    
}
