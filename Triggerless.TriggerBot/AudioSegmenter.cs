using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System.ComponentModel;

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

        public void RunFFmpeg(string ffmpegLocation, string inputFile, string outputFile, int quality)
        {
            if (quality < 1 || quality > 10) throw new ArgumentException("Quality can only be from 1 to 10");
            // Prepare the ffmpeg command
            string arguments = $"-i \"{inputFile}\" -c:a libvorbis -q:a {quality} \"{outputFile}\"";

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
