using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Triggerless.TriggerBot.Models;

namespace Triggerless.TriggerBot.Components
{
    public partial class AutoDJCtrl : UserControl
    {
        public AutoDJCtrl()
        {
            InitializeComponent();
        }

        public LiveList<ProductDisplayInfo> LiveList { get; set; }
        public bool IsLive { get; set; }
    }
}
