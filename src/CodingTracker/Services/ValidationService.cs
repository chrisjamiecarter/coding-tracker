using CodingTracker.Constants;
using CodingTracker.Models;
using System.Globalization;

namespace CodingTracker.Services;

/// <summary>
/// Service to handle all user input validation.
/// </summary>
public static class ValidationService
{
    public static ValidationResult IsValidStartDateTime(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return new ValidationResult(false, "Start time cannot be empty.");
        }

        bool isCorrectDateTimeFormat = DateTime.TryParseExact(input, StringFormat.DateTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDateTime);
        if (!isCorrectDateTimeFormat)
        {
            return new ValidationResult(false, $"Start time in wrong format. Format = {StringFormat.DateTime}.");
        }
        else if (startDateTime > DateTime.Now)
        {
            return new ValidationResult(false, "Start time cannot be in the future.");
        }

        return new ValidationResult(true, "Validation successful");
    }

    public static ValidationResult IsValidEndDateTime(string input, DateTime startDateTime)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return new ValidationResult(false, "End time cannot be empty.");
        }

        bool isCorrectDateTimeFormat = DateTime.TryParseExact(input, StringFormat.DateTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDateTime);
        if (!isCorrectDateTimeFormat)
        {
            return new ValidationResult(false, $"End time in wrong format. Format = {StringFormat.DateTime}.");
        }
        else if (endDateTime > DateTime.Now)
        {
            return new ValidationResult(false, "End time cannot be in the future.");
        }
        else if (endDateTime <= startDateTime)
        {
            return new ValidationResult(false, "End time must be after the start time.");
        }

        return new ValidationResult(true, "Validation successful");
    }
}
