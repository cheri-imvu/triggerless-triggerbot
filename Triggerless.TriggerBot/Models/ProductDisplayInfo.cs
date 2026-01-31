using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Triggerless.TriggerBot
{
    public class ProductDisplayInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public byte[] ImageBytes { get; set; }
        public List<TriggerDisplayInfo> Triggers { get; set; }
        public List<ProductTag> Tags { get; set; }
        
        public ProductDisplayInfo() { 
            Triggers = new List<TriggerDisplayInfo>();
            Tags = new List<ProductTag>();
        }

        public string LyricsPath => Path.Combine(PlugIn.Location.LyricSheetsPath, $"{Id}.lyrics");
        public bool HasLyrics => File.Exists(LyricsPath);
        public List<LyricEntry> Lyrics => JsonConvert.DeserializeObject<List<LyricEntry>>(File.ReadAllText(LyricsPath));
    }

    public class TriggerDisplayInfo
    {
        public long ProductId { get; set; }
        public int Sequence { get; set; }
        public string Prefix { get; set; }
        public string Trigger { get; set; }
        public double LengthMS { get; set; }
        public string Location { get; set; }
        public string AddnTriggers { get; set; }

    }
    
    public class ProductTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
