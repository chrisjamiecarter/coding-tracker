using Dapper;
using System.Data.SQLite;

namespace CodingTracker.Data.Managers;

/// <summary>
/// Partial class for non entity specific data manager methods.
/// </summary>
public partial class SqliteDataManager
{
    private static readonly string CreateTableCodingSessionQuery =
        @"
        CREATE TABLE IF NOT EXISTS CodingSession
        (
             Id INTEGER PRIMARY KEY AUTOINCREMENT
            ,StartTime TEXT NOT NULL
            ,EndTime TEXT NOT NULL
        )
        ;";

    private static readonly string CreateTableCodingGoalQuery =
        @"
        CREATE TABLE IF NOT EXISTS CodingGoal
        (
             Id INTEGER PRIMARY KEY AUTOINCREMENT
            ,WeeklyDurationInHours REAL NOT NULL
        )
        ;";

    public SqliteDataManager(string connectionString)
    {
        ConnectionString = connectionString;

        InitialiseAsync().GetAwaiter().GetResult();
    }

    public string ConnectionString { get; init; }

    private async Task CreateTableCodingSessionAsync()
    {
        using var connection = new SQLiteConnection(ConnectionString);
        await connection.ExecuteAsync(CreateTableCodingSessionQuery);
    }

    private async Task CreateTableCodingGoalAsync()
    {
        using var connection = new SQLiteConnection(ConnectionString);
        await connection.ExecuteAsync(CreateTableCodingGoalQuery);
    }

    private async Task InitialiseAsync()
    {
        // Put all table creation methods here, in dependency order.
        await CreateTableCodingSessionAsync();
        await CreateTableCodingGoalAsync();
        await AddCodingGoalAsync();
    }
}
