using System;

namespace Triggerless.TriggerBot.Models
{
    public enum ReportPayloadType
    {
        None = 0,
        AppInitialize,
        AppShown,
        AppClosing,
        SongPlaying,
        ChknCreated,
        LyricsSaved,
    }

    public class ReportPayload
    {
        public string SessionId { get; set; }
        public string AvatarName { get; set; }
        public string UniqueId { get; set; } 
        public DateTime CreationTime { get; set; }        
        public ReportPayloadType Type { get; set; }
        public string JsonText { get; set; }
    }
}
