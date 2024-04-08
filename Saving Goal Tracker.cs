using System;
using System.Collections.Generic;

class SavingsGoal
{
    public string GoalName { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; private set; }

    public SavingsGoal(string name, decimal targetAmount)
    {
        GoalName = name;
        TargetAmount = targetAmount;
        CurrentAmount = 0;
    }

    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            Console.WriteLine("Invalid amount. Deposit amount should be positive.");
            return;
        }

        CurrentAmount += amount;
        Console.WriteLine($"{amount:C} deposited into {GoalName}.");
        CheckProgress();
    }

    private void CheckProgress()
    {
        decimal progress = CurrentAmount / TargetAmount * 100;
        Console.WriteLine($"Progress for {GoalName}: {progress:F2}%");

        if (progress >= 100)
        {
            Console.WriteLine($"Congratulations! You've reached your savings goal for {GoalName}.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<SavingsGoal> goals = new List<SavingsGoal>();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("1. Add a savings goal");
            Console.WriteLine("2. Deposit into a savings goal");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter goal name: ");
                    string goalName = Console.ReadLine();
                    Console.Write("Enter target amount: ");
                    decimal targetAmount = decimal.Parse(Console.ReadLine());
                    goals.Add(new SavingsGoal(goalName, targetAmount));
                    break;
                case "2":
                    Console.WriteLine("Select a savings goal:");
                    for (int i = 0; i < goals.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {goals[i].GoalName}");
                    }
                    int selection = int.Parse(Console.ReadLine());
                    Console.Write("Enter deposit amount: ");
                    decimal depositAmount = decimal.Parse(Console.ReadLine());
                    goals[selection - 1].Deposit(depositAmount);
                    break;
                case "3":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
