using CodingTracker.ConsoleApp.Models;
using CodingTracker.Constants;
using CodingTracker.Models;
using CodingTracker.Services;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker.ConsoleApp.Views;

internal class DeleteCodingSessionPage : BasePage
{
    #region Constants

    private const string PageTitle = "Delete Coding Session";

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
            return codingSessions.First(x => x.Id == option.Id);
        }
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
