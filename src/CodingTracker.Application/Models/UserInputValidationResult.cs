namespace CodingTracker.Application.Models;

/// <summary>
/// The result of a validation action on a user input and the output message.
/// NOTE: Not named ValidationResult as clashes with the Spectre Console library used in the console application.
/// </summary>
public class UserInputValidationResult
{
    public UserInputValidationResult(bool isValid, string message)
    {
        IsValid = isValid;
        Message = message;
    }

    public bool IsValid { get; init; } = false;
    public string Message { get; init; } = "";
}
