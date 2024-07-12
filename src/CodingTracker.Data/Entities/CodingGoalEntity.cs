namespace CodingTracker.Data.Entities;

public class CodingGoalEntity
{
    public CodingGoalEntity()
    {
        // Required for Dapper.
    }

    public int Id { get; init; }

    public double WeeklyDurationInHours { get; init; }
}
