using CheckPackage.Configuration.Json.Binder;
using CheckPackage.Configuration.Json.Entities;
using CheckPackage.Core.Extractors;
using CheckPackage.DownloadSheet.Commands;
using CheckPackage.DownloadSheet.Configuration;

namespace CheckPackage.DownloadSheet.CommandsBinders
{
    public class LoadlistExtractBinder : IParameterExtractCommandBinder
    {
        public ParameterExtractCommand Bind(ParameterExtractJson json)
        {
            var castedJson = (LoadlistExtractJson)json;
            return new LoadlistExtracter(castedJson.ParameterId);
        }

        public bool CanBind(ParameterExtractJson json) => json is LoadlistExtractJson;        
    }
}
