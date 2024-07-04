using CodingTracker.Data.Extensions;
using System.Data;

namespace CodingTracker.Data.Entities;

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
