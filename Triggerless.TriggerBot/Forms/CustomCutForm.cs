using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Forms
{
    public partial class CustomCutForm : Form
    {
        public List<Cut> Cuts { get; set; }
        public string AudioFilePath { get; set; }

        public CustomCutForm()
        {
            InitializeComponent();
        }

        private void CustomCutForm_Load(object sender, EventArgs e)
        {
            if (AudioFilePath == null) return;
            AudioFilePath = AudioFilePath.Trim();
            if (!System.IO.File.Exists(AudioFilePath)) return;
            waveformEditorControl1.LoadAudio(AudioFilePath);
        }

        private string GetTimeString(double seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return time.ToString(@"mm\:ss\.ff");
        }

        private void CutsChanged(object sender, CutsChangedEventArgs e)
        {
            Cuts = e.Cuts;
            grdCuts.Rows.Clear();
            
            foreach (Cut cut in e.Cuts)
            {
                int rowIndex = grdCuts.Rows.Add(
                    cut.Index,
                    GetTimeString(cut.StartTimeSeconds), 
                    GetTimeString(cut.EndTimeSeconds),
                    GetTimeString(cut.LengthSeconds)
                );
                if (cut.LengthSeconds > 20)
                {
                    grdCuts.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                }
            }

            if (grdCuts.Rows.Count > 0)
            {
                int lastRowIndex = grdCuts.Rows.Count - 1;
                grdCuts.FirstDisplayedScrollingRowIndex = lastRowIndex;
                grdCuts.CurrentCell = grdCuts.Rows[lastRowIndex].Cells[0];
            }
        }

        private bool ValidateCuts()
        {
            if (Cuts == null || Cuts.Count == 0)
            {
                StyledMessageBox.Show(Program.MainForm, "No cuts defined. Please define at least one cut.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            foreach (Cut cut in Cuts)
            {
                if (cut.LengthSeconds > 20)
                {
                    StyledMessageBox.Show(Program.MainForm, $"Cut {cut.Index} has invalid length. Please ensure all cuts have a length of 20 seconds or less.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (!ValidateCuts()) return;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; 
            Close();
        }
    }
}
