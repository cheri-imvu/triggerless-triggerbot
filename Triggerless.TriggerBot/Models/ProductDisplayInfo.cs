using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triggerless.TriggerBot
{
    public class ProductDisplayInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public byte[] ImageBytes { get; set; }
        public List<TriggerDisplayInfo> Triggers { get; set; }
        
        public ProductDisplayInfo() { 
            Triggers = new List<TriggerDisplayInfo>();
        }
    }

    public class TriggerDisplayInfo
    {
        public long ProductId { get; set; }
        public int Sequence { get; set; }
        public string Prefix { get; set; }
        public string Trigger { get; set; }
        public double LengthMS { get; set; }
        public string AddnTriggers { get; set; }

    }
}
