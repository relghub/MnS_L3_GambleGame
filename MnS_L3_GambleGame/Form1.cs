using System.Collections.Concurrent;

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
            int numThreads = Environment.ProcessorCount;  // Get the number of available processors
            int experimentsPerThread = experiments / numThreads;
            ConcurrentBag<long> wins = new();
            ConcurrentBag<long> totalIterations = new();
            int completedExperiments = 0;

			
            Parallel.For(0, numThreads, i =>
            {
                long localWins = 0;
                long localIterations = 0;

                for (int j = 0; j < experimentsPerThread; j++)
                {
                    (bool sessionWon, int sessionIterations) = PlayGame(startingAmount, targetWin, textBoxes, mode);
                    if (sessionWon) localWins++;
                    localIterations += sessionIterations;

                    // Increment completed experiments and report intermediate probability
                    Interlocked.Increment(ref completedExperiments);
                    if (completedExperiments % 100000 == 0) // Adjust frequency as needed
                    {
                        double currentProbability = (double)(wins.Count + localWins) / completedExperiments;
                        if (currentProbability != 0) progress.Report($"{currentProbability}");
                    }
                }

                // Add local results to global bags
                wins.Add(localWins);
                totalIterations.Add(localIterations);
            });

            // Aggregate results
            double totalWins = wins.Sum();
            double averageIterations = totalIterations.Sum() / (double)experiments;

            return (totalWins / experiments, averageIterations);
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

			label.Text = "Evaluating...";
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
