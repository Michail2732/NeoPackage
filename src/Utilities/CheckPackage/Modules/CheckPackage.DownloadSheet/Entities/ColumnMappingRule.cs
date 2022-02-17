using CheckPackage.Core.Conditions;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Selectors;
using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.Entities
{
    public class ColumnMappingRule: IRepositoryItem<string>
    {
        public string Id { get; }
        public string Column { get;  }
        public string Name { get;  }        
        public ParameterSelectCommand Selector { get; }
        public ParameterExtractCommand Extracter { get; }

        public ColumnMappingRule(string id, string column, string name, ParameterSelectCommand selector, 
            ParameterExtractCommand extracter)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Column = column ?? throw new ArgumentNullException(nameof(column));
            Name = name ?? throw new ArgumentNullException(nameof(name));            
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
            Extracter = extracter ?? throw new ArgumentNullException(nameof(extracter));
        }
    }
}
