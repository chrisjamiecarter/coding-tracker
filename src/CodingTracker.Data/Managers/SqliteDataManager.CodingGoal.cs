using CodingTracker.Data.Entities;
using Dapper;
using System.Data.SQLite;

namespace CodingTracker.Data.Managers;

/// <summary>
/// Partial class for CodingGoal entity specific data manager methods.
/// Note: There will only ever be 1 record in the database table.
/// </summary>
public partial class SqliteDataManager
{
    private static readonly string AddCodingGoalQuery =
        @"
        INSERT OR IGNORE INTO CodingGoal 
        (
             Id
            ,WeeklyDurationInHours
        )
        VALUES
        (
             1
            ,0
        )
        ;";

    private static readonly string GetCodingGoalQuery =
        @"
        SELECT
            *
        FROM
            CodingGoal
        WHERE
            Id = 1
        ;";

    private static readonly string SetCodingGoalQuery =
        @"
        UPDATE
            CodingGoal
        SET
            WeeklyDurationInHours = $WeeklyDurationInHours
        WHERE
            Id = 1
        ;";

    public async Task AddCodingGoalAsync()
    {
        using var connection = new SQLiteConnection(ConnectionString);
        await connection.ExecuteAsync(AddCodingGoalQuery);
    }

    public async Task<CodingGoalEntity> GetCodingGoalAsync()
    {
        using var connection = new SQLiteConnection(ConnectionString);
        return await connection.QuerySingleAsync<CodingGoalEntity>(GetCodingGoalQuery);
    }

    public async Task SetCodingGoalAsync(double weeklyDurationInHours)
    {
        var parameters = new
        {
            WeeklyDurationInHours = weeklyDurationInHours
        };

        using var connection = new SQLiteConnection(ConnectionString);
        await connection.ExecuteAsync(SetCodingGoalQuery, parameters);
    }
}
