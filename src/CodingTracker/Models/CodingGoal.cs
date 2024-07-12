using CodingTracker.Data.Entities;

namespace CodingTracker.Models;

/// <summary>
/// Coding session data transformation object.
/// </summary>
public class CodingGoal
{
    public CodingGoal(CodingGoalEntity entity)
    {
        Id = entity.Id;
        WeeklyDurationInHours = entity.WeeklyDurationInHours;
    }

    public CodingGoal(double weeklyDurationInHours)
    {
        WeeklyDurationInHours = weeklyDurationInHours;
    }

    public int Id { get; init; }

    public double WeeklyDurationInHours { get; init; }
}
