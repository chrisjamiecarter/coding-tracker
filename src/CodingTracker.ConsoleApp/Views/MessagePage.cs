using Spectre.Console;

namespace CodingTracker.ConsoleApp.Views;

/// <summary>
/// A page which displays a parameterised message and title.
/// </summary>
internal class MessagePage : BasePage
{
    #region Methods: Internal

    internal static void Show(string title, string message)
    {
        AnsiConsole.Clear();

        WriteHeader(title);

        AnsiConsole.WriteLine(message);

        WriteFooter();

        // Await user confirmation to continue.
        Console.ReadKey();
    }

    #endregion
}
