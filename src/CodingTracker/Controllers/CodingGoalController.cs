using CodingTracker.Data.Managers;
using CodingTracker.Models;

namespace CodingTracker.Controllers;

/// <summary>
/// Controller class for the Coding Goal object.
/// </summary>
public class CodingGoalController
{
    #region Fields

    private readonly SqliteDataManager _dataManager;

    #endregion
    #region Constructors

    public CodingGoalController(string databaseConnectionString)
    {
        _dataManager = new SqliteDataManager(databaseConnectionString);
    }

    #endregion
    #region Methods: Public

    public CodingGoal GetCodingGoal()
    {
        return new(_dataManager.GetCodingGoal());
    }

    public void SetCodingGoal(double weeklyDurationInHours)
    {
        _dataManager.SetCodingGoal(weeklyDurationInHours);
    }

    #endregion
}
