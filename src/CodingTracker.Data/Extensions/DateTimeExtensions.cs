using System.Data.SQLite;

namespace CodingTracker.Data.Extensions;

/// <summary>
/// 
/// </summary>
public static class DateTimeExtensions
{
    private static readonly string SqliteDateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

    public static string ToSqliteDateTimeString(this DateTime dateTime)
    {
        return dateTime.ToString(SqliteDateTimeFormat);
    }
}
