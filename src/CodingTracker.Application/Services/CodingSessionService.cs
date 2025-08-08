using CodingTracker.Application.Models;
using CodingTracker.Data.Managers;

namespace CodingTracker.Application.Services;

/// <summary>
/// Business logic class for the Coding Session object and the database entity.
/// </summary>
public class CodingSessionService
{
    private readonly SqliteDataManager _dataManager;

    public CodingSessionService(SqliteDataManager dataManager)
    {
        _dataManager = dataManager;
    }

    public void AddCodingSession(DateTime startTime, DateTime endTime)
    {
        _dataManager.AddCodingSession(startTime, endTime);
    }

    public void DeleteCodingSession(int codingSessionId)
    {
        _dataManager.DeleteCodingSession(codingSessionId);
    }

    public List<CodingSession> GetCodingSessions()
    {
        return _dataManager.GetCodingSessions().Select(x => new CodingSession(x)).ToList();
    }

    /// <summary>
    /// Seeds the database with 10 random CodingSession entries.
    /// </summary>
    public void SeedDatabase()
    {
        if (_dataManager.GetCodingSessions().Count == 0)
        {
            for (int i = 10; i > 0; i--)
            {
                var endDateTime = DateTime.Now.AddDays(-i).AddMinutes(-Random.Shared.Next(0, 120));
                var startDateTime = endDateTime.AddMinutes(-Random.Shared.Next(1, 120));
                _dataManager.AddCodingSession(startDateTime, endDateTime);
            }
        }
    }

    public void SetCodingSession(int codingSessionId, DateTime startTime, DateTime endTime)
    {
        _dataManager.SetCodingSession(codingSessionId, startTime, endTime);
    }
}
