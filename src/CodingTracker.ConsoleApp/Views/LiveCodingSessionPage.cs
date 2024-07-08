using CodingTracker.Models;
using Spectre.Console;
using System;
using System.Diagnostics;

namespace CodingTracker.ConsoleApp.Views;

internal class LiveCodingSessionPage : BasePage
{
    #region Constants

    private const string PageTitle = "Live Coding Session";

    #endregion
    #region Methods: Internal

    internal static CodingSession Show()
    {
        Stopwatch stopwatch = new Stopwatch();

        AnsiConsole.Clear();
        WriteHeader(PageTitle);
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("Press any key to start the session...");
        Console.ReadKey();

        // Start timer.
        DateTime start = DateTime.Now;
        stopwatch.Start();

        AnsiConsole.Clear();
        WriteHeader(PageTitle);
        var stopwatchDisplayRow = Console.CursorTop;
        AnsiConsole.WriteLine(@$"Current coding session duration: {stopwatch.Elapsed:hh\:mm\:ss}");
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("Press any key to stop the session...");

        var lastUpdate = stopwatch.Elapsed;
        while (!Console.KeyAvailable)
        {
            // Only update every 500 milliseconds.
            if (stopwatch.Elapsed > lastUpdate.Add(TimeSpan.FromMilliseconds(500)))
            {
                lastUpdate = stopwatch.Elapsed;
                Console.SetCursorPosition(0, stopwatchDisplayRow);
                AnsiConsole.WriteLine(@$"Current coding session duration: {stopwatch.Elapsed:hh\:mm\:ss}");
            }
        }

        // Stop timer.
        stopwatch.Stop();
        DateTime end = DateTime.Now;

        return new CodingSession(start, end);
    }

    #endregion
}
