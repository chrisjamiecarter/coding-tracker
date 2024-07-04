namespace CodingTracker.ConsoleApp.Models;

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
