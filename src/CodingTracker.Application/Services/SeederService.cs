using CodingTracker.Data.Managers;

namespace CodingTracker.Application.Services;

/// <summary>
/// Service to seed the database with 10 random entries.
/// </summary>
public sealed class SeederService
{
    private const int SeedRecords = 10;
    
    private SqliteDataManager _dataManager;

    public SeederService(SqliteDataManager dataManager)
    {
        _dataManager = dataManager;
    }

    public async Task SeedDatabaseAsync()
    {
        if (!_dataManager.GetCodingSessions().Any())
        {
            for (int i = SeedRecords; i > 0; i--)
            {
                var endDateTime = DateTime.Now.AddDays(-i).AddMinutes(-Random.Shared.Next(0, 120));
                var startDateTime = endDateTime.AddMinutes(-Random.Shared.Next(1, 120));
                _dataManager.AddCodingSession(startDateTime, endDateTime);
            }
        }

        await Task.CompletedTask;
    }
}
