using Newtonsoft.Json;

namespace Triggerless.TriggerBot
{
    public partial class Collector
    {
        public class ContentsJsonItem
        {
            [JsonProperty("url")] public string Url { get; set; }
            [JsonProperty("original_dimensions")] public string OriginalDimensions { get; set; }
            [JsonProperty("name")] public string Name { get; set; }
            [JsonProperty("tags")] public string[] Tags { get; set; }
            public string Location => string.IsNullOrWhiteSpace(Url) ? Name : Url;
        }
    }
}
