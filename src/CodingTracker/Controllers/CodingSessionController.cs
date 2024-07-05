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

    public void AddCodingSession(CodingSession codingSession)
    {
        _dataManager.AddCodingSession(codingSession.StartTime, codingSession.EndTime, codingSession.Duration);
    }

    public IReadOnlyList<CodingSession> GetCodingSessions()
    {
        return _dataManager.GetCodingSessions().Select(x => new CodingSession(x)).ToList();
    }

    public void SeedDatabase()
    {
        if (_dataManager.GetCodingSessions().Count == 0)
        {
            var startDateTime = DateTime.Now.AddMinutes(-123);
            var endDateTime = DateTime.Now;
            var duration = (endDateTime - startDateTime).TotalHours;
            _dataManager.AddCodingSession(startDateTime, endDateTime, duration);
        }
    }

    #endregion
}
