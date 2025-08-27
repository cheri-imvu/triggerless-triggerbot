using System;
using System.IO;
using NAudio.Wave;
using NAudio.Vorbis; // OGG via NVorbis

public static class UniversalAudioReader
{
    public static WaveStream Open(string filePath)
    {
        var ext = Path.GetExtension(filePath).ToLowerInvariant();

        switch (ext)
        {
            case ".wav":
                return new WaveFileReader(filePath);

            case ".ogg":
                return new VorbisWaveReader(filePath); // requires NAudio.Vorbis

            case ".mp3":
                // MediaFoundationReader handles mp3 too; fall back to Mp3FileReader if MF not available
                try { return new MediaFoundationReader(filePath); }
                catch { return new Mp3FileReader(filePath); }

            case ".flac":
                // Use Media Foundation FLAC decoder (Win10/11). Throws if codec not present.
                return new MediaFoundationReader(filePath);

            default:
                // Best-effort: let Media Foundation try (aac, wma, m4a, etc.)
                return new MediaFoundationReader(filePath);
        }
    }
}
