using CheckPackage.Core.Entities;
using CheckPackage.Core.Extractors;
using CheckPackage.DownloadSheet.Entities;
using CheckPackage.DownloadSheet.Service;
using Microsoft.Extensions.Logging;
using Package.Abstraction.Entities;
using Package.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CheckPackage.DownloadSheet.Commands
{
    public class LoadlistExtracter : ParameterExtractCommand
    {                                
        public string ParameterId { get; }

        public LoadlistExtracter(string parameterId)
        {
            ParameterId = parameterId ?? throw new ArgumentNullException(nameof(parameterId));
        }

        // todo: messages
        protected override IEnumerable<Parameter> InnerExtractParameters(IEnumerable<Parameter> source, PackageContext context)
        {
            var mapper = context.GetService<ILoadlistRowMapper>();
            var loadlistReader = context.GetService<ILoadlistExcelReader>();
            var loadlistFilePath = source.FirstOrDefault().Value?.ToString();
            if (string.IsNullOrEmpty(loadlistFilePath) || !File.Exists(loadlistFilePath))
            {
                context.Logger.LogError($"todo: messages");
                yield break;
            }
            Loadlist loadlist = loadlistReader.Read(loadlistFilePath!, 3);
            yield return new Parameter(ParameterId, loadlist);
        }
        
    }
}
