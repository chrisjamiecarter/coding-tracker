using System.Text;
using CodingTracker.Application.Constants;
using Spectre.Console;

namespace CodingTracker.ConsoleApp.Views;

/// <summary>
/// The base class for any page view.
/// </summary>
internal abstract class BasePage
{
    protected static readonly string PromptTitle = "Select an [blue]option[/]...";
    private static readonly string DividerLine = "[cyan2]----------------------------------------[/]";

    protected static void WriteFooter()
    {
        AnsiConsole.Markup($"{Environment.NewLine}Press any [blue]key[/] to continue...");
    }

    protected static void WriteHeader(string title)
    {
        AnsiConsole.Clear();
        AnsiConsole.Markup(GetHeaderText(title));
    }

    private static string GetHeaderText(string pageTitle)
    {
        var sb = new StringBuilder();
        sb.AppendLine(DividerLine);
        sb.AppendLine($"[bold cyan2]{Page.Title}[/]: [honeydew2]{pageTitle}[/]");
        sb.AppendLine(DividerLine);
        sb.AppendLine();
        return sb.ToString();
    }
}
