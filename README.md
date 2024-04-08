# Savings Goal Tracker

The Savings Goal Tracker is a simple application built with C# and WinForms that allows users to set savings goals, track their progress, and receive notifications when they reach milestones.

## Features

- Add savings goals with names and target amounts.
- Deposit money into savings goals to track progress.
- Receive notifications when savings goals are reached.

## Requirements

- Visual Studio (or any other C# IDE)
- .NET Framework
- System.Data.SQLite NuGet package

## Installation

1. Clone or download this repository.
2. Open the solution file (SavingsGoalTracker.sln) in Visual Studio.
3. Restore NuGet packages if needed.
4. Build and run the application.

## Usage

1. Launch the application.
2. Click on the "Add Goal" button to create a new savings goal.
3. Enter the goal name and target amount, then click "Add Goal."
4. Once a goal is added, select it from the list and enter the amount to deposit into that goal.
5. Click "Deposit" to add the amount to the selected goal.
6. Track the progress of your savings goals and receive notifications when goals are reached.

## Database

The application uses SQLite as a lightweight database to store savings goals. The `SavingsGoals.db` file is created automatically in the project directory and stores goal information.

## Contributing

Contributions are welcome! If you have suggestions or found a bug, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
