namespace Triggerless.TriggerBot
{
    public partial class Collector
    {
        public class TriggerResult
        {
            public int Sequence { get; set; }
            public string TriggerName { get; set; }
            public string OggName { get; set; }
            public double LengthMS { get; set; }
        }
    }
}
