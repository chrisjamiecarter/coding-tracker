namespace CodingTracker.Models;

/// <summary>
/// Holds a validation result and message.
/// </summary>
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
