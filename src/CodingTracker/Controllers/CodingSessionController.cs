using CodingTracker.Data.Managers;
using CodingTracker.Models;

namespace CodingTracker.Controllers;

/// <summary>
/// Controller class for the Coding Session object.
/// </summary>
public class CodingSessionController
{
    #region Fields

    private readonly SqliteDataManager _dataManager;

    #endregion
    #region Constructors

    public CodingSessionController(string databaseConnectionString)
    {
        _dataManager = new SqliteDataManager(databaseConnectionString);
    }

    #endregion
    #region Methods: Public

    public void AddCodingSession(DateTime startTime, DateTime endTime)
    {
        var duration = CalculateDuration(startTime, endTime);
        _dataManager.AddCodingSession(startTime, endTime, duration);
    }

    public List<CodingSession> GetCodingSessions()
    {
        return _dataManager.GetCodingSessions().Select(x => new CodingSession(x)).ToList();
    }

    public void SetCodingSession(int codingSessionId, DateTime startTime, DateTime endTime)
    {
        var duration = CalculateDuration(startTime, endTime);
        _dataManager.SetCodingSession(codingSessionId, startTime, endTime, duration);
    }

    public void DeleteCodingSession(int codingSessionId)
    {
        _dataManager.DeleteCodingSession(codingSessionId);
    }

    public void SeedDatabase()
    {
        if (_dataManager.GetCodingSessions().Count == 0)
        {
            for (int i = 100; i > 0; i--)
            {
                var endDateTime = DateTime.Now.AddDays(-i).AddMinutes(-Random.Shared.Next(0, 120));
                var startDateTime = endDateTime.AddMinutes(-Random.Shared.Next(1, 120));
                var duration = (endDateTime - startDateTime).TotalHours;
                _dataManager.AddCodingSession(startDateTime, endDateTime, duration);
            }            
        }
    }

    #endregion
    #region Methods: Private

    // Requirement: Do not let user enter duration. Must be calculated in the CodingSessionController.
    private static double CalculateDuration(DateTime startTime, DateTime endTime)
    {
        return (endTime - startTime).TotalHours;
    }

    #endregion
}
