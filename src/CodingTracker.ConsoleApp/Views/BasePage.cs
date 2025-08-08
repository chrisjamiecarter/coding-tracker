using System.Text;
using Spectre.Console;

namespace CodingTracker.ConsoleApp.Views;

/// <summary>
/// The base class for any page view.
/// </summary>
internal abstract class BasePage
{
    protected static readonly string ApplicationTitle = "Coding Tracker";
    protected static readonly string PromptTitle = "Select an [blue]option[/]...";
    protected static readonly string DividerLine = "[cyan2]----------------------------------------[/]";

    protected static void WriteFooter()
    {
        AnsiConsole.Write(new Rule().RuleStyle("grey").LeftJustified());
        AnsiConsole.Markup($"{Environment.NewLine}Press any [blue]key[/] to continue...");
        Console.ReadKey();
    }

    protected static void WriteHeader(string title)
    {
        AnsiConsole.Clear();
        AnsiConsole.Markup(GetHeaderText(title));
    }

    private static string GetHeaderText(string pageTitle)
    {
        var builder = new StringBuilder();
        builder.AppendLine(DividerLine);
        builder.AppendLine($"[bold cyan2]{ApplicationTitle}[/]: [honeydew2]{pageTitle}[/]");
        builder.AppendLine(DividerLine);
        builder.AppendLine();
        return builder.ToString();
    }
}
