﻿namespace Triggerless.TriggerBot
{
    public class TriggerBotSettings
    {
        public int MainWindowLeft { get; set; }
        public int MainWindowTop { get; set; }
        public int MainWindowWidth { get; set; }
        public int MainWindowHeight { get; set; }
        public bool MainWindowTopMost { get; set; }
        public bool MinimizeOnPlay { get; set; }
        public string[] LastAdditionalTriggers { get; set; }
        public string LastInitialDirectory { get; set; }
        public int AudioQuality { get; set; }
        public string MaxAudioLength { get; set; }
        public bool DefaultToFemale { get; set; }
        public bool GenerateIcons { get; set; }
        public bool CleanUpOggFiles { get; set; }
        public int LagMS { get; set; }

        public string LastTab { get; set; } = "Playback";
    }

}
