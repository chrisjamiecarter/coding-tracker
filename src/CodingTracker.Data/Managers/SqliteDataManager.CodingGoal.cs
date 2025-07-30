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

    public void AddCodingGoal()
    {
        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();
        connection.Execute(AddCodingGoalQuery);
    }

    public CodingGoalEntity GetCodingGoal()
    {
        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();
        return connection.QuerySingle<CodingGoalEntity>(GetCodingGoalQuery);
    }

    public void SetCodingGoal(double weeklyDurationInHours)
    {
        var parameters = new
        {
            WeeklyDurationInHours = weeklyDurationInHours
        };

        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();
        connection.Execute(SetCodingGoalQuery, parameters);
    }
}
