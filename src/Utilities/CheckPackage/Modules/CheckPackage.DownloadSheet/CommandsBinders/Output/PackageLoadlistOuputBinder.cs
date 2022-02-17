using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Extractors;
using CheckPackage.Core.Output;
using CheckPackage.DownloadSheet.Commands;
using CheckPackage.DownloadSheet.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckPackage.DownloadSheet.CommandsBinders
{
    public class PackageLoadlistOuputBinder : IPackageOutputCommandBinder
    {
        public PackageOutputCommand Bind(PackageOutputJson json)
        {
            var castedJson = (PackageLoadlistOutputJson)json;
            return new PackageLoadlistOutput(castedJson.Message);
        }

        public bool CanBind(PackageOutputJson json) => json is PackageLoadlistOutputJson;        
    }
}
