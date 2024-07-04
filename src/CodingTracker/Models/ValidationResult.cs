namespace CodingTracker.Models;

public class ValidationResult
{
    public ValidationResult(bool isValid, string message)
    {
        IsValid = isValid;
        Message = message;
    }

    public bool IsValid { get; init; } = false;

    public string Message { get; init; } = "";
}
