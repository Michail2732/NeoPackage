using System;

public class CheckResult
{
    public readonly string ItemId;
    public readonly string CheckId;
    public readonly string? ErrorMessage;
    public readonly bool IsSuccess;

    public CheckResult(string itemId, string checkId)
    {
        ItemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
        CheckId = checkId ?? throw new ArgumentNullException(nameof(checkId));
        IsSuccess = true;
        ErrorMessage = null;
    }
    
    public CheckResult(string itemId, string checkId, string errorMessage)
    {
        ItemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
        CheckId = checkId ?? throw new ArgumentNullException(nameof(checkId));
        IsSuccess = false;
        ErrorMessage = errorMessage?? throw new ArgumentNullException(nameof(errorMessage));
    }
    
}