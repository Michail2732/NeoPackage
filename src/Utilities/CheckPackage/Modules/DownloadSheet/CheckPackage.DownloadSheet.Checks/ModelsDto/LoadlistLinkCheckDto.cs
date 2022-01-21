using Package.Abstraction.Entities;
using Package.Localization;
using Package.Validation.Context;
using System.Linq;

namespace CheckPackage.DownloadSheet.Checks
{
    public class LoadlistLinkCheckDto : LoadlistBaseCheckDto
    {
        public LoadlistLinkCheckDto(string parameterId, string? errorMessage) : 
            base(parameterId, errorMessage)
        {
        }

        public string? FilterId { get; set; }
        public string? FilterIdTo { get; set; }
        public ushort MinCountLink { get; set; }
        

        public override Result Validate(ValidationContext context)
        {
            var result = ValidateProtected(context);
            if (!result.IsSuccess)
                return result;            
            if (string.IsNullOrEmpty(FilterId))
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotSetProperty, nameof(FilterId)));
            if (string.IsNullOrEmpty(FilterIdTo))
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotSetProperty, nameof(FilterIdTo)));            
            if (!RowFilters.Any(a => a.Id == FilterId))
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotFoundLoadlistFilter, FilterId));
            if (!RowFilters.Any(a => a.Id == FilterIdTo))
                return Result.Error(context.MessageBuilder.Get(MessageKeys.NotFoundLoadlistFilter, FilterIdTo));
            return Result.Success();
        }

    }
}
