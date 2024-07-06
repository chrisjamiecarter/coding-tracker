﻿using CodingTracker.ConsoleApp.Enums;
using CodingTracker.ConsoleApp.Models;
using CodingTracker.Constants;
using CodingTracker.Controllers;
using CodingTracker.Models;
using Spectre.Console;
using System.Data;

namespace CodingTracker.ConsoleApp.Views;

internal class MainMenuPage : BasePage
{
    #region Constants

    private const string PageTitle = "Main Menu";

    private const string PromptTitle = "Select an option...";

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
                new(0, "Close application"),
                new(1, "View coding sessions report"),
                new(2, "Create coding session record"),
                new(3, "Update coding session record"),
                new(4, "Delete coding session record")
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
                .Title(PromptTitle)
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
        table.AddColumn("ID");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");
        foreach (var x in data)
        {
            table.AddRow(x.Id.ToString(), x.StartTime.ToString(StringFormat.DateTime), x.EndTime.ToString(StringFormat.DateTime), x.Duration.ToString("F2"));
        }
        
        // Display report.
        MessagePage.Show("Habit Report", table);
    }

    private void CreateCodingSession()
    {
        // Get required data.
        var session = CreateCodingSessionPage.Show();
        if (session == null)
        {
            // If nothing is returned, user has opted to not commit.
            return;
        }

        // Commit to database.
        _codingSessionController.AddCodingSession(session);

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
        _codingSessionController.SetCodingSession(codingSession);

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
        _codingSessionController.DeleteCodingSession(codingSession);

        // Display output.
        MessagePage.Show("Delete Coding Session", $"Coding session deleted successfully.");
    }

    #endregion
}
