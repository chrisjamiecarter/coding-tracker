﻿using CodingTracker.ConsoleApp.Constants;
using CodingTracker.ConsoleApp.Enums;
using CodingTracker.ConsoleApp.Models;
using CodingTracker.Constants;
using CodingTracker.Controllers;
using CodingTracker.Enums;
using CodingTracker.Models;
using Spectre.Console;
using System.ComponentModel;
using System.Linq;

namespace CodingTracker.ConsoleApp.Views;

internal class MainMenuPage : BasePage
{
    #region Constants

    private const string PageTitle = "Main Menu";

    #endregion
    #region Fields

    private readonly CodingSessionController _codingSessionController;

    #endregion
    #region Constructors

    public MainMenuPage(CodingSessionController codingSessionController)
    {
        _codingSessionController = codingSessionController;
    }

    #endregion
    #region Properties

    internal static IEnumerable<PromptChoice> PageOptions
    {
        get
        {
            return
            [
                new(5, "Start live coding session"),
                new(1, "View coding sessions report"),
                new(6, "Filter coding sessions report"),
                new(2, "Create coding session record"),
                new(3, "Update coding session record"),
                new(4, "Delete coding session record"),
                new(0, "Close application")
            ];
        }
    }

    #endregion
    #region Methods

    internal void Show()
    {
        var status = PageStatus.Opened;

        while (status != PageStatus.Closed)
        {
            AnsiConsole.Clear();

            WriteHeader(PageTitle);

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<PromptChoice>()
                .Title(Prompt.Title)
                .AddChoices(PageOptions)
                .UseConverter(c => c.Name!)
                );

            status = PerformOption(option);
        }
    }

    private PageStatus PerformOption(PromptChoice option)
    {
        switch (option.Id)
        {
            case 0:

                // Close application.
                return PageStatus.Closed;

            case 1:
                // View coding sessions report.
                ViewCodingSessionsReport();                
                break;

            case 2:

                // Create coding session record.
                CreateCodingSession();
                break;

            case 3:

                // Update coding session record.
                UpdateCodingSession();
                break;

            case 4:

                // Delete coding session record.
                DeleteCodingSession();
                break;

            case 5:

                // Delete coding session record.
                LiveCodingSession();
                break;

            case 6:

                // Filter coding sessions report.
                FilterCodingSessionsReport();
                break;

            default:

                // Do nothing, but remain on this page.
                break;
        }

        return PageStatus.Opened;
    }

    private void ViewCodingSessionsReport()
    {
        // Get raw data.
        var data = _codingSessionController.GetCodingSessions();

        // Configure table data.
        var table = new Table();
                
        table.Title = new TableTitle("Coding Session Report");
        table.AddColumn("ID");
        table.AddColumn("Start");
        table.AddColumn("End");
        table.AddColumn("Duration");

        foreach (var x in data)
        {
            table.AddRow(x.Id.ToString(), x.StartTime.ToString(StringFormat.DateTime), x.EndTime.ToString(StringFormat.DateTime), x.Duration.ToString("F2"));
        }
        if (data.Count > 0)
        {
            table.Caption = new TableTitle($"Total: {data.Sum(x => x.Duration):F2}{Environment.NewLine}Average: {data.Average(x => x.Duration):F2}");
        }
        else
        {
            table.Caption = new TableTitle("No coding sessions found.");
        }

        // Fill up window.
        table.Expand();

        // Display report.
        MessagePage.Show("Coding Session Report", table);
    }

    private void FilterCodingSessionsReport()
    {
        // Get raw data.
        var data = _codingSessionController.GetCodingSessions();

        // Get filter.
        var filter = ReportFilterPage.Show();
        if (filter == null)
        {
            // If nothing is returned, user has opted to not commit.
            return;
        }

        // Apply filter.
        var filteredData = filter.Apply(data);

        // Configure table data.
        var table = new Table();
        string tableTitle = $"Coding Session Report (Type: {filter.Type})";
        tableTitle += filter.StartDate.HasValue && filter.EndDate.HasValue ? $" Period: {filter.StartDate:yyyy-MM-dd} - {filter.EndDate:yyyy-MM-dd}" : "";
        table.Title = new TableTitle(tableTitle);
        table.AddColumn("Start");
        table.AddColumn("End");
        table.AddColumn("Duration");

        foreach (var x in filteredData)
        {
            //table.AddRow(x.Id.ToString(), x.StartTime.ToString(StringFormat.DateTime), x.EndTime.ToString(StringFormat.DateTime), x.Duration.ToString("F2"));
            table.AddRow(x.StartDateTimeString, x.EndDateTimeString, x.DurationString);
        }
        if (filteredData.Any())
        {
            table.Caption = new TableTitle($"Total: {filteredData.Sum(x => x.Duration):F2}{Environment.NewLine}Average: {filteredData.Average(x => x.Duration):F2}");
        }
        else
        {
            table.Caption = new TableTitle("No coding sessions found.");
        }

        // Fill up window.
        table.Expand();

        // Display report.
        MessagePage.Show("Coding Session Report", table);
    }

    private void CreateCodingSession()
    {
        // Get required data.
        var codingSession = CreateCodingSessionPage.Show();
        if (codingSession == null)
        {
            // If nothing is returned, user has opted to not commit.
            return;
        }

        // Commit to database.
        _codingSessionController.AddCodingSession(codingSession.StartTime, codingSession.EndTime);

        // Display output.
        MessagePage.Show("Create Coding Session", $"Coding session created successfully.");
    }

    private void UpdateCodingSession()
    {
        // Get required data.
        var codingSessions = _codingSessionController.GetCodingSessions();

        // Get updated coding session.
        var codingSession = UpdateCodingSessionPage.Show(codingSessions);
        if (codingSession == null)
        {
            // If nothing is returned, user has opted to not commit.
            return;
        }

        // Commit to database.
        _codingSessionController.SetCodingSession(codingSession.Id, codingSession.StartTime, codingSession.EndTime);

        // Display output.
        MessagePage.Show("Update Coding Session", $"Coding session updated successfully.");
    }

    private void DeleteCodingSession()
    {
        // Get required data.
        var codingSessions = _codingSessionController.GetCodingSessions();

        // Get coding session to be deleted.
        var codingSession = DeleteCodingSessionPage.Show(codingSessions);
        if (codingSession == null)
        {
            // If nothing is returned, user has opted to not commit.
            return;
        }

        // Commit to database.
        _codingSessionController.DeleteCodingSession(codingSession.Id);

        // Display output.
        MessagePage.Show("Delete Coding Session", $"Coding session deleted successfully.");
    }

    private void LiveCodingSession()
    {
        // Get required data.
        var codingSession = LiveCodingSessionPage.Show();

        // Commit to database.
        _codingSessionController.AddCodingSession(codingSession.StartTime, codingSession.EndTime);

        // Display output.
        MessagePage.Show("Live Coding Session", $"Coding session created successfully.");
    }

    #endregion
}
