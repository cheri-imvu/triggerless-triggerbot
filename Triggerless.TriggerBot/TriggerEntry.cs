namespace Triggerless.TriggerBot
{
    public partial class Collector
    {
        public class TriggerEntry
        {
            public long ProductId { get; set; }
            public string TriggerName { get; set; }
            public string OggName { get; set; }
            public double LengthMS { get; set; }
            public int Sequence { get; set; }
            public string Prefix { get; set; }
        }
    }
}
