using CodingTracker.ConsoleApp.Constants;
using CodingTracker.ConsoleApp.Models;
using CodingTracker.Constants;
using CodingTracker.Enums;
using CodingTracker.Models;
using CodingTracker.Services;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker.ConsoleApp.Views;

internal class ReportFilterPage : BasePage
{
    #region Constants

    private const string PageTitle = "Filter Report";

    #endregion
    #region Properties

    internal static IEnumerable<PromptChoice> ReportFilterTypeOptions
    {
        get
        {
            return
            [
                new(1, "All"),
                new(2, "By day"),
                new(3, "By week"),
                new(4, "By month"),
                new(5, "By year"),
                new(0, "Close page")
            ];
        }
    }

    internal static IEnumerable<PromptChoice> ReportFilterOrderByOptions
    {
        get
        {
            return
            [
                new(1, "Ascending"),
                new(2, "Descending"),
                new(0, "Close page")
            ];
        }
    }

    #endregion
    #region Methods: Internal

    internal static ReportFilter? Show()
    {
        ReportFilter? nullReportFilter = null;

        AnsiConsole.Clear();

        WriteHeader(PageTitle);

        var reportFilterTypeOption = AnsiConsole.Prompt(
            new SelectionPrompt<PromptChoice>()
            .Title(Prompt.Title)
            .AddChoices(ReportFilterTypeOptions)
            .UseConverter(c => c.Name!)
            );

        ReportFilterType filterType = ReportFilterType.All;
        DateTime? startDateTime = null;
        DateTime? endDateTime = null;
        ReportOrderByType orderBy = ReportOrderByType.Ascending;

        switch (reportFilterTypeOption.Id)
        {
            case 0:

                // Close page.
                return nullReportFilter;

            case 1:

                // All.
                return new ReportFilter
                {
                    Type = filterType,
                    StartDate = startDateTime,
                    EndDate = endDateTime,
                    OrderBy = orderBy
                };

            case 2:

                // By day.
                filterType = ReportFilterType.Day;
                startDateTime = GetStartDate(GetDateStringFormat(filterType));
                if (startDateTime == null)
                {
                    return nullReportFilter;
                }
                endDateTime = GetEndDate(GetDateStringFormat(filterType), startDateTime.Value.Date);
                if (endDateTime == null)
                { 
                    return nullReportFilter;
                }
                endDateTime = endDateTime.Value.AddDays(1).AddTicks(-1);
                break;

            case 3:

                // By week.
                filterType = ReportFilterType.Week;
                startDateTime = GetStartDate(GetDateStringFormat(filterType));
                if (startDateTime == null)
                {
                    return nullReportFilter;
                }
                endDateTime = GetEndDate(GetDateStringFormat(filterType), startDateTime.Value.Date);
                if (endDateTime == null)
                {
                    return nullReportFilter;
                }
                endDateTime = endDateTime.Value.AddDays(1).AddTicks(-1);
                break;

            case 4:

                // By month.
                filterType = ReportFilterType.Month;
                startDateTime = GetStartDate(GetDateStringFormat(filterType));
                if (startDateTime == null)
                {
                    return nullReportFilter;
                }
                endDateTime = GetEndDate(GetDateStringFormat(filterType), startDateTime.Value.Date);
                if (endDateTime == null)
                {
                    return nullReportFilter;
                }
                endDateTime = endDateTime.Value.AddMonths(1).AddTicks(-1);
                break;

            case 5:

                // By year.
                filterType = ReportFilterType.Year;
                startDateTime = GetStartDate(GetDateStringFormat(filterType));
                if (startDateTime == null)
                {
                    return nullReportFilter;
                }
                endDateTime = GetEndDate(GetDateStringFormat(filterType), startDateTime.Value.Date);
                if (endDateTime == null)
                {
                    return nullReportFilter;
                }
                endDateTime = endDateTime.Value.AddYears(1).AddTicks(-1);
                break;

            default:
                return nullReportFilter;
        }

        var reportFilterOrderByOption = AnsiConsole.Prompt(
            new SelectionPrompt<PromptChoice>()
            .Title(Prompt.Title)
            .AddChoices(ReportFilterOrderByOptions)
            .UseConverter(c => c.Name!)
        );

        switch (reportFilterOrderByOption.Id)
        {
            case 0:

                // Close page.
                return nullReportFilter;

            case 1:

                // Ascending.
                orderBy = ReportOrderByType.Ascending;
                break;

            case 2:

                // Descending.
                orderBy = ReportOrderByType.Descending;
                break;

            default:
                return nullReportFilter;
        }

        return new ReportFilter
        {
            Type = filterType,
            StartDate = startDateTime,
            EndDate = endDateTime,
            OrderBy = orderBy
        };
    }

    private static string GetDateStringFormat(ReportFilterType filterType)
    {
        return filterType switch
        {
            ReportFilterType.All => StringFormat.DateTime,
            ReportFilterType.Day => StringFormat.Date,
            ReportFilterType.Week => StringFormat.Date,
            ReportFilterType.Month => StringFormat.YearMonth,
            ReportFilterType.Year => StringFormat.Year,
            _ => StringFormat.DateTime
        };
    }

    private static DateTime? GetStartDate(string dateStringFormat)
    {
        var startMessage = $"Enter the start date, format '{dateStringFormat}', or 0 to return to main menu: ";
        var startInput = AnsiConsole.Ask<string>(startMessage);
        var startInputValidation = ValidationService.IsValidReportStartDate(startInput, dateStringFormat);
        while (!startInputValidation.IsValid)
        {
            if (startInput == "0")
            {
                return null;
            }
            AnsiConsole.WriteLine(startInputValidation.Message);
            startInput = AnsiConsole.Ask<string>(startMessage);
            startInputValidation = ValidationService.IsValidReportStartDate(startInput, dateStringFormat);
        }
        return DateTime.ParseExact(startInput, dateStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
    }

    private static DateTime? GetEndDate(string dateStringFormat, DateTime startDate)
    {
        var endMessage = $"Enter the end date, format '{dateStringFormat}', or 0 to return to main menu: ";
        var endInput = AnsiConsole.Ask<string>(endMessage);
        var endInputValidation = ValidationService.IsValidReportEndDate(endInput, dateStringFormat, startDate);
        while (!endInputValidation.IsValid)
        {
            if (endInput == "0")
            {
                return null;
            }
            AnsiConsole.WriteLine(endInputValidation.Message);
            endInput = AnsiConsole.Ask<string>(endMessage);
            endInputValidation = ValidationService.IsValidReportEndDate(endInput, dateStringFormat, startDate);
        }
        return DateTime.ParseExact(endInput, dateStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
    }

    #endregion
}
