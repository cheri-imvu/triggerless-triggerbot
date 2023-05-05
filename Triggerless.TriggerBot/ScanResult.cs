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

        public enum ScanResultType
        {
            Success = 0,
            DatabaseError = 1,
            NetworkError = 2,
            DecodingError = 3,
            SystemError = 4,
            Pending = 5,
            JsonError = 6,
            NoUsefulTriggers = 7,
            XmlError = 8,
        }
    }
}
