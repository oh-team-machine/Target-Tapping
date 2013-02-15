namespace AHSGame
{
    partial class Setup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cancelButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.handComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timeoutUpDown = new System.Windows.Forms.NumericUpDown();
            this.configUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.activeUpDown = new System.Windows.Forms.NumericUpDown();
            this.repetitionsUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.configUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repetitionsUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(13, 244);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(201, 244);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // handComboBox
            // 
            this.handComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.handComboBox.FormattingEnabled = true;
            this.handComboBox.Items.AddRange(new object[] {
            "Left",
            "Right"});
            this.handComboBox.Location = new System.Drawing.Point(13, 20);
            this.handComboBox.Name = "handComboBox";
            this.handComboBox.Size = new System.Drawing.Size(100, 21);
            this.handComboBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dominant Limb";
            // 
            // timeoutUpDown
            // 
            this.timeoutUpDown.DecimalPlaces = 2;
            this.timeoutUpDown.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.timeoutUpDown.Location = new System.Drawing.Point(13, 64);
            this.timeoutUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.timeoutUpDown.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.timeoutUpDown.Name = "timeoutUpDown";
            this.timeoutUpDown.Size = new System.Drawing.Size(99, 20);
            this.timeoutUpDown.TabIndex = 4;
            this.timeoutUpDown.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            // 
            // configUpDown
            // 
            this.configUpDown.Location = new System.Drawing.Point(13, 148);
            this.configUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.configUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.configUpDown.Name = "configUpDown";
            this.configUpDown.Size = new System.Drawing.Size(99, 20);
            this.configUpDown.TabIndex = 5;
            this.configUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Direction Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Configuration";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Simultaneous Directions";
            // 
            // activeUpDown
            // 
            this.activeUpDown.Location = new System.Drawing.Point(12, 106);
            this.activeUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.activeUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.activeUpDown.Name = "activeUpDown";
            this.activeUpDown.Size = new System.Drawing.Size(99, 20);
            this.activeUpDown.TabIndex = 9;
            this.activeUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // repetitionsUpDown
            // 
            this.repetitionsUpDown.Location = new System.Drawing.Point(12, 190);
            this.repetitionsUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.repetitionsUpDown.Name = "repetitionsUpDown";
            this.repetitionsUpDown.Size = new System.Drawing.Size(101, 20);
            this.repetitionsUpDown.TabIndex = 10;
            this.repetitionsUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(163, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Repetitions";
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 279);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.repetitionsUpDown);
            this.Controls.Add(this.activeUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.configUpDown);
            this.Controls.Add(this.timeoutUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.handComboBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.cancelButton);
            this.Name = "Setup";
            this.Text = "Setup";
            ((System.ComponentModel.ISupportInitialize)(this.timeoutUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.configUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.activeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repetitionsUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ComboBox handComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown timeoutUpDown;
        private System.Windows.Forms.NumericUpDown configUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown activeUpDown;
        private System.Windows.Forms.NumericUpDown repetitionsUpDown;
        private System.Windows.Forms.Label label5;
    }
}