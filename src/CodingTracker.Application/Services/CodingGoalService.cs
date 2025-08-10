using CodingTracker.Application.Models;
using CodingTracker.Data.Managers;

namespace CodingTracker.Application.Services;

/// <summary>
/// Business logic class for the Coding Goal object and the database entity.
/// </summary>
public class CodingGoalService
{
    private readonly SqliteDataManager _dataManager;

    public CodingGoalService(SqliteDataManager dataManager)
    {
        _dataManager = dataManager;
    }

    public async Task<CodingGoal> GetCodingGoalAsync()
    {
        var entity = await _dataManager.GetCodingGoalAsync();
        return new CodingGoal(entity);
    }

    public async Task SetCodingGoalAsync(double weeklyDurationInHours)
    {
        await _dataManager.SetCodingGoalAsync(weeklyDurationInHours);
    }
}
