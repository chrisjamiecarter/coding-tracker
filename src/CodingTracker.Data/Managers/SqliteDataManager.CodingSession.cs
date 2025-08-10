using CodingTracker.Data.Entities;
using CodingTracker.Data.Extensions;
using Dapper;
using System.Data.SQLite;

namespace CodingTracker.Data.Managers;

/// <summary>
/// Partial class for CodingSession entity specific data manager methods.
/// </summary>
public partial class SqliteDataManager
{
    private static readonly string AddCodingSessionQuery =
        @"
        INSERT INTO CodingSession
        (
             StartTime
            ,EndTime
        )
        VALUES
        (
             $StartTime
            ,$EndTime
        )
        ;";

    private static readonly string DeleteCodingSessionQuery =
        @"
        DELETE FROM
            CodingSession
        WHERE
            Id = $Id
        ;";

    private static readonly string GetCodingSessionQuery =
        @"
        SELECT
            *
        FROM
            CodingSession
        WHERE
            Id = $Id
        ;";

    private static readonly string GetCodingSessionsQuery =
        @"
        SELECT
            *
        FROM
            CodingSession
        ;";

    private static readonly string UpdateCodingSessionQuery =
        @"
        UPDATE
            CodingSession
        SET
             StartTime = $StartTime
            ,EndTime = $EndTime
        WHERE
            Id = $Id
        ;";

    public async Task AddCodingSessionAsync(DateTime start, DateTime end)
    {
        var parameters = new
        { 
            StartTime = start.ToSqliteDateTimeString(),
            EndTime = end.ToSqliteDateTimeString(),
        };

        using var connection = new SQLiteConnection(ConnectionString);
        await connection.ExecuteAsync(AddCodingSessionQuery, parameters);
    }

    public async Task DeleteCodingSessionAsync(int id)
    {
        var parameters = new
        {
            Id = id
        };

        using var connection = new SQLiteConnection(ConnectionString);
        await connection.ExecuteAsync(DeleteCodingSessionQuery, parameters);
    }

    public async Task<CodingSessionEntity> GetCodingSessionAsync(int id)
    {
        var parameters = new
        {
            Id = id
        };

        using var connection = new SQLiteConnection(ConnectionString);
        return await connection.QuerySingleAsync<CodingSessionEntity>(GetCodingSessionQuery, parameters);
    }

    public async Task<IReadOnlyList<CodingSessionEntity>> GetCodingSessionsAsync()
    {
        using var connection = new SQLiteConnection(ConnectionString);
        var output = await connection.QueryAsync<CodingSessionEntity>(GetCodingSessionsQuery);
        return output.AsList();
    }

    public async Task UpdateCodingSessionAsync(int id, DateTime start, DateTime end)
    {
        var parameters = new
        {
            Id = id,
            StartTime = start.ToSqliteDateTimeString(),
            EndTime = end.ToSqliteDateTimeString(),
        };

        using var connection = new SQLiteConnection(ConnectionString);
        await connection.ExecuteAsync(UpdateCodingSessionQuery, parameters);
    }
}
