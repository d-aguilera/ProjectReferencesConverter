using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjectReferencesConverter
{
    public partial class DupesDialog : Form
    {
        public static DialogResult Prompt(IEnumerable<string> dupes, string project, out string selected)
        {
            using (var dupesForm = new DupesDialog(dupes, project))
            {
                var result = dupesForm.ShowDialog();
                selected = dupesForm.Selected;
                return result;
            }
        }

        public IEnumerable<string> Dupes
        {
            get;
            private set;
        }

        public string Project
        {
            get;
            private set;
        }

        public string Selected
        {
            get;
            private set;
        }

        DupesDialog(IEnumerable<string> dupes, string project)
        {
            InitializeComponent();

            Dupes = dupes;
            Project = project;

            ProjectLabel.Text = string.Format(ProjectLabel.Text, Project);

            DupesListBox.Items.Clear();
            DupesListBox.Items.AddRange(Dupes.ToArray());

            Selected = null;
        }

        void AcceptButton_Click(object sender, EventArgs e)
        {
            Selected = (string)DupesListBox.SelectedItem;
        }

        void SkipButton_Click(object sender, EventArgs e)
        {
            Selected = null;
        }

        void AbortButton_Click(object sender, EventArgs e)
        {
            Selected = null;
        }

        void DupesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AcceptanceButton.Enabled = DupesListBox.SelectedIndex >= 0;
        }

        void DupeResolutionForm_Load(object sender, EventArgs e)
        {
            if (DupesListBox.Items.Count > 0)
                DupesListBox.SelectedIndex = 0;
        }
    }
}
