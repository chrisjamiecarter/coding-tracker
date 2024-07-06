using CodingTracker.Constants;
using CodingTracker.Models;
using CodingTracker.Services;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker.ConsoleApp.Views;

internal class CreateCodingSessionPage : BasePage
{
    #region Constants

    private const string PageTitle = "Create Coding Session";

    #endregion
    #region Methods: Internal

    internal static CodingSession? Show()
    {
        CodingSession? nullCodingSession = null;

        AnsiConsole.Clear();

        WriteHeader(PageTitle);

        // TODO: Refactor!

        var startMessage = $"Enter the start date and time, format '{StringFormat.DateTime}', or 0 to return to main menu: ";
        var startInput = AnsiConsole.Ask<string>(startMessage);
        var startInputValidation = ValidationService.IsValidStartDateTime(startInput);
        while (!startInputValidation.IsValid)
        {
            if (startInput == "0")
            {
                return nullCodingSession;
            }
            AnsiConsole.WriteLine(startInputValidation.Message);
            startInput = AnsiConsole.Ask<string>(startMessage);
            startInputValidation = ValidationService.IsValidStartDateTime(startInput);
        }
        DateTime start = DateTime.ParseExact(startInput, StringFormat.DateTime, CultureInfo.InvariantCulture, DateTimeStyles.None);

        // TODO: Refactor!

        var endMessage = $"Enter the end date and time, format '{StringFormat.DateTime}', or 0 to return to main menu: ";
        var endInput = AnsiConsole.Ask<string>(endMessage);
        var endInputValidation = ValidationService.IsValidEndDateTime(endInput, start);
        while (!endInputValidation.IsValid)
        {
            if (endInput == "0")
            {
                return nullCodingSession;
            }
            AnsiConsole.WriteLine(endInputValidation.Message);
            endInput = AnsiConsole.Ask<string>(endMessage);
            endInputValidation = ValidationService.IsValidEndDateTime(endInput, start);
        }
        DateTime end = DateTime.ParseExact(endInput, StringFormat.DateTime, CultureInfo.InvariantCulture, DateTimeStyles.None);

        // Requirement: Do not let user enter duration. Must be calculated.
        var duration = CalculateDuration(start, end);

        return new CodingSession(start, end, duration);
    }

    private static double CalculateDuration(DateTime start, DateTime end)
    {
        return (end - start).TotalHours;
    }

    #endregion

}
