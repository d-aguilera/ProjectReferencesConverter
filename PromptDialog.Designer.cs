namespace ProjectReferencesConverter
{
    partial class PromptDialog
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
            this.PromptLabel = new System.Windows.Forms.Label();
            this.PromptTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.AcceptanceButton = new System.Windows.Forms.Button();
            this.CancelationButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PromptLabel
            // 
            this.PromptLabel.AutoSize = true;
            this.PromptLabel.Location = new System.Drawing.Point(3, 0);
            this.PromptLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.PromptLabel.Name = "PromptLabel";
            this.PromptLabel.Size = new System.Drawing.Size(66, 13);
            this.PromptLabel.TabIndex = 1;
            this.PromptLabel.Text = "PromptLabel";
            // 
            // PromptTextBox
            // 
            this.PromptTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PromptTextBox.Location = new System.Drawing.Point(3, 22);
            this.PromptTextBox.Name = "PromptTextBox";
            this.PromptTextBox.Size = new System.Drawing.Size(293, 20);
            this.PromptTextBox.TabIndex = 2;
            this.PromptTextBox.Text = "PromptTextBox";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.AcceptanceButton);
            this.flowLayoutPanel1.Controls.Add(this.CancelationButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(133, 48);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(166, 39);
            this.flowLayoutPanel1.TabIndex = 3;
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
            this.AcceptanceButton.Click += new System.EventHandler(this.AcceptanceButton_Click);
            // 
            // CancelationButton
            // 
            this.CancelationButton.AutoSize = true;
            this.CancelationButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelationButton.Location = new System.Drawing.Point(91, 8);
            this.CancelationButton.Margin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.CancelationButton.Name = "CancelationButton";
            this.CancelationButton.Padding = new System.Windows.Forms.Padding(4);
            this.CancelationButton.Size = new System.Drawing.Size(75, 31);
            this.CancelationButton.TabIndex = 5;
            this.CancelationButton.Text = "Cancel";
            this.CancelationButton.UseVisualStyleBackColor = true;
            this.CancelationButton.Click += new System.EventHandler(this.CancelationButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.PromptLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.PromptTextBox, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(299, 87);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // PromptDialog
            // 
            this.AcceptButton = this.AcceptanceButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.CancelationButton;
            this.ClientSize = new System.Drawing.Size(323, 105);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromptDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PromptDialog";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PromptLabel;
        private System.Windows.Forms.TextBox PromptTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button AcceptanceButton;
        private System.Windows.Forms.Button CancelationButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}