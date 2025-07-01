using Microsoft.Win32;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Triggerless.TriggerBot
{
    public class AudioSegmenter: Component
    {
        public void AddDirectoryToUserPath(string directoryToAdd)
        {
            // Retrieve the user's PATH environment variable from the registry
            RegistryKey envKey = Registry.CurrentUser.OpenSubKey(@"Environment", true);
            string userPath = (string)envKey.GetValue("Path", string.Empty);

            // Split the PATH string into an array of directories
            string[] pathDirectories = userPath.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // Check if the directory is already in the PATH
            bool directoryExistsInPath = pathDirectories.Any(dir => dir.Equals(directoryToAdd, StringComparison.OrdinalIgnoreCase));

            // Add the directory to the PATH if it's not there
            if (!directoryExistsInPath)
            {
                userPath += $";{directoryToAdd}";
                envKey.SetValue("Path", userPath);
                Console.WriteLine($"Directory '{directoryToAdd}' has been added to the user's PATH.");
            }
            else
            {
                Console.WriteLine($"Directory '{directoryToAdd}' is already in the user's PATH.");
            }

            // Close the registry key
            envKey.Close();
        }


        public int SegmentBySmartCut(string inputFilePath, string outputDir, string filenamePrefix)
        {
            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException("Input MP3 file not found.", inputFilePath);

            Directory.CreateDirectory(outputDir);

            List<float> envelope = new List<float>();
            int sampleRate;
            TimeSpan totalDuration;

            // Step 1: Analyze amplitude
            using (var reader = new AudioFileReader(inputFilePath))
            {
                sampleRate = reader.WaveFormat.SampleRate;
                totalDuration = reader.TotalTime;
                int frameSize = sampleRate / 100; // 10ms per frame
                float[] buffer = new float[frameSize];

                int samplesRead;
                while ((samplesRead = reader.Read(buffer, 0, frameSize)) > 0)
                {
                    float sumSq = 0;
                    for (int i = 0; i < samplesRead; i++)
                        sumSq += buffer[i] * buffer[i];

                    float rms = (float)Math.Sqrt(sumSq / samplesRead);
                    envelope.Add(rms);
                }
            }

            // Step 2: Identify cut points
            List<double> cutTimes = new List<double> { 0.0 };
            int minFrames = 15 * 100;
            int maxFrames = 20 * 100;
            int startFrame = 0;

            while (startFrame + minFrames < envelope.Count)
            {
                int searchEnd = Math.Min(envelope.Count, startFrame + maxFrames);
                float bestAvg = float.MaxValue;
                int bestCenter = -1;

                for (int i = startFrame + minFrames; i < searchEnd; i++)
                {
                    int windowRadius = 10;
                    int winStart = Math.Max(i - windowRadius, 0);
                    int winEnd = Math.Min(i + windowRadius, envelope.Count);
                    float avg = envelope.Skip(winStart).Take(winEnd - winStart).Average();

                    if (avg < bestAvg)
                    {
                        bestAvg = avg;
                        bestCenter = i;
                    }
                }

                if (bestCenter != -1)
                {
                    double cutTime = bestCenter * 0.01;
                    cutTimes.Add(cutTime);
                    startFrame = bestCenter;
                }
                else
                {
                    double fallbackCut = (startFrame + maxFrames) * 0.01;
                    cutTimes.Add(fallbackCut);
                    startFrame += maxFrames;
                }
            }

            cutTimes.Add(totalDuration.TotalSeconds);

            // Step 3: Export segments
            int successfulExports = 0;

            for (int i = 0; i < cutTimes.Count - 1; i++)
            {
                double start = cutTimes[i];
                double duration = cutTimes[i + 1] - start;

                // Skip if duration is less than 0.5 seconds
                if (duration < 0.5)
                    continue;

                string outputFile = Path.Combine(outputDir, string.Format("{0}{1}.wav", filenamePrefix, successfulExports + 1));

                var psi = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = string.Format("-y -i \"{0}\" -ss {1:F3} -t {2:F3} -ac 2 -ar 44100 \"{3}\"",
                        inputFilePath, start, duration, outputFile),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(psi))
                {
                    if (process != null)
                    {
                        string err = process.StandardError.ReadToEnd();
                        process.WaitForExit();

                        if (process.ExitCode == 0)
                        {
                            successfulExports++;
                        }
                        else
                        {
                            Console.WriteLine("FFmpeg error on segment {0}: {1}", i + 1, err);
                        }
                    }
                }
            }

            return successfulExports;
        }

        public void SegmentAudio(string inputFilePath, string outputDirectory, TimeSpan segmentDuration, string outputFileNamePrefix)
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

                int segmentIndex = 1;

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
                        string outputFilePath = Path.Combine(outputDirectory, $"{outputFileNamePrefix}{segmentIndex:000}.wav");

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

        public class OutputEventArgs : EventArgs
        {
            public string Data { get; set; }
        }

        public delegate void OutputEventHandler(object sender, OutputEventArgs e);
        public static event OutputEventHandler OutputReceived;
        public static event OutputEventHandler ErrorReceived;

        /*
        -c codec            codec name
        -b:a bitrate        audio bitrate
        -ar rate            set audio sampling rate (in Hz)
        -vol volume         change audio volume (256=normal)
        -ac channels        set number of audio channels
        -q:a quality        set audio quality (codec-specific)      
         */

        public void RunFFmpeg(string ffmpegLocation, string inputFile, string outputFile, int option, double volume = 1.0)
        {

            string[] options = { 
            //Options
            // 7.8 min per 2MB file
                $"-b:a 32k -ar 22050",
            // 6.4 min per 2MB file
                $"-b:a 44k -ar 22050",
            // 4.6 min per 2MB file
                $"-q:a 1 -ac 1",
            // 3.6 min per 2MB file
                $"-q:a 1",
            };

            var volumeOption = volume == 1.0 ? "" : $" -filter:a \"volume={volume}\"";

            string arguments = $"-i \"{inputFile}\" -c:a libvorbis {options[option]}{volumeOption} \"{outputFile}\"";

            // Create a ProcessStartInfo object
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = $"{ffmpegLocation}\\ffmpeg.exe",
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            // debug output
            Debug.WriteLine(startInfo.FileName);
            Debug.WriteLine(File.Exists(startInfo.FileName));
            Debug.WriteLine(inputFile);
            Debug.WriteLine(File.Exists(inputFile));
            Debug.WriteLine(outputFile);
            Debug.WriteLine(arguments);

            // Start the process
            using (Process process = new Process { StartInfo = startInfo })
            {
                // Set up event handlers for output and error data received
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        OutputReceived?.Invoke(sender, new OutputEventArgs { Data = e.Data });
                    }
                };
                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        ErrorReceived?.Invoke(sender, new OutputEventArgs { Data = e.Data });
                    }
                };

                // Subscribe to the events
                //OutputReceived += OnOutputReceived;
                //ErrorReceived += OnErrorReceived;

                // Start the process and begin reading output and error data asynchronously
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // Wait for the process to exit
                process.WaitForExit();
            }
        }

        public static void OpenFileExplorerAndHighlight(string fileName)
        {
            if (File.Exists(fileName))
            {
                string arguments = $"/select, \"{fileName}\"";
                Process.Start("explorer.exe", arguments);
            }
            else
            {
                Console.WriteLine("File not found: " + fileName);
            }
        }

    }

}
