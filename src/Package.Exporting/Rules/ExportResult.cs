using System;
using Package.Domain;

namespace Package.Exporting.Rules
{

    public class ExportResult
    {
        public readonly string ItemId;
        public readonly bool IsSuccess;
        public readonly string? Message;

        public ExportResult(
            string itemId,
            bool isSuccess,
            string? message)
        {
            ItemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}