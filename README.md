<div align="center">

<img src="./_resources/coding-tracker-logo.png" alt="coding tracker logo" width="100px" />
<h1>Coding Tracker</h1>

</div>

Welcome to the Coding Tracker App!

This is a simple demo console application that allows a user to perform CRUD operations against a database.

## Requirements

- [x] Logs daily coding time.
- [x] Uses the "Spectre.Console" library to show the data on the console.
- [x] Has classes in separate files.
- [x] Tells the user the required date and time input format and not allow any other format.
- [x] Has a configuration file that contains the database connection string.
- [x] Stores and retrieve data from a real database.
- [x] Creates a sqlite database, if one isn’t present, when the application starts.
- [x] Creates a table in the database, if one isn’t present, where Coding Sessions will be logged.
- [x] Shows the user a menu of options.
- [ ] Allows users to insert, delete, update and view Coding Sessions.
- [x] Has a "CodingSession" class in a separate file, which contains the properties of a coding session: Id, StartTime, EndTime, Duration.
- [x] Allows the user to input the start and end times manually.
- [x] Does not allow the user to input the duration of the session. It must be calculated based on the Start and End times, in a separate "CalculateDuration" method.
- [x] Uses Dapper ORM for the data access instead of ADO.NET.
- [x] Does not use anonymous objects when reading from the database. It must read table data into a list of Coding Sessions.
- [x] Handles all possible errors so that the application never crashes.
- [x] Allows the user to insert 0 to terminate the application.
- [x] Contains a Read Me file which explains how the app works.

### Additional Requirements

- [ ] Allows the user to track a live Coding Session time via a stopwatch.
- [ ] Allows the user to filter a report of Coding Sessions by period (weeks, days, years) and/or order ascending or descending.
- [ ] Allows the user to filter a report of Coding Sessions by total and average coding session per period.
- [ ] Allows the user to set coding goals and show how far the user is from reaching their goal, along with how many hours a day they would have to code to reach their goal.

## Features

TODO

## Getting Started

### Prerequisites

- .NET 8 SDK installed on your system.
- A code editor like Visual Studio or Visual Studio Code

### Installation

1. Clone the repository:
	- `git clone https://github.com/cjc-sweatbox/coding-tracker.git`

2. Navigate to the project directory:
	- `cd src\coding-tracker\CodingTracker.ConsoleApp`

3. Run the application using the .NET CLI:
	- `dotnet run`

### Running the Application

1. Run the application using the .NET CLI in the project directory:
	- `dotnet run`

## Usage

TODO

## How It Works

TODO

## Database

TODO

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for details.

## Contact

For any questions or feedback, please open an issue.

---
***Happy Coding Tracking!***
