using System;
using System.Diagnostics;
using System.IO;

namespace Triggerless.TriggerBot
{
    public static class TriggerbotLinker
    {
        /// <summary>
        /// Ensures a directory link named "_Triggerbot" exists at:
        ///   %USERPROFILE%\Documents\IMVU Projects\_Triggerbot
        /// pointing to:
        ///   %USERPROFILE%\Documents\Triggerbot
        /// Returns true if it already existed (as a link) or was created now.
        /// </summary>
        public static bool EnsureTriggerbotJunction()
        {
            string docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string target = Path.Combine(docs, "Triggerbot");
            string linkDir = Path.Combine(docs, "IMVU Projects");
            string link = Path.Combine(linkDir, "_Triggerbot");

            // Make sure parent exists
            Directory.CreateDirectory(linkDir);

            // Optionally ensure the target exists (comment this out if you don't want to create it)
            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);

            // If something already exists where the link should go...
            if (Directory.Exists(link) || File.Exists(link))
            {
                // If it's a reparse point (junction/symlink), assume it's OK
                if (Directory.Exists(link) &&
                    (new DirectoryInfo(link).Attributes & FileAttributes.ReparsePoint) != 0)
                    return true;

                // It's a regular folder or a file with that name — do NOT delete it automatically.
                // Caller can decide how to handle this case.
                return false;
            }

            // Create a junction with: mklink /J "link" "target"
            // (Junctions generally don't require admin, and behave like real directories)
            var psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c mklink /J \"{link}\" \"{target}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = linkDir
            };

            using (var p = Process.Start(psi))
            {
                string stdout = p.StandardOutput.ReadToEnd();
                string stderr = p.StandardError.ReadToEnd();
                p.WaitForExit();

                if (p.ExitCode == 0 && Directory.Exists(link))
                    return true;

                // If you want diagnostics:
                Debug.WriteLine($"mklink exit {p.ExitCode}");
                if (!string.IsNullOrWhiteSpace(stdout)) Debug.WriteLine(stdout);
                if (!string.IsNullOrWhiteSpace(stderr)) Debug.WriteLine(stderr);
                return false;
            }
        }
    }
}
