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

    public void SetCodingSession(int codingSessionId, DateTime startTime, DateTime endTime)
    {
        _dataManager.SetCodingSession(codingSessionId, startTime, endTime);
    }
}
