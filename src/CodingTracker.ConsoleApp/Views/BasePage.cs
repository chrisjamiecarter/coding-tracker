using HabitTracker.Constants;
using Spectre.Console;
using System.Text;

namespace CodingTracker.ConsoleApp.Views;

/// <summary>
/// The base class for any page view.
/// </summary>
internal abstract class BasePage
{
    #region Methods: Protected

    protected static void WriteFooter()
    {
        AnsiConsole.Write($"{Environment.NewLine}Press any key to continue...");
    }

    protected static void WriteHeader(string title)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(GetHeaderText(title));
    }

    #endregion
    #region Methods: Private

    private static string GetHeaderText(string pageTitle)
    {
        var sb = new StringBuilder();
        sb.AppendLine("----------------------------------------");
        sb.AppendLine($"{Application.Title}: {pageTitle}");
        sb.AppendLine("----------------------------------------");
        sb.AppendLine();
        return sb.ToString();
    }

    #endregion
}
