using CodingTracker.Constants;
using CodingTracker.Models;
using System.Globalization;

namespace CodingTracker.Services;

/// <summary>
/// Service to handle all user input validation.
/// </summary>
public static class ValidationService
{
    public static ValidationResult IsValidReportStartDate(string input, string format)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return new ValidationResult(false, "Report start date cannot be empty.");
        }

        bool isCorrectDateFormat = DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        if (!isCorrectDateFormat)
        {
            return new ValidationResult(false, $"Report start date in wrong format. Format = {format}.");
        }
        
        return new ValidationResult(true, "Validation successful");
    }

    public static ValidationResult IsValidReportEndDate(string input, string format, DateTime startDate)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return new ValidationResult(false, "Report end date cannot be empty.");
        }

        bool isCorrectDateFormat = DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDate);
        if (!isCorrectDateFormat)
        {
            return new ValidationResult(false, $"Report end date in wrong format. Format = {format}.");
        }

        // Strip off any time parts.
        startDate = startDate.Date;
        endDate = endDate.Date;

        if (endDate < startDate)
        {
            return new ValidationResult(false, "End date cannot be before the start date.");
        }

        return new ValidationResult(true, "Validation successful");
    }

    public static ValidationResult IsValidStartDateTime(string input, string format)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return new ValidationResult(false, "Start time cannot be empty.");
        }

        bool isCorrectDateTimeFormat = DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDateTime);
        if (!isCorrectDateTimeFormat)
        {
            return new ValidationResult(false, $"Start time in wrong format. Format = {format}.");
        }
        else if (startDateTime > DateTime.Now)
        {
            return new ValidationResult(false, "Start time cannot be in the future.");
        }

        return new ValidationResult(true, "Validation successful");
    }

    public static ValidationResult IsValidEndDateTime(string input, string format, DateTime startDateTime)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return new ValidationResult(false, "End time cannot be empty.");
        }

        bool isCorrectDateTimeFormat = DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDateTime);
        if (!isCorrectDateTimeFormat)
        {
            return new ValidationResult(false, $"End time in wrong format. Format = {format}.");
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
