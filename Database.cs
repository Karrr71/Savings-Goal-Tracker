Install-Package System.Data.SQLite
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SavingsGoalTracker
{
    public class DatabaseHelper
    {
        private const string ConnectionString = "Data Source=SavingsGoals.db;Version=3;";

        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                var createTableQuery = @"CREATE TABLE IF NOT EXISTS SavingsGoals (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            GoalName TEXT NOT NULL,
                                            TargetAmount DECIMAL NOT NULL,
                                            CurrentAmount DECIMAL NOT NULL
                                        )";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddGoal(SavingsGoal goal)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                var insertQuery = @"INSERT INTO SavingsGoals (GoalName, TargetAmount, CurrentAmount)
                                    VALUES (@GoalName, @TargetAmount, @CurrentAmount)";

                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@GoalName", goal.GoalName);
                    command.Parameters.AddWithValue("@TargetAmount", goal.TargetAmount);
                    command.Parameters.AddWithValue("@CurrentAmount", goal.CurrentAmount);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<SavingsGoal> GetGoals()
        {
            var goals = new List<SavingsGoal>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                var selectQuery = "SELECT * FROM SavingsGoals";

                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = Convert.ToInt32(reader["Id"]);
                            var goalName = Convert.ToString(reader["GoalName"]);
                            var targetAmount = Convert.ToDecimal(reader["TargetAmount"]);
                            var currentAmount = Convert.ToDecimal(reader["CurrentAmount"]);

                            var goal = new SavingsGoal(goalName, targetAmount)
                            {
                                Id = id,
                                CurrentAmount = currentAmount
                            };

                            goals.Add(goal);
                        }
                    }
                }
            }

            return goals;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SavingsGoalTracker
{
    public partial class MainForm : Form
    {
        private List<SavingsGoal> goals;

        public MainForm()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();
            goals = DatabaseHelper.GetGoals();
            RefreshGoalList();
        }

        private void btnAddGoal_Click(object sender, EventArgs e)
        {
            string goalName = txtGoalName.Text;
            decimal targetAmount;
            if (!decimal.TryParse(txtTargetAmount.Text, out targetAmount))
            {
                MessageBox.Show("Please enter a valid target amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var goal = new SavingsGoal(goalName, targetAmount);
            DatabaseHelper.AddGoal(goal);
            goals.Add(goal);
            RefreshGoalList();
            ClearInputs();
        }

        // Other methods remain unchanged...
    }
}
