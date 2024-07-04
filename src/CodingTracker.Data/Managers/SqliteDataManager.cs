﻿using Dapper;
using System.Data.SQLite;

namespace CodingTracker.Data.Managers
{
    /// <summary>
    /// Partial class for non entity specific data manager methods.
    /// </summary>
    public partial class SqliteDataManager
    {
        #region Constants

        private static readonly string CreateTableCodingSessionQuery =
            @"
            CREATE TABLE IF NOT EXISTS CodingSession
            (
                 Id INTEGER PRIMARY KEY AUTOINCREMENT
                ,StartTime TEXT NOT NULL
                ,EndTime TEXT NOT NULL
                ,Duration REAL NOT NULL
            )
            ;";

        #endregion
        #region Properties

        public string ConnectionString { get; init; }

        #endregion
        #region Constructor

        public SqliteDataManager(string connectionString)
        {
            ConnectionString = connectionString;

            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            var filePath = connection.FileName;
            connection.Close();
            if(File.Exists(filePath)) File.Delete(filePath);

            Initialise();
        }

        #endregion
        #region Methods: Private - Initialise

        private void Initialise()
        {
            // Put all table creation methods here, in dependency order.
            CreateTableCodingSession();
        }

        #endregion
        #region Methods: Private - Create

        private void CreateTableCodingSession()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            connection.Execute(CreateTableCodingSessionQuery);
        }

        #endregion
    }
}
