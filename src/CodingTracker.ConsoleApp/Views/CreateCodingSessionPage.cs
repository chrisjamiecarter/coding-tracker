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
        var dateStringFormat = StringFormat.DateTime;

        var startMessage = $"Enter the start date and time, format '{dateStringFormat}', or 0 to return to main menu: ";
        var startInput = AnsiConsole.Ask<string>(startMessage);
        var startInputValidation = ValidationService.IsValidStartDateTime(startInput, dateStringFormat);
        while (!startInputValidation.IsValid)
        {
            if (startInput == "0")
            {
                return nullCodingSession;
            }
            AnsiConsole.WriteLine(startInputValidation.Message);
            startInput = AnsiConsole.Ask<string>(startMessage);
            startInputValidation = ValidationService.IsValidStartDateTime(startInput, dateStringFormat);
        }
        DateTime start = DateTime.ParseExact(startInput, dateStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);

        // TODO: Refactor!

        var endMessage = $"Enter the end date and time, format '{dateStringFormat}', or 0 to return to main menu: ";
        var endInput = AnsiConsole.Ask<string>(endMessage);
        var endInputValidation = ValidationService.IsValidEndDateTime(endInput, dateStringFormat, start);
        while (!endInputValidation.IsValid)
        {
            if (endInput == "0")
            {
                return nullCodingSession;
            }
            AnsiConsole.WriteLine(endInputValidation.Message);
            endInput = AnsiConsole.Ask<string>(endMessage);
            endInputValidation = ValidationService.IsValidEndDateTime(endInput, dateStringFormat, start);
        }
        DateTime end = DateTime.ParseExact(endInput, dateStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);

        return new CodingSession(start, end);
    }

    #endregion
}
