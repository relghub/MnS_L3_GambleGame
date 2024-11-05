namespace MnS_L3_GambleGame
{
	public enum TextBoxes
	{
		initAmountBox, amountToWinBox, currAmountBox, expProbBox, theorProbBox, logBox
	}
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void GambleStart_Click(object sender, EventArgs e)
		{
			RouletteSimulation.SingleExperiment([
				initAmountBox,
				amountToWinBox,
				currAmountBox,
				expProbBox,
				theorProbBox,
				logBox
			], iterLabel);
		}

		private async void GambleEstimateButton_Click(object sender, EventArgs e)
		{
			Progress<string> progress = new(s => expProbBox.Text = s);
			await Task.Factory.StartNew(() =>
			{
				RouletteSimulation.MultipleExperiments(
				[
				initAmountBox,
				amountToWinBox,
				currAmountBox,
				expProbBox,
				theorProbBox,
				logBox
				], iterLabel, progress);
			});
		}
	}

	class RouletteSimulation
	{
		private static readonly Random random = new();
		private static readonly double winProbability = 9.0 / 19.0;

		// Function to simulate one game session
		static (bool, int) PlayGame(int startingAmount, int targetWin, TextBox[] textBoxes, bool batchMode)
		{
			int i = 0,
				currentAmount = startingAmount,
				winGoal = startingAmount + targetWin;

			while (currentAmount > 0 && currentAmount < winGoal)
			{
				i++;
				// Player bets 1 dollar
				if (random.NextDouble() < winProbability) // Player wins the bet
				{
					if (batchMode)
					{
						textBoxes[(int)TextBoxes.logBox].AppendText($"Iteration {i}: Colors match" + Environment.NewLine);
						textBoxes[(int)TextBoxes.currAmountBox].Text = (++currentAmount).ToString();
					}
					else
					{
						currentAmount++;
					}

				}
				else // Player loses the bet
				{
					if (batchMode)
					{
						textBoxes[(int)TextBoxes.logBox].AppendText($"Iteration {i}: Colors do not match" + Environment.NewLine);
						textBoxes[(int)TextBoxes.currAmountBox].Text = (--currentAmount).ToString();
					}
					else
					{
						currentAmount--;
					}
				}
			}

			// Return true if the player reached the win goal, false if they lost all their money
			return (currentAmount >= winGoal, i);
		}

		// Function to estimate the probability by running multiple experiments
		static (double, double) EstimateWinningProbability(int startingAmount,
												 int targetWin,
												 int experiments,
												 TextBox[] textBoxes,
												 bool mode,
												 IProgress<string> progress)
		{
			int wins = 0;
			long iterations = 0;

			for (int i = 0; i < experiments; i++)
			{
				(bool, int) session = PlayGame(startingAmount, targetWin, textBoxes, mode);
				if (session.Item1)
				{
					wins++;
					Task.Delay(1).Wait();
					progress.Report(((double)wins / experiments).ToString());
				}
				iterations += session.Item2;
			}

			// Calculate the probability of winning
			return ((double)wins / experiments, double.Truncate(iterations / experiments));
		}

		static double EvaluateTheoreticalProbability(int n, int m)
		{
			double z = (1 / winProbability) - 1,
				p_nm = (Math.Pow(z, n) - 1) / (Math.Pow(z, n + m) - 1);
			return p_nm;
		}

		public static void MultipleExperiments(TextBox[] textBoxes, Label label, IProgress<string> progress)
		{
			// Input values
			int n = int.Parse(textBoxes[(int)TextBoxes.initAmountBox].Text);  // Starting amount in dollars
			int m = int.Parse(textBoxes[(int)TextBoxes.amountToWinBox].Text);  // Target win amount in dollars
			int experiments = 100000000;  // Number of experiments to run

			textBoxes[(int)TextBoxes.theorProbBox].Text = $"{EvaluateTheoreticalProbability(n, m)}"; // Display the theoretical probability

			// Estimate the probability
			(double, double) result = EstimateWinningProbability(n, m, experiments, textBoxes, false, progress);

			// Display the result
			textBoxes[(int)TextBoxes.expProbBox].Text = $"{result.Item1}";
			label.Text = $"Average of iterations: {result.Item2}";
		}

		public static void SingleExperiment(TextBox[] textBoxes, Label label)
		{
			// Input values
			int n = int.Parse(textBoxes[(int)TextBoxes.initAmountBox].Text);  // Starting amount in dollars
			int m = int.Parse(textBoxes[(int)TextBoxes.amountToWinBox].Text);  // Target win amount in dollars

			if (n > 0) {
				(bool, int) win = PlayGame(n, m, textBoxes, true);
				textBoxes[(int)TextBoxes.logBox].AppendText(win.Item1 ? "YOU WIN" + Environment.NewLine : "GAME OVER" + Environment.NewLine);
				label.Text = $"Iteration number: {win.Item2}"; }
			else
			{
				MessageBox.Show("Not enough money!");
			}
		}
	}

}
