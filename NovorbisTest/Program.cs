using NVorbis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovorbisTest
{
    internal class Program
    {
        const string OGG_FILE = @"E:\AUDIO\MUSIC\Steely Dan\Aja\New Folder\02-Aja.ogg";

        static void Main(string[] args)
        {
            var dtStart = DateTime.Now;
            using (var fs = File.OpenRead(OGG_FILE)) {
                double result = VorbisReader.GetOggLengthMS(fs);
                var dtEnd = DateTime.Now;
                Console.Write($"{result} ms\nTime to complete: {(dtEnd - dtStart).TotalMilliseconds} ms \nClick any key to exit");
            }
            Console.ReadKey();

        }
    }
}
