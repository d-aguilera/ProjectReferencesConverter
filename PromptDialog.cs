using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectReferencesConverter
{
    public partial class PromptDialog : Form
    {
        public static DialogResult Prompt(string text, out string value)
        {
            return Prompt(text, "Specify Value", out value);
        }

        public static DialogResult Prompt(string text, string caption, out string value)
        {
            return Prompt(text, caption, string.Empty, out value);
        }

        public static DialogResult Prompt(string text, string caption, string initialValue, out string value)
        {
            return Prompt(text, caption, initialValue, 0, out value);
        }

        public static DialogResult Prompt(string text, string caption, string initialValue, int width, out string value)
        {
            using (var dialog = new PromptDialog(text, caption, initialValue, width))
            {
                var result = dialog.ShowDialog();
                value = dialog.Value;
                return result;
            }
        }

        PromptDialog(string text, string caption, string initialValue, int width)
        {
            InitializeComponent();

            Text = caption;
            PromptLabel.Text = text;
            PromptTextBox.Text = initialValue;
            if (width > 0)
                Width = width;
        }

        public string Value
        {
            get;
            private set;
        }

        void AcceptanceButton_Click(object sender, EventArgs e)
        {
            Value = PromptTextBox.Text;
        }

        void CancelationButton_Click(object sender, EventArgs e)
        {
            Value = null;
        }
    }
}
