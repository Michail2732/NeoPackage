using Package.Abstraction.Entities;
using Package.Localization;
using Package.Validation.Context;
using System;
using System.Collections.Generic;

namespace CheckPackage.DownloadSheet.Checks
{
    public sealed class LoadlistStructureCheckDto : LoadlistBaseCheckDto
    {
        public IReadOnlyList<string> IdentificationColumns { get; }

        public LoadlistStructureCheckDto(string parameterId, string? errorMessage, 
            IReadOnlyList<string> identificationColumns) :
            base(parameterId, errorMessage)
        {
            IdentificationColumns = identificationColumns ?? throw new ArgumentNullException(nameof(identificationColumns));            
        }

        public override Result Validate(ValidationContext context)
        {
            if (IdentificationColumns.Count == 0)
                return Result.Error(context.MessageBuilder.Get(MessageKeys.IdentificationColumnsCountMustBeLarge0));
            return ValidateProtected(context);
        }
    }
}
