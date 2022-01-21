using CheckPackage.Core.Checks;
using Package.Abstraction.Entities;
using Package.Localization;
using Package.Validation.Context;

namespace CheckPackage.DownloadSheet.Checks
{
    public class LoadlistSDictionaryCheckDto : LoadlistBaseCheckDto
    {
        public LoadlistSDictionaryCheckDto(string parameterId, string? errorMessage) : 
            base(parameterId, errorMessage)
        {
        }

        public string? DictionaryId { get; set; }
        

        public override Result Validate(ValidationContext context)
        {
            var result = ValidateProtected(context);
            if (!result.IsSuccess)
                return result;
            if (string.IsNullOrEmpty(DictionaryId))
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotSetProperty, nameof(ParameterId)));
            if (!context.Resources.HasResource<SimpleDictionaryResource, string>())
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotFoundSimpleDiscts));
            if (context.Resources.GetStorage<SimpleDictionaryResource, string>().GetItem(DictionaryId!) == null)
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotFoundDictionary, DictionaryId));
            return Result.Success();
        }
    }
}
