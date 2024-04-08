using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SavingsGoalTracker
{
    public partial class MainForm : Form
    {
        private List<SavingsGoal> goals = new List<SavingsGoal>();

        public MainForm()
        {
            InitializeComponent();
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

            goals.Add(new SavingsGoal(goalName, targetAmount));
            RefreshGoalList();
            ClearInputs();
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (lstGoals.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a goal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal depositAmount;
            if (!decimal.TryParse(txtDepositAmount.Text, out depositAmount))
            {
                MessageBox.Show("Please enter a valid deposit amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedIndex = lstGoals.SelectedIndex;
            goals[selectedIndex].Deposit(depositAmount);
            RefreshGoalList();
            ClearInputs();
        }

        private void RefreshGoalList()
        {
            lstGoals.Items.Clear();
            foreach (SavingsGoal goal in goals)
            {
                lstGoals.Items.Add(goal);
            }
        }

        private void ClearInputs()
        {
            txtGoalName.Clear();
            txtTargetAmount.Clear();
            txtDepositAmount.Clear();
        }
    }

    public class SavingsGoal
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
            CurrentAmount += amount;
            MessageBox.Show($"{amount:C} deposited into {GoalName}.", "Deposit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CheckProgress();
        }

        private void CheckProgress()
        {
            decimal progress = CurrentAmount / TargetAmount * 100;

            if (progress >= 100)
            {
                MessageBox.Show($"Congratulations! You've reached your savings goal for {GoalName}.", "Goal Reached", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public override string ToString()
        {
            return $"{GoalName} - {CurrentAmount:C} / {TargetAmount:C}";
        }
    }
}
