namespace CodingTracker.Models;

/// <summary>
/// Coding session data transformation object.
/// </summary>
public class CodingSessionReport
{
    private string _dateTimeFormat;

    public CodingSessionReport(DateTime start, DateTime end, double duration, string dateTimeFormat)
    {
        StartDateTime = start;
        EndDateTime = end;
        Duration = duration;
        _dateTimeFormat = dateTimeFormat;
    }

    public DateTime StartDateTime { get; init; }
    
    public string StartDateTimeString => StartDateTime.ToString(_dateTimeFormat);

    public DateTime EndDateTime { get; init; }
    
    public string EndDateTimeString => EndDateTime.ToString(_dateTimeFormat);

    public double Duration { get; init; }

    public string DurationString => Duration.ToString("F2");
}
