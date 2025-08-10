
namespace CodingTracker.Application.Services;

/// <summary>
/// Service to handle Coding Goal Progress calculations.
/// </summary>
public class CodingGoalProgressService
{
    private readonly CodingSessionService _codingSessionService;
    private readonly CodingGoalService _codingGoalService;

    public CodingGoalProgressService(CodingSessionService codingSessionService, CodingGoalService codingGoalService)
    {
        _codingSessionService = codingSessionService;
        _codingGoalService = codingGoalService;
    }

    public async Task<string> GetCodingGoalProgressAsync()
    {
        var codingGoal = await _codingGoalService.GetCodingGoalAsync();
        if (codingGoal == null || codingGoal.WeeklyDurationInHours == 0)
        {
            return "please set a coding goal for motivation.";
        }

        // Lets say a week is Monday - Sunday.
        var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
        var endOfWeek = startOfWeek.AddDays(7).AddTicks(-1);

        var codingSessions = await _codingSessionService.GetCodingSessionsAsync();

        // Get the total duration spent this week.
        var filteredCodingSessions = codingSessions.Where(w => w.StartTime >= startOfWeek && w.EndTime <= endOfWeek);
        double totalDuration = filteredCodingSessions.Sum(x => x.Duration);

        // Get difference.
        double difference = codingGoal.WeeklyDurationInHours - totalDuration;

        // Goal Reached?
        if (difference <= 0)
        {
            return $"you have reached your weekly coding goal. Well done!";
        }
        else if (difference < 0)
        {
            return $"you are {Math.Abs(difference):F2} hours over your weekly coding goal. Well done!";
        }

        // Get required.
        var daysRemaining = (endOfWeek.Date - DateTime.Today).Days;
        var averagePerDay = difference / daysRemaining;

        return $"you require {difference:F2} more hours to reach your weekly coding goal. Which is {averagePerDay:F2} hours per day. You can do it!";
    }
}
