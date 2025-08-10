using System.ComponentModel;
using CodingTracker.Application.Constants;
using CodingTracker.Application.Models;
using CodingTracker.Application.Services;
using CodingTracker.ConsoleApp.Enums;
using CodingTracker.ConsoleApp.Extensions;
using Spectre.Console;

namespace CodingTracker.ConsoleApp.Views;

/// <summary>
/// The main menu page of the application.
/// </summary>
internal class MainMenuPage : BasePage
{
    private const string PageTitle = "Main Menu";

    private readonly CodingSessionService _codingSessionService;
    private readonly CodingGoalService _codingGoalService;
    private readonly CodingGoalProgressService _codingGoalProgressService;

    public MainMenuPage(CodingSessionService codingSessionService, CodingGoalService codingGoalService, CodingGoalProgressService codingGoalProgressService)
    {
        _codingSessionService = codingSessionService;
        _codingGoalService = codingGoalService;
        _codingGoalProgressService = codingGoalProgressService;
    }

    private enum MainMenuPageChoices
    {
        [Description("Start live coding session")]
        LiveCodingSession = 0,

        [Description("View coding sessions report")]
        ViewCodingSessionsReport = 1,

        [Description("Filter coding sessions report")]
        FilterCodingSessionsReport = 2,

        [Description("Create coding session record")]
        CreateCodingSession = 3,

        [Description("Update coding session record")]
        UpdateCodingSession = 4,

        [Description("Delete coding session record")]
        DeleteCodingSession = 5,

        [Description("Set coding goal")]
        SetCodingGoal = 6,

        [Description("Close application")]
        CloseApplication = 7,
    }

    internal async Task ShowAsync()
    {
        var status = PageStatus.Opened;

        while (status != PageStatus.Closed)
        {
            WriteHeader(PageTitle);

            await WriteCodingGoalProgressAsync();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuPageChoices>()
                .Title(PromptTitle)
                .AddChoices(Enum.GetValues<MainMenuPageChoices>())
                .UseConverter(c => c.GetDescription())
                );

            status = await PerformSelectedChoiceAsync(choice);
        }
    }

    private async Task CreateCodingSessionAsync()
    {
        // Get required data.
        var codingSession = CreateCodingSessionPage.Show();

        // If nothing is returned, user has opted to not commit.
        if (codingSession == null)
        {
            return;
        }

        // Commit to database.
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("Adding coding session. Please wait...", async ctx =>
            {
                await _codingSessionService.AddCodingSessionAsync(codingSession.StartTime, codingSession.EndTime);
            });

        // Display output.
        MessagePage.Show("Create Coding Session", "Coding session created successfully.");
    }

    private async Task DeleteCodingSessionAsync()
    {
        // Get required data.
        var codingSessions = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("Getting coding sessions. Please wait...", async ctx =>
            {
                return await _codingSessionService.GetCodingSessionsAsync();
            });

        // Get coding session to be deleted.
        var codingSession = DeleteCodingSessionPage.Show(codingSessions);

        // If nothing is returned, user has opted to not commit.
        if (codingSession == null)
        {
            return;
        }

        // Commit to database.
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("Deleting coding session. Please wait...", async ctx =>
            {
                await _codingSessionService.DeleteCodingSessionAsync(codingSession.Id);
            });

        // Display output.
        MessagePage.Show("Delete Coding Session", "Coding session deleted successfully.");
    }

    private async Task FilterCodingSessionsReportAsync()
    {
        // Get raw data.
        var data = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("Getting coding sessions. Please wait...", async ctx =>
            {
                return await _codingSessionService.GetCodingSessionsAsync();
            });

        // Get filter.
        var filter = ReportFilterPage.Show();

        // If nothing is returned, user has opted to not commit.
        if (filter == null)
        {
            return;
        }

        // Apply filter.
        var filteredData = filter.Apply(data);

        // Configure table data.
        string tableTitle = $"Coding Session Report {(filter.StartDate.HasValue && filter.EndDate.HasValue ? $"for range: {filter.StartDate:yyyy-MM-dd} - {filter.EndDate:yyyy-MM-dd}" : "")}";
        var table = new Table
        {
            Title = new TableTitle(tableTitle)
        };
        table.AddColumn("Start");
        table.AddColumn("End");
        table.AddColumn("Duration");

        foreach (var x in filteredData)
        {
            table.AddRow(x.StartDateTimeString, x.EndDateTimeString, x.DurationString);
        }

        table.Caption = new TableTitle(filteredData.Any()
            ? $"Total: {filteredData.Sum(x => x.Duration):F2}{Environment.NewLine}Average: {filteredData.Average(x => x.Duration):F2}"
            : "No coding sessions found.");

        // Fill up window.
        table.Expand();

        // Display report.
        MessagePage.Show("Coding Session Report", table);
    }

    private async Task LiveCodingSessionAsync()
    {
        // Get required data.
        var codingSession = LiveCodingSessionPage.Show();

        // Commit to database.
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("Adding coding session. Please wait...", async ctx =>
            {
                await _codingSessionService.AddCodingSessionAsync(codingSession.StartTime, codingSession.EndTime);
            });

        // Display output.
        MessagePage.Show("Live Coding Session", "Coding session created successfully.");
    }

    private async Task<PageStatus> PerformSelectedChoiceAsync(MainMenuPageChoices choice)
    {
        switch (choice)
        {
            case MainMenuPageChoices.CloseApplication:

                return PageStatus.Closed;

            case MainMenuPageChoices.LiveCodingSession:

                await LiveCodingSessionAsync();
                break;

            case MainMenuPageChoices.ViewCodingSessionsReport:

                await ViewCodingSessionsReportAsync();
                break;

            case MainMenuPageChoices.FilterCodingSessionsReport:

                await FilterCodingSessionsReportAsync();
                break;

            case MainMenuPageChoices.CreateCodingSession:

                await CreateCodingSessionAsync();
                break;

            case MainMenuPageChoices.UpdateCodingSession:

                await UpdateCodingSessionAsync();
                break;

            case MainMenuPageChoices.DeleteCodingSession:

                await DeleteCodingSessionAsync();
                break;

            case MainMenuPageChoices.SetCodingGoal:

                await SetCodingGoalAsync();
                break;

            default:

                break;
        }

        return PageStatus.Opened;
    }

    private async Task SetCodingGoalAsync()
    {
        // Get coding goal.
        var codingGoal = SetCodingGoalPage.Show();

        // If nothing is returned, user has opted to not commit.
        if (codingGoal == null)
        {
            return;
        }

        // Commit to database.
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("Updating coding goal. Please wait...", async ctx =>
            {
                await _codingGoalService.SetCodingGoalAsync(codingGoal.WeeklyDurationInHours);
            });

        // Display output.
        MessagePage.Show("Set Coding Goal", "Coding goal set successfully.");
    }

    private async Task UpdateCodingSessionAsync()
    {
        // Get required data.
        var codingSessions = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("Getting coding sessions. Please wait...", async ctx =>
            {
                return await _codingSessionService.GetCodingSessionsAsync();
            });

        // Get updated coding session.
        var codingSession = UpdateCodingSessionPage.Show(codingSessions);

        // If nothing is returned, user has opted to not commit.
        if (codingSession == null)
        {
            return;
        }

        // Commit to database.
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("Updating coding sessions. Please wait...", async ctx =>
            {
                await _codingSessionService.UpdateCodingSessionAsync(codingSession.Id, codingSession.StartTime, codingSession.EndTime);
            });

        // Display output.
        MessagePage.Show("Update Coding Session", "Coding session updated successfully.");
    }

    private async Task ViewCodingSessionsReportAsync()
    {
        // Get raw data.
        var data = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("Getting coding sessions. Please wait...", async ctx =>
            {
                return await _codingSessionService.GetCodingSessionsAsync();
            });

        // Configure table data.
        var table = new Table
        {
            Title = new TableTitle("Coding Session Report")
        };
        table.AddColumn("ID");
        table.AddColumn("Start");
        table.AddColumn("End");
        table.AddColumn("Duration");

        foreach (var x in data)
        {
            table.AddRow(x.Id.ToString(), x.StartTime.ToString(StringFormat.DateTime), x.EndTime.ToString(StringFormat.DateTime), x.Duration.ToString("F2"));
        }

        table.Caption = new TableTitle(data.Count > 0
            ? $"Total: {data.Sum(x => x.Duration):F2}{Environment.NewLine}Average: {data.Average(x => x.Duration):F2}"
            : "No coding sessions found.");

        // Fill up window.
        table.Expand();

        // Display report.
        MessagePage.Show("Coding Session Report", table);
    }

    private async Task WriteCodingGoalProgressAsync()
    {
        var progress = await _codingGoalProgressService.GetCodingGoalProgressAsync();
        AnsiConsole.WriteLine($"Welcome, {progress}");
        AnsiConsole.WriteLine();
    }
}
