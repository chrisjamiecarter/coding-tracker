using CodingTracker.Data.Entities;
using CodingTracker.Data.Extensions;
using Dapper;
using System.Data.SQLite;

namespace CodingTracker.Data.Managers
{
    /// <summary>
    /// Partial class for CodingSession entity specific data manager methods.
    /// </summary>
    public partial class SqliteDataManager
    {
        #region Constants

        private static readonly string AddCodingSessionQuery =
            @"
            INSERT INTO CodingSession
            (
                 StartTime
                ,EndTime
                ,Duration
            )
            VALUES
            (
                 $StartTime
                ,$EndTime
                ,$Duration
            )
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

        private static readonly string SetCodingSessionQuery =
            @"
            UPDATE
                CodingSession
            SET
                 StartTime = $StartTime
                ,EndTime = $EndTime
                ,Duration = $Duration
            WHERE
                Id = $Id
            ;";

        private static readonly string DeleteCodingSessionQuery =
            @"
            DELETE FROM
                CodingSession
            WHERE
                Id = $Id
            ;";

        #endregion
        #region Methods: Public
        #region Methods: Create

        public void AddCodingSession(DateTime start, DateTime end, double duration)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            connection.Execute(AddCodingSessionQuery, new { StartTime = start.ToSqliteDateTimeString(), EndTime = end.ToSqliteDateTimeString(), Duration = duration});
        }

        #endregion
        #region Methods: Read

        public CodingSessionEntity GetCodingSession(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection.QuerySingle<CodingSessionEntity>(GetCodingSessionQuery, new { Id = id });
        }

        public IReadOnlyList<CodingSessionEntity> GetCodingSessions()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection.Query<CodingSessionEntity>(GetCodingSessionsQuery).ToList();
        }

        #endregion
        #region Methods: Update

        public void SetCodingSession(int id, DateTime start, DateTime end, double duration)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            connection.Execute(SetCodingSessionQuery, new { Id = id, StartTime = start.ToSqliteDateTimeString(), EndTime = end.ToSqliteDateTimeString(), Duration = duration});
        }

        #endregion
        #region Methods: Delete

        public void DeleteCodingSession(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            connection.Execute(DeleteCodingSessionQuery, new { Id = id });
        }

        #endregion
        #endregion
    }
}
