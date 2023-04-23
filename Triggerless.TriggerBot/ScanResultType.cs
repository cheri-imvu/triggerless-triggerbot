namespace Triggerless.TriggerBot
{
    public partial class Collector
    {
        public enum ScanResultType
        {
            Success = 0,
            DatabaseError = 1,
            NetworkError = 2,
            DecodingError = 3,
            SystemError = 4,
            Pending = 5,
            JsonError = 6
        }
    }
}
