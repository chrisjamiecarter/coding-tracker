using CodingTracker.Application.Constants;
using CodingTracker.Application.Models;
using CodingTracker.ConsoleApp.Models;
using Spectre.Console;

namespace CodingTracker.ConsoleApp.Views;

/// <summary>
/// Page which allows users to select a CodingSession they want to delete.
/// </summary>
internal class DeleteCodingSessionPage : BasePage
{
    private const string PageTitle = "Delete Coding Session";

    internal static IEnumerable<UserChoice> PageChoices
    {
        get
        {
            return
            [
                new(0, "Close page"),                
            ];
        }
    }

    internal static CodingSession? Show(List<CodingSession> codingSessions)
    {
        WriteHeader(PageTitle);

        var option = GetOption(codingSessions);

        return option.Id == 0 ? null : codingSessions.First(x => x.Id == option.Id);
    }

    private static UserChoice GetOption(List<CodingSession> codingSessions)
    {
        // Add the coding sessions to the existing PageChoices.
        IEnumerable<UserChoice> pageChoices = [.. PageChoices, .. codingSessions.Select(x => new UserChoice(x.Id, $"{x.StartTime.ToString(StringFormat.DateTime)} - {x.EndTime.ToString(StringFormat.DateTime)} ({x.Duration:F2})"))];

        return AnsiConsole.Prompt(
                new SelectionPrompt<UserChoice>()
                .Title(PromptTitle)
                .AddChoices(pageChoices)
                .UseConverter(c => c.Name!)
                );
    }
}
