using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triggerless.TriggerBot
{
    public partial class Collector
    {
        public class CollectorEventArgs : EventArgs
        {
            public int TotalProducts { get; set; }
            public int CompletedProducts { get; set; }
            public string Message { get; set; }
        }

        public delegate void CollectorEventHandler(object sender, CollectorEventArgs e);

        public event CollectorEventHandler CollectorEvent;

        public void FireEvent(CollectorEventArgs e)
        {
            CollectorEvent?.Invoke(this, e);
        }
    }
}
