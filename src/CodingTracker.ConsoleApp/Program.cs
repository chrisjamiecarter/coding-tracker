using System.Configuration;
using CodingTracker.Application.Controllers;
using CodingTracker.ConsoleApp.Extensions;
using CodingTracker.ConsoleApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;

namespace CodingTracker.ConsoleApp;

/// <summary>
/// The entry point for the application.
/// Configures the required services and middleware before running the application.
/// </summary>
internal class Program
{
    private static void Main(string[] args)
    {
        // Read required configuration settings.
        string databaseConnectionString = ConfigurationManager.AppSettings.GetString("DatabaseConnectionString");
        bool seedDatabase = ConfigurationManager.AppSettings.GetBoolean("SeedDatabase");

        using IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<CodingGoalController>(x => new(databaseConnectionString));
                services.AddScoped<CodingSessionController>(x => new(databaseConnectionString));
                services.AddTransient<MainMenuPage>();
            })
            .Build();

        try
        {
            // Generate seed data if required.
            if (seedDatabase)
            {
                using var scope = host.Services.CreateScope();
                var codingSessionController = scope.ServiceProvider.GetRequiredService<CodingSessionController>();

                // Could be a long(ish) process, so show a spinner while it works.
                AnsiConsole.Status()
                    .Spinner(Spinner.Known.Aesthetic)
                    .Start("Generating seed data. Please wait...", ctx =>
                    {
                        codingSessionController.SeedDatabase();
                    });
                AnsiConsole.WriteLine("Seed data generated.");
            }
            
            // Show the main menu.
            var mainMenuPage = host.Services.GetRequiredService<MainMenuPage>();
            mainMenuPage.Show();
            
            Environment.Exit(0);
        }
        catch (Exception exception)
        {
            ErrorPage.Show(exception);
            Environment.Exit(-1);
        }
    }
}
