namespace ProjectReferencesConverter
{
    partial class DupesDialog
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
            this.DupesListBox = new System.Windows.Forms.ListBox();
            this.AcceptanceButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SkipButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            this.DupesLabel = new System.Windows.Forms.Label();
            this.ProjectLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DupesListBox
            // 
            this.DupesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DupesListBox.FormattingEnabled = true;
            this.DupesListBox.Location = new System.Drawing.Point(12, 56);
            this.DupesListBox.Name = "DupesListBox";
            this.DupesListBox.Size = new System.Drawing.Size(728, 56);
            this.DupesListBox.TabIndex = 2;
            this.DupesListBox.SelectedIndexChanged += new System.EventHandler(this.DupesListBox_SelectedIndexChanged);
            // 
            // AcceptanceButton
            // 
            this.AcceptanceButton.AutoSize = true;
            this.AcceptanceButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AcceptanceButton.Location = new System.Drawing.Point(8, 8);
            this.AcceptanceButton.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.AcceptanceButton.Name = "AcceptanceButton";
            this.AcceptanceButton.Padding = new System.Windows.Forms.Padding(4);
            this.AcceptanceButton.Size = new System.Drawing.Size(75, 31);
            this.AcceptanceButton.TabIndex = 4;
            this.AcceptanceButton.Text = "OK";
            this.AcceptanceButton.UseVisualStyleBackColor = true;
            this.AcceptanceButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.AcceptanceButton);
            this.flowLayoutPanel1.Controls.Add(this.SkipButton);
            this.flowLayoutPanel1.Controls.Add(this.AbortButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(491, 112);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(249, 39);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // SkipButton
            // 
            this.SkipButton.AutoSize = true;
            this.SkipButton.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.SkipButton.Location = new System.Drawing.Point(91, 8);
            this.SkipButton.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.SkipButton.Name = "SkipButton";
            this.SkipButton.Padding = new System.Windows.Forms.Padding(4);
            this.SkipButton.Size = new System.Drawing.Size(75, 31);
            this.SkipButton.TabIndex = 5;
            this.SkipButton.Text = "Skip";
            this.SkipButton.UseVisualStyleBackColor = true;
            this.SkipButton.Click += new System.EventHandler(this.SkipButton_Click);
            // 
            // AbortButton
            // 
            this.AbortButton.AutoSize = true;
            this.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.AbortButton.Location = new System.Drawing.Point(174, 8);
            this.AbortButton.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Padding = new System.Windows.Forms.Padding(4);
            this.AbortButton.Size = new System.Drawing.Size(75, 31);
            this.AbortButton.TabIndex = 6;
            this.AbortButton.Text = "Abort";
            this.AbortButton.UseVisualStyleBackColor = true;
            this.AbortButton.Click += new System.EventHandler(this.AbortButton_Click);
            // 
            // DupesLabel
            // 
            this.DupesLabel.AutoSize = true;
            this.DupesLabel.Location = new System.Drawing.Point(12, 31);
            this.DupesLabel.Name = "DupesLabel";
            this.DupesLabel.Size = new System.Drawing.Size(354, 13);
            this.DupesLabel.TabIndex = 1;
            this.DupesLabel.Text = "The following candidate files were found. Please select one and click OK:";
            // 
            // ProjectLabel
            // 
            this.ProjectLabel.AutoSize = true;
            this.ProjectLabel.Location = new System.Drawing.Point(12, 9);
            this.ProjectLabel.Name = "ProjectLabel";
            this.ProjectLabel.Size = new System.Drawing.Size(171, 13);
            this.ProjectLabel.TabIndex = 0;
            this.ProjectLabel.Text = "Ambiguous reference in Project {0}";
            // 
            // DupesDialog
            // 
            this.AcceptButton = this.AcceptanceButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.SkipButton;
            this.ClientSize = new System.Drawing.Size(752, 160);
            this.Controls.Add(this.ProjectLabel);
            this.Controls.Add(this.DupesLabel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.DupesListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DupesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ambiguous Reference Found";
            this.Load += new System.EventHandler(this.DupeResolutionForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox DupesListBox;
        private System.Windows.Forms.Button AcceptanceButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button SkipButton;
        private System.Windows.Forms.Label DupesLabel;
        private System.Windows.Forms.Button AbortButton;
        private System.Windows.Forms.Label ProjectLabel;
    }
}