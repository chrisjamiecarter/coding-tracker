using CodingTracker.Application.Models;
using CodingTracker.Data.Managers;

namespace CodingTracker.Application.Controllers;

/// <summary>
/// Controller class for the Coding Goal object and the database entity.
/// </summary>
public class CodingGoalController
{
    private readonly SqliteDataManager _dataManager;

    public CodingGoalController(SqliteDataManager dataManager)
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
