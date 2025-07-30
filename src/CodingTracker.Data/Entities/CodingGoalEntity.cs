namespace CodingTracker.Data.Entities;

/// <summary>
/// Represents a row in the CodingGoal table.
/// </summary>
public class CodingGoalEntity
{
    public CodingGoalEntity()
    {
        // Required for Dapper.
    }

    public int Id { get; init; }
    public double WeeklyDurationInHours { get; init; }
}
