using Package.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.Configuration.Json.Entities
{
    public class PackageConfigurationJson
    {        
        public string? PackageDirectory { get; set; }
        public bool IsCheckHash { get; set; }
        public string? RulesId { get; set; }
        public string? ParametersId { get; set; }
    }
}
