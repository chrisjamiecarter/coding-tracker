namespace CodingTracker.ConsoleApp.Models;

/// <summary>
/// Use to represent an option a user can selection from a Spectre Console Prompt.
/// </summary>
internal class PromptChoice
{
    internal int Id { get; init; }

    internal string? Name { get; init; }

    public PromptChoice(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
