namespace MinCutMaxFlow
{
    partial class MinCutMaxFlow
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
            maxFlowButton = new Button();
            pictureBoxGraph = new PictureBox();
            addFileButton = new Button();
            minCutButton = new Button();
            maxFlowNameLabel = new Label();
            flowValuesNameLabel = new Label();
            flowValuesLabel = new Label();
            minCutLabel = new Label();
            residualGraphLabel = new Label();
            minCutValuesNameLabel = new Label();
            minCostMaxFlowButton = new Button();
            minCostLabel = new Label();
            minCostMaxFlowLabel = new Label();
            minCostFlowValuesLabel = new Label();
            minCostNameLabel = new Label();
            minCostMaxFlowNameLabel = new Label();
            minCostFlowValuesNameLabel = new Label();
            maxFlowLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGraph).BeginInit();
            SuspendLayout();
            // 
            // maxFlowButton
            // 
            maxFlowButton.Location = new Point(12, 108);
            maxFlowButton.Name = "maxFlowButton";
            maxFlowButton.Size = new Size(99, 23);
            maxFlowButton.TabIndex = 0;
            maxFlowButton.Text = "Max Flow";
            maxFlowButton.UseVisualStyleBackColor = true;
            maxFlowButton.Click += maxFlowButton_Click;
            // 
            // pictureBoxGraph
            // 
            pictureBoxGraph.Location = new Point(518, 12);
            pictureBoxGraph.Name = "pictureBoxGraph";
            pictureBoxGraph.Size = new Size(416, 411);
            pictureBoxGraph.TabIndex = 11;
            pictureBoxGraph.TabStop = false;
            // 
            // addFileButton
            // 
            addFileButton.Location = new Point(12, 12);
            addFileButton.Name = "addFileButton";
            addFileButton.Size = new Size(75, 23);
            addFileButton.TabIndex = 12;
            addFileButton.Text = "Add file";
            addFileButton.UseVisualStyleBackColor = true;
            addFileButton.Click += addFileButton_Click;
            // 
            // minCutButton
            // 
            minCutButton.Location = new Point(154, 108);
            minCutButton.Name = "minCutButton";
            minCutButton.Size = new Size(99, 23);
            minCutButton.TabIndex = 14;
            minCutButton.Text = "Min Cut";
            minCutButton.UseVisualStyleBackColor = true;
            minCutButton.Click += minCutButton_Click;
            // 
            // maxFlowNameLabel
            // 
            maxFlowNameLabel.AutoSize = true;
            maxFlowNameLabel.Location = new Point(12, 140);
            maxFlowNameLabel.Name = "maxFlowNameLabel";
            maxFlowNameLabel.Size = new Size(59, 15);
            maxFlowNameLabel.TabIndex = 16;
            maxFlowNameLabel.Text = "Max flow:";
            maxFlowNameLabel.Visible = false;
            // 
            // flowValuesNameLabel
            // 
            flowValuesNameLabel.AutoSize = true;
            flowValuesNameLabel.Location = new Point(12, 167);
            flowValuesNameLabel.Name = "flowValuesNameLabel";
            flowValuesNameLabel.Size = new Size(104, 15);
            flowValuesNameLabel.TabIndex = 18;
            flowValuesNameLabel.Text = "List of flow values:";
            flowValuesNameLabel.Visible = false;
            // 
            // flowValuesLabel
            // 
            flowValuesLabel.AutoSize = true;
            flowValuesLabel.Location = new Point(12, 191);
            flowValuesLabel.Name = "flowValuesLabel";
            flowValuesLabel.Size = new Size(91, 15);
            flowValuesLabel.TabIndex = 19;
            flowValuesLabel.Text = "flowValuesLabel";
            flowValuesLabel.Visible = false;
            // 
            // minCutLabel
            // 
            minCutLabel.AutoSize = true;
            minCutLabel.Location = new Point(154, 167);
            minCutLabel.Name = "minCutLabel";
            minCutLabel.Size = new Size(75, 15);
            minCutLabel.TabIndex = 20;
            minCutLabel.Text = "minCutLabel";
            minCutLabel.Visible = false;
            // 
            // residualGraphLabel
            // 
            residualGraphLabel.AutoSize = true;
            residualGraphLabel.Location = new Point(12, 688);
            residualGraphLabel.Name = "residualGraphLabel";
            residualGraphLabel.Size = new Size(109, 15);
            residualGraphLabel.TabIndex = 21;
            residualGraphLabel.Text = "Residual Graph Test";
            residualGraphLabel.Visible = false;
            // 
            // minCutValuesNameLabel
            // 
            minCutValuesNameLabel.AutoSize = true;
            minCutValuesNameLabel.Location = new Point(154, 140);
            minCutValuesNameLabel.Name = "minCutValuesNameLabel";
            minCutValuesNameLabel.Size = new Size(85, 15);
            minCutValuesNameLabel.TabIndex = 23;
            minCutValuesNameLabel.Text = "Min cut edges:";
            minCutValuesNameLabel.Visible = false;
            // 
            // minCostMaxFlowButton
            // 
            minCostMaxFlowButton.Location = new Point(312, 108);
            minCostMaxFlowButton.Name = "minCostMaxFlowButton";
            minCostMaxFlowButton.Size = new Size(120, 23);
            minCostMaxFlowButton.TabIndex = 24;
            minCostMaxFlowButton.Text = "Min Cost Max Flow";
            minCostMaxFlowButton.UseVisualStyleBackColor = true;
            minCostMaxFlowButton.Click += minCostMaxFlowButton_Click;
            // 
            // minCostLabel
            // 
            minCostLabel.AutoSize = true;
            minCostLabel.Location = new Point(378, 140);
            minCostLabel.Name = "minCostLabel";
            minCostLabel.Size = new Size(38, 15);
            minCostLabel.TabIndex = 25;
            minCostLabel.Text = "label1";
            minCostLabel.Visible = false;
            // 
            // minCostMaxFlowLabel
            // 
            minCostMaxFlowLabel.AutoSize = true;
            minCostMaxFlowLabel.Location = new Point(378, 167);
            minCostMaxFlowLabel.Name = "minCostMaxFlowLabel";
            minCostMaxFlowLabel.Size = new Size(38, 15);
            minCostMaxFlowLabel.TabIndex = 26;
            minCostMaxFlowLabel.Text = "label2";
            minCostMaxFlowLabel.Visible = false;
            // 
            // minCostFlowValuesLabel
            // 
            minCostFlowValuesLabel.AutoSize = true;
            minCostFlowValuesLabel.Location = new Point(312, 216);
            minCostFlowValuesLabel.Name = "minCostFlowValuesLabel";
            minCostFlowValuesLabel.Size = new Size(38, 15);
            minCostFlowValuesLabel.TabIndex = 27;
            minCostFlowValuesLabel.Text = "label3";
            minCostFlowValuesLabel.Visible = false;
            // 
            // minCostNameLabel
            // 
            minCostNameLabel.AutoSize = true;
            minCostNameLabel.Location = new Point(312, 140);
            minCostNameLabel.Name = "minCostNameLabel";
            minCostNameLabel.Size = new Size(56, 15);
            minCostNameLabel.TabIndex = 28;
            minCostNameLabel.Text = "Min cost:";
            minCostNameLabel.Visible = false;
            // 
            // minCostMaxFlowNameLabel
            // 
            minCostMaxFlowNameLabel.AutoSize = true;
            minCostMaxFlowNameLabel.Location = new Point(312, 167);
            minCostMaxFlowNameLabel.Name = "minCostMaxFlowNameLabel";
            minCostMaxFlowNameLabel.Size = new Size(59, 15);
            minCostMaxFlowNameLabel.TabIndex = 29;
            minCostMaxFlowNameLabel.Text = "Max flow:";
            minCostMaxFlowNameLabel.Visible = false;
            // 
            // minCostFlowValuesNameLabel
            // 
            minCostFlowValuesNameLabel.AutoSize = true;
            minCostFlowValuesNameLabel.Location = new Point(312, 191);
            minCostFlowValuesNameLabel.Name = "minCostFlowValuesNameLabel";
            minCostFlowValuesNameLabel.Size = new Size(104, 15);
            minCostFlowValuesNameLabel.TabIndex = 30;
            minCostFlowValuesNameLabel.Text = "List of flow values:";
            minCostFlowValuesNameLabel.Visible = false;
            // 
            // maxFlowLabel
            // 
            maxFlowLabel.AutoSize = true;
            maxFlowLabel.Location = new Point(73, 140);
            maxFlowLabel.Name = "maxFlowLabel";
            maxFlowLabel.Size = new Size(38, 15);
            maxFlowLabel.TabIndex = 31;
            maxFlowLabel.Text = "label4";
            maxFlowLabel.Visible = false;
            // 
            // MinCutMaxFlow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1098, 842);
            Controls.Add(maxFlowLabel);
            Controls.Add(minCostFlowValuesNameLabel);
            Controls.Add(minCostMaxFlowNameLabel);
            Controls.Add(minCostNameLabel);
            Controls.Add(minCostFlowValuesLabel);
            Controls.Add(minCostMaxFlowLabel);
            Controls.Add(minCostLabel);
            Controls.Add(minCostMaxFlowButton);
            Controls.Add(minCutValuesNameLabel);
            Controls.Add(residualGraphLabel);
            Controls.Add(minCutLabel);
            Controls.Add(flowValuesLabel);
            Controls.Add(flowValuesNameLabel);
            Controls.Add(maxFlowNameLabel);
            Controls.Add(minCutButton);
            Controls.Add(addFileButton);
            Controls.Add(pictureBoxGraph);
            Controls.Add(maxFlowButton);
            Name = "MinCutMaxFlow";
            Text = "MinCutMaxFlow";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxGraph).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button maxFlowButton;
        private PictureBox pictureBoxGraph;
        private Button addFileButton;
        private Label flowValuesNameLabel;
        private Label flowValuesLabel;
        private Button minCutButton;
        private Label maxFlowNameLabel;
        private TextBox textBox1;
        private Label minCutLabel;
        private Label residualGraphLabel;
        private Label minCutValuesNameLabel;
        private Button minCostMaxFlowButton;
        private Label minCostLabel;
        private Label minCostMaxFlowLabel;
        private Label minCostFlowValuesLabel;
        private Label minCostNameLabel;
        private Label minCostMaxFlowNameLabel;
        private Label minCostFlowValuesNameLabel;
        private Label maxFlowLabel;
    }
}