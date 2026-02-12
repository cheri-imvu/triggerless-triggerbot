using System;
using System.Collections.Generic;

namespace Triggerless.TriggerBot.Models
{
    public class TriggerEntry
    {
        public long ProductId { get; set; }
        public string TriggerName { get; set; }
        public string OggName { get; set; }
        public string Location { get; set; }
        public int Sequence { get; set; }
        public string Prefix { get; set; }
        public double LengthMS { get; set; }
        public double WaitMS { get; set; }
    }

    public class ProductRecord
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string CreatorName { get; set; }
        public string ImageLocation { get; set; }
        public byte[] ImageBytes { get; set; }
        public DateTime DateCreated { get; set; }
        public long AddedBy { get; set; }
    }

    public class CollectorPayload : ProductRecord
    {
        public List<TriggerEntry> Triggers { get; set; } = new List<TriggerEntry>();
    }

    public class CollectorResponsePayload : CollectorPayload
    {
        public ScanResultType Result { get; set; }
        public string Message { get; set; }
    }
}
