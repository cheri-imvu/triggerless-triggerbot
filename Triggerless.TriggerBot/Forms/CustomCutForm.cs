using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Forms
{
    public partial class CustomCutForm : Form
    {
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
            grdCuts.Rows.Clear();
            
            foreach (var cut in e.Cuts)
            {
                double lengthSeconds = cut.EndTimeSeconds - cut.StartTimeSeconds;
                int rowIndex = grdCuts.Rows.Add(
                    cut.Index,
                    GetTimeString(cut.StartTimeSeconds), 
                    GetTimeString(cut.EndTimeSeconds),
                    GetTimeString(lengthSeconds)
                );
                if (lengthSeconds > 20)
                {
                    grdCuts.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                }
            }
        }
    }
}
