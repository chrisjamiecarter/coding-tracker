namespace CodingTracker.Data.Entities;

/// <summary>
/// Represents a row in the CodingSession table.
/// </summary>
public class CodingSessionEntity
{
    public CodingSessionEntity()
    {
        // Required for Dapper.
    }

    public int Id { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public double Duration { get; init; }
}
