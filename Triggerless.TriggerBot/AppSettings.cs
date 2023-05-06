using ManagedWinapi.Windows.Contents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Triggerless.TriggerBot
{
    public partial class TriggerBotMainForm
    {
        public class AppSettings
        {

        }

        public class TriggerBot
        {
            public WindowSettings Window { get; set; }

            public TriggerBot() 
            { 
                Window = new WindowSettings();
            }

        }
        public class WindowSettings
        {
            public Point Location { get; set; }
            public Size Size { get; set; }
        }

        private string AppData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private string AppSettingsFile => Path.Combine(AppData, "Triggerless", "TriggerBot");

        private void SaveSettings(object sender, EventArgs e)
        {
            if (File.Exists(AppSettingsFile))
            {
                File.Delete(AppSettingsFile);
            }

            var ser = new XmlSerializer(typeof(TriggerBot));
            //ser.Serialize()

        }

    }

}
