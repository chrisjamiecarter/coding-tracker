using CodingTracker.ConsoleApp.Enums;
using CodingTracker.ConsoleApp.Models;
using Spectre.Console;
using System.Collections.ObjectModel;

namespace CodingTracker.ConsoleApp.Views;

internal class MainMenuPage : BasePage
{
    #region Constants

    private const string PageTitle = "Main Menu";

    private const string PromptTitle = "Select an option...";

    #endregion
    #region Fields

    //TODO: private readonly Service _service;

    #endregion
    #region Constructors

    public MainMenuPage() // TODO: pass in service.
    {
        // TODO: assign fields.
    }

    #endregion
    #region Properties

    internal static IEnumerable<MainMenuOption> PageOptions
    {
        get
        {
            return
            [
                new(0, "Close application"),
                new(1, "Do something")
            ];
        }
    }

    #endregion
    #region Methods

    internal void Show()
    {
        var status = PageStatus.Opened;

        while(status != PageStatus.Closed)
        {
            AnsiConsole.Clear();

            WriteHeader(PageTitle);

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOption>()
                .Title(PromptTitle)
                .AddChoices(PageOptions)
                .UseConverter(c => c.Name!)
                );

            status = PerformOption(option);
        }
    }

    private PageStatus PerformOption(MainMenuOption option)
    {
        switch (option.Index)
        {
            case 0:
                // Close application.
                AnsiConsole.WriteLine("Closing application.");
                return PageStatus.Closed;
                
            case 1:
                // Do something.
                AnsiConsole.WriteLine("Doing something now.");
                return PageStatus.Opened;
            default:
                // Do nothing, but remain on this page.
                return PageStatus.Opened;
        }
    }

    #endregion
}
