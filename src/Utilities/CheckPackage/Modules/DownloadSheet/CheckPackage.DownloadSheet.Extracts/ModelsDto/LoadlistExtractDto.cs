using CheckPackage.Core.Extracts;
using Package.Abstraction.Entities;
using Package.Localization;

namespace CheckPackage.DownloadSheet.Extracters
{
    public class LoadlistExtractDto : ExtractInfo
    {
        public LoadlistExtractDto(string parameterId, string sourceParameterId) :
            base(parameterId, sourceParameterId)
        {
        }

        public override Result Validate(ExtractContext context)
        {
            if (string.IsNullOrEmpty(ParameterId))
                return Result.Error(context.Messages.Get(MessageKeys.NotSetProperty, nameof(ParameterId)));            
            return Result.Success();                      
        }
    }
}
