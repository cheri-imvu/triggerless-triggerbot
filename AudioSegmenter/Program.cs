using System;
using System.IO;
using System.Linq;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Text.RegularExpressions;

namespace AudioSegmenter
{
    class Program
    {


        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: AudioSegmenter <input_file>");
                return;
            }

            string inputFilePath = args[0];
            string outputDirectory = Path.GetDirectoryName(inputFilePath);
            TimeSpan segmentDuration = TimeSpan.FromSeconds(19.9);
            int quality = 5; // 5 represents medium quality
            var outputFileNamePrefix = "xyz";

            SegmentAudio(inputFilePath, outputDirectory, segmentDuration, quality, outputFileNamePrefix);

            Console.WriteLine("Segmentation complete.");
        }


        static void SegmentAudio(string inputFilePath, string outputDirectory, TimeSpan segmentDuration, int quality, string outputFileNamePrefix)
        {
            if (string.IsNullOrWhiteSpace(outputFileNamePrefix))
            {
                throw new ArgumentException("Output filename prefix cannot be empty.");
            }

            // Remove any spaces, commas, numbers, or DOS-forbidden characters
            outputFileNamePrefix = Regex.Replace(outputFileNamePrefix, @"[\s,0-9<>:""/\\|?*]", "");

            using (var reader = new AudioFileReader(inputFilePath))
            {
                int targetSampleRate = 48000;
                var resampler = new WdlResamplingSampleProvider(reader, targetSampleRate);

                int segmentIndex = 0;

                while (reader.Position < reader.Length)
                {
                    int samplesToRead = (int)(segmentDuration.TotalSeconds * resampler.WaveFormat.SampleRate * resampler.WaveFormat.Channels);
                    float[] buffer = new float[samplesToRead];
                    int samplesRead = 0;
                    int totalSamplesRead = 0;

                    while (totalSamplesRead < samplesToRead)
                    {
                        samplesRead = resampler.Read(buffer, totalSamplesRead, samplesToRead - totalSamplesRead);
                        if (samplesRead == 0) break;
                        totalSamplesRead += samplesRead;
                    }

                    // Ensure the segment duration is at least 1% of the desired segment duration
                    if (totalSamplesRead < samplesToRead / 100) break;

                    if (totalSamplesRead > 0)
                    {
                        string outputFilePath = Path.Combine(outputDirectory, $"{outputFileNamePrefix}_segment{segmentIndex:000}.wav");

                        using (var fileStream = File.Create(outputFilePath))
                        {
                            // Write the PCM data to WAV format
                            using (var wavWriter = new WaveFileWriter(fileStream, resampler.WaveFormat))
                            {
                                wavWriter.WriteSamples(buffer, 0, totalSamplesRead);
                            }
                        }
                    }

                    segmentIndex++;
                }
            }
        }

    }
}
