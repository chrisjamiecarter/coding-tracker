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

    public List<CodingSession> GetCodingSessions()
    {
        return _dataManager.GetCodingSessions().Select(x => new CodingSession(x)).ToList();
    }

    public void SetCodingSession(CodingSession codingSession)
    {
        _dataManager.SetCodingSession(codingSession.Id, codingSession.StartTime, codingSession.EndTime, codingSession.Duration);
    }

    public void DeleteCodingSession(CodingSession codingSession)
    {
        _dataManager.DeleteCodingSession(codingSession.Id);
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
}
