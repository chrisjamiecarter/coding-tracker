using CodingTracker.ConsoleApp.Models;
using CodingTracker.Constants;
using CodingTracker.Models;
using CodingTracker.Services;
using Spectre.Console;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodingTracker.ConsoleApp.Views;

internal class UpdateCodingSessionPage : BasePage
{
    #region Constants

    private const string PageTitle = "Update Coding Session";

    private const string PromptTitle = "Select an option...";

    #endregion
    #region Properties

    internal static IEnumerable<PromptChoice> PageOptions
    {
        get
        {
            return
            [
                new(0, "Close page"),                
            ];
        }
    }

    #endregion
    #region Methods: Internal

    internal static CodingSession? Show(List<CodingSession> codingSessions)
    {
        CodingSession? nullCodingSession = null;

        AnsiConsole.Clear();

        WriteHeader(PageTitle);

        var option = GetOption(codingSessions);

        if (option.Id == 0)
        {
            // Close page.
            return nullCodingSession;
        }
        else
        {
            var codingSession = codingSessions.First(x => x.Id == option.Id);
            return GetUpdatedCodingSession(codingSession);
        }
    }

    private static CodingSession? GetUpdatedCodingSession(CodingSession codingSession)
    {
        CodingSession? nullCodingSession = null;

        // Configure table data.
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");
        table.AddRow(codingSession.Id.ToString(), codingSession.StartTime.ToString(StringFormat.DateTime), codingSession.EndTime.ToString(StringFormat.DateTime), codingSession.Duration.ToString("F2"));
        
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

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

        return new CodingSession(start, end)
        { 
            Id = codingSession.Id
        };
    }

    private static double CalculateDuration(DateTime start, DateTime end)
    {
        return (end - start).TotalHours;
    }

    #endregion
    #region Methods: Private

    private static PromptChoice GetOption(List<CodingSession> codingSessions)
    {
        // Add the coding sessions to the existing PageOptions.
        IEnumerable<PromptChoice> pageOptions = [.. PageOptions, ..codingSessions.Select(x => new PromptChoice(x.Id, $"{x.StartTime.ToString(StringFormat.DateTime)} - {x.EndTime.ToString(StringFormat.DateTime)} ({x.Duration.ToString("F2")})"))];

        return AnsiConsole.Prompt(
                new SelectionPrompt<PromptChoice>()
                .Title(PromptTitle)
                .AddChoices(pageOptions)
                .MoreChoicesText("Show more...")
                .UseConverter(c => c.Name!)
                );
    }

    #endregion
}
