using System.Collections.Generic;

namespace Triggerless.TriggerBot
{
    public partial class Collector
    {
        public class ScanResult {
            public long ProductId { get; set;}
            public ScanResultType Result { get; set; }
            public string Message { get; set; } 
            public List<TriggerResult> TriggerResults { get; set; }
        }
    }
}
