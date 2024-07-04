using System.Configuration;
using CodingTracker.ConsoleApp.Views;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CodingTracker.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            // Read required configuration settings.
            string databaseConnectionString = GetAppSettingsString("DatabaseConnectionString");
            bool seedDatabase = GetAppSettingsBoolean("SeedDatabase");
            // string error = GetAppSettingsString("Error");

            // Create the required service.
            //TODO:

            // Generate seed data if required.
            if (seedDatabase)
            {
                AnsiConsole.WriteLine("Generating seed data. Please wait...");
                // TODO:Service.SeedDatabase();
                AnsiConsole.WriteLine("Seed data generated.");
            }

            // Show the main menu.
            var mainMenu = new MainMenuPage();
            mainMenu.Show();
        }
        catch (Exception exception)
        {
            // Replace with: MessagePage.Show("Error", exception.Message);
            AnsiConsole.WriteException(exception, ExceptionFormats.NoStackTrace);
            Console.ReadKey();
        }
        finally
        {
            Environment.Exit(0);
        }
    }

    private static bool GetAppSettingsBoolean(string key)
    {
        string? value = ConfigurationManager.AppSettings.Get(key);

        if (value == null)
        {
            throw new Exception($"Unable to get a value for '{key}' in App.config.");
        }

        if (!bool.TryParse(value, out bool result))
        {
            throw new Exception($"Unable to parse '{value}' to boolean for '{key}' in App.config.");
        }

        return result;
    }

    private static string GetAppSettingsString(string key)
    {
        string? value = ConfigurationManager.AppSettings.Get(key);

        if (value == null)
        {
            throw new Exception($"Unable to get a value for '{key}' in App.config.");
        }

        return value;
    }
}
