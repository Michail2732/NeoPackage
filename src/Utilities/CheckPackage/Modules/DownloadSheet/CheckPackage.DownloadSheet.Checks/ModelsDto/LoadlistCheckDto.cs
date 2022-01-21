using Package.Abstraction.Entities;
using Package.Validation.Context;

namespace CheckPackage.DownloadSheet.Checks
{
    public class LoadlistCheckDto : LoadlistBaseCheckDto
    {
        public CheckType CheckType { get; set; }

        public LoadlistCheckDto(string parameterId, string? errorMessage): 
            base(parameterId, errorMessage)
        {

        }

        public override Result Validate(ValidationContext context)
        {
            return ValidateProtected(context);
        }
    }
}
