namespace MnS_L3_GambleGame
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gambleStart = new Button();
            label1 = new Label();
            label2 = new Label();
            initAmountBox = new TextBox();
            amountToWinBox = new TextBox();
            expProbBox = new TextBox();
            label3 = new Label();
            gambleEstimateButton = new Button();
            logBox = new TextBox();
            iterLabel = new Label();
            currAmountBox = new TextBox();
            label4 = new Label();
            theorProbBox = new TextBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // gambleStart
            // 
            gambleStart.Location = new Point(12, 219);
            gambleStart.Name = "gambleStart";
            gambleStart.Size = new Size(94, 29);
            gambleStart.TabIndex = 0;
            gambleStart.Text = "Gamble";
            gambleStart.UseVisualStyleBackColor = true;
            gambleStart.Click += GambleStart_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(219, 15);
            label1.Name = "label1";
            label1.Size = new Size(101, 20);
            label1.TabIndex = 1;
            label1.Text = "Initial amount";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(213, 55);
            label2.Name = "label2";
            label2.Size = new Size(107, 20);
            label2.TabIndex = 2;
            label2.Text = "Amount to win";
            // 
            // initAmountBox
            // 
            initAmountBox.Location = new Point(12, 12);
            initAmountBox.Name = "initAmountBox";
            initAmountBox.Size = new Size(164, 27);
            initAmountBox.TabIndex = 3;
            // 
            // amountToWinBox
            // 
            amountToWinBox.Location = new Point(12, 51);
            amountToWinBox.Name = "amountToWinBox";
            amountToWinBox.Size = new Size(164, 27);
            amountToWinBox.TabIndex = 4;
            // 
            // expProbBox
            // 
            expProbBox.Location = new Point(12, 129);
            expProbBox.Name = "expProbBox";
            expProbBox.ReadOnly = true;
            expProbBox.Size = new Size(164, 27);
            expProbBox.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(185, 135);
            label3.Name = "label3";
            label3.Size = new Size(135, 20);
            label3.TabIndex = 6;
            label3.Text = "Experimental prob.";
            // 
            // gambleEstimateButton
            // 
            gambleEstimateButton.Location = new Point(112, 219);
            gambleEstimateButton.Name = "gambleEstimateButton";
            gambleEstimateButton.Size = new Size(208, 29);
            gambleEstimateButton.TabIndex = 7;
            gambleEstimateButton.Text = "Estimate probability";
            gambleEstimateButton.UseVisualStyleBackColor = true;
            gambleEstimateButton.Click += GambleEstimateButton_Click;
            // 
            // logBox
            // 
            logBox.Location = new Point(12, 261);
            logBox.Multiline = true;
            logBox.Name = "logBox";
            logBox.Size = new Size(308, 213);
            logBox.TabIndex = 8;
            // 
            // iterLabel
            // 
            iterLabel.AutoSize = true;
            iterLabel.Location = new Point(12, 487);
            iterLabel.Name = "iterLabel";
            iterLabel.Size = new Size(135, 20);
            iterLabel.TabIndex = 9;
            iterLabel.Text = "Iteration number: 0";
            // 
            // currAmountBox
            // 
            currAmountBox.Location = new Point(12, 90);
            currAmountBox.Name = "currAmountBox";
            currAmountBox.ReadOnly = true;
            currAmountBox.Size = new Size(164, 27);
            currAmountBox.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(208, 95);
            label4.Name = "label4";
            label4.Size = new Size(112, 20);
            label4.TabIndex = 11;
            label4.Text = "Current amount";
            // 
            // theorProbBox
            // 
            theorProbBox.Location = new Point(12, 168);
            theorProbBox.Name = "theorProbBox";
            theorProbBox.ReadOnly = true;
            theorProbBox.Size = new Size(164, 27);
            theorProbBox.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(198, 175);
            label5.Name = "label5";
            label5.Size = new Size(122, 20);
            label5.TabIndex = 13;
            label5.Text = "Theoretical prob.";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(332, 515);
            Controls.Add(label5);
            Controls.Add(theorProbBox);
            Controls.Add(label4);
            Controls.Add(currAmountBox);
            Controls.Add(iterLabel);
            Controls.Add(logBox);
            Controls.Add(gambleEstimateButton);
            Controls.Add(label3);
            Controls.Add(expProbBox);
            Controls.Add(amountToWinBox);
            Controls.Add(initAmountBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(gambleStart);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "Casino";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button gambleStart;
        private Label label1;
        private Label label2;
        private TextBox initAmountBox;
        private TextBox amountToWinBox;
        private Label label3;
        internal TextBox expProbBox;
        private Button gambleEstimateButton;
        private TextBox logBox;
        private Label iterLabel;
        internal TextBox currAmountBox;
        private Label label4;
        internal TextBox theorProbBox;
        private Label label5;
    }
}
