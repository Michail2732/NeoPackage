using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Checks;
using CheckPackage.DownloadSheet.Commands;
using CheckPackage.DownloadSheet.Configuration;
using CheckPackage.DownloadSheet.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.CommandsBinders
{
    public class LoadlistStructureCheckBinder : IPackageCheckCommandBinder
    {
        public PackageCheckCommand Bind(PackageCheckJson json)
        {
            var castedJson = (LoadlistStructureCheckJson)json;
            return new LoadlistStructureCheck(castedJson.IdentifierColumns, castedJson.Message)
            { 
                Logic = castedJson.Logic,
                Inverse = castedJson.Inverse
            };
        }

        public bool CanBind(PackageCheckJson json) => json is LoadlistStructureCheckJson;        
    }
}
