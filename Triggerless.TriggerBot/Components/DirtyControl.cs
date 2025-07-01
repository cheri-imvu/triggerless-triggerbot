using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Components
{
    public partial class DirtyControl : UserControl
    {
        private bool _dirty;

        public DirtyControl()
        {
            InitializeComponent();
        }

        public bool Dirty { 
            get => _dirty; 
            set
            {
                _dirty = value;
                picDirty.Visible = _dirty;
                picClean.Visible = !_dirty;
            }
        }

        private void DirtyControl_Load(object sender, EventArgs e)
        {

        }

        private void picDirty_Click(object sender, EventArgs e)
        {

        }
    }
}
