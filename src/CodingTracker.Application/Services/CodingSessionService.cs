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

    public async Task AddCodingSessionAsync(DateTime startTime, DateTime endTime)
    {
        await _dataManager.AddCodingSessionAsync(startTime, endTime);
    }

    public async Task DeleteCodingSessionAsync(int codingSessionId)
    {
        await _dataManager.DeleteCodingSessionAsync(codingSessionId);
    }

    public async Task<List<CodingSession>> GetCodingSessionsAsync()
    {
        var codingSessions = await _dataManager.GetCodingSessionsAsync();
        return [.. codingSessions.Select(x => new CodingSession(x))];
    }

    public async Task UpdateCodingSessionAsync(int codingSessionId, DateTime startTime, DateTime endTime)
    {
        await _dataManager.UpdateCodingSessionAsync(codingSessionId, startTime, endTime);
    }
}
