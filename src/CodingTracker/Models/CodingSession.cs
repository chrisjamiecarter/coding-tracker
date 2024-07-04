using CodingTracker.Data.Entities;

namespace CodingTracker.Models;

public class CodingSession
{
    public CodingSession(CodingSessionEntity entity)
    {
        Id = entity.Id;
        StartTime = entity.StartTime;
        EndTime = entity.EndTime;
        Duration = entity.Duration;
    }

    public int Id { get; init; }

    public DateTime StartTime { get; init; }

    public DateTime EndTime { get; init; }

    public double Duration { get; init; }
}
