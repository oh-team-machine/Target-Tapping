namespace GameLibrary
{
    /// <summary>
    /// 
    /// </summary>
    partial class PatientDatabase
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
            this.patientNameList = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // patientNameList
            // 
            this.patientNameList.FormattingEnabled = true;
            this.patientNameList.Location = new System.Drawing.Point(12, 12);
            this.patientNameList.Name = "patientNameList";
            this.patientNameList.Size = new System.Drawing.Size(211, 238);
            this.patientNameList.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(285, 12);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(88, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add Patient";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(285, 41);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(88, 23);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Delete Patient";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(285, 228);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(88, 23);
            this.selectButton.TabIndex = 3;
            this.selectButton.Text = "Select Patient";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // PatientDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 262);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.patientNameList);
            this.Name = "PatientDatabase";
            this.Text = "Patient Database";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox patientNameList;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button selectButton;
    }
}