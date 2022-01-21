using CheckPackage.Core.Condition;
using CheckPackage.Core.Extracts;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Mapping
{
    public class ColumnMappingResource: IEntity<string>
    {
        public string? Id { get; set; }
        public string? Column { get; set; }
        public string? Name { get; set; }
        public ConditionInfo? Conditions { get; set; }
        public Dictionary<string, ExtractInfo>? Extracters { get; set; }        
    }
}
