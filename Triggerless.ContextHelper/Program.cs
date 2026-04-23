using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using Triggerless.PlugIn;

class Program
{
    static int Main(string[] args)
    {
        if (args.Length == 0)
            return 1;

        string file = args[0];

        if (!File.Exists(file))
            return 2;

        // Try to send to Triggerbot
        if (SendIpcToTriggerbot(file))
            return 0;

        // Not running -> launch Triggerbot with argument
        StartTriggerbot(file);
        return 0;
    }

    static bool SendIpcToTriggerbot(string file)
    {
        try
        {
            using (var client = new NamedPipeClientStream(".", "TriggerbotPipe", PipeDirection.Out))
            {
                client.Connect(250); // small timeout

                using (var writer = new StreamWriter(client))
                {
                    writer.WriteLine("OPEN:" + file);
                }

                return true;
            }
        }
        catch
        {
            return false; // Triggerbot not running
        }
    }

    static void StartTriggerbot(string file)
    {
#if DEBUG
        string exe = @"D:\DEV\CS\triggerless-triggerbot\Triggerless.TriggerBot\bin\x64\Debug\TriggerBot.exe";
#else
        string exe = Location.TriggerbotExePath;
#endif

        Process.Start(new ProcessStartInfo
        {
            FileName = exe,
            Arguments = "\"" + file + "\"",
            UseShellExecute = false
        });
    }
}