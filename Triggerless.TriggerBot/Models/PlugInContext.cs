using System.Windows.Forms;
using Triggerless.PlugIn;

namespace Triggerless.TriggerBot.Models
{
    public class PlugInContext : IPlugInContext
    {
        private UserControl _userControl;
        public Control Parent { get; set; }
        public UserControl Control
        {
            get => _userControl;
            set => _userControl = value;
        }
        public bool SendTrigger(string trigger)
        {
            if (Program.MainForm  == null) return false;
            Program.MainForm.DispatchText(trigger);
            return true;
        }
    }

}
