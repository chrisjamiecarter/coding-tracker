using CodingTracker.Models;
using CodingTracker.Services;
using Spectre.Console;

namespace CodingTracker.ConsoleApp.Views;

internal class SetCodingGoalPage : BasePage
{
    #region Constants

    private const string PageTitle = "Set Coding Goal";
    
    #endregion
    #region Methods: Internal

    internal static CodingGoal Show()
    {
        AnsiConsole.Clear();

        WriteHeader(PageTitle);

        var message = $"Enter you weekly coding goal duration in hours: ";
        var input = AnsiConsole.Ask<double>(message);
        var inputValidation = ValidationService.IsValidCodingGoalDuration(input);
        while (!inputValidation.IsValid)
        {
            AnsiConsole.WriteLine(inputValidation.Message);
            input = AnsiConsole.Ask<double>(message);
            inputValidation = ValidationService.IsValidCodingGoalDuration(input);
        }
        
        return new CodingGoal(input);
    }

    #endregion
}
