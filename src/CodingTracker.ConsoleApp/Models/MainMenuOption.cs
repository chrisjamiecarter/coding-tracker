namespace CodingTracker.ConsoleApp.Models;

/// <summary>
/// Holds an index and a name of a main menu option.
/// </summary>
internal class MainMenuOption
{
    internal int Index { get; init; }

    internal string? Name { get; init; }

    public MainMenuOption(int index, string name)
    {
        Index = index;
        Name = name;
    }
}
