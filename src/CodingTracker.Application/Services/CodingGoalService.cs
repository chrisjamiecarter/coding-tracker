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

    public CodingGoal GetCodingGoal()
    {
        var entity = _dataManager.GetCodingGoal();
        return new CodingGoal(entity);
    }

    public void SetCodingGoal(double weeklyDurationInHours)
    {
        _dataManager.SetCodingGoal(weeklyDurationInHours);
    }
}
