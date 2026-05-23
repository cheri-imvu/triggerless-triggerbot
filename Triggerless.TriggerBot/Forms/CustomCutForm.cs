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
    }
}
