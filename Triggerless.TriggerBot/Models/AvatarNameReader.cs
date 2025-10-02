
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Triggerless.TriggerBot.Models;

namespace Triggerless.TriggerBot
{
    public static class AvatarNameReader
    {
        public static bool TryGetAvatarName(out string name)
        {
            name = null;
            return true;
        }

        public static long GetAvatarId()
        {
            var firstFilename = "IMVULog.log";
            var logNumber = 0;

            // IMVULog.log, IMVULog.log.1, ... IMVULog.log.5 (last one)
            Func<string> logFile = () => Path.Combine(
                Shared.ImvuFileLocation,           // same dir as productInfoCache.db
                firstFilename + (logNumber == 0 ? "" : $".{logNumber}"));

            // These patterns are specific for the currently logged in IMVU member's cid
            var patterns = new[]
            {
                // https://api.imvu.com/users/{cid}/inventory_lists
                @"https://api\.imvu\.com/users/(?<cid>\d+)/inventory_lists",

                // https://api.imvu.com/inventory_lists/{cid}-{some number}
                @"https://api\.imvu\.com/inventory_lists/(?<cid>\d+)-\d+",

                // ...cid=123 anywhere in text (case-insensitive), shows in URLs
                @"\bcid=(?<cid>\d+)\b",

                //Request with userId,key,auth = (83079851,
                @"Request with userId,key,auth = \((?<cid>\d+),",

                //INFO: --> test.getBuddyState(83079851,
                @"INFO: --> test\.getBuddyState\((?<cid>\d+),"
            };
            long result = 0;

            while (result == 0)
            {
                var logText = ReadAllTextUnlocked(logFile());
                foreach (var pattern in patterns)
                {
                    var matches = Regex.Matches(logText, pattern, RegexOptions.IgnoreCase).Cast<Match>();
                    if (!matches.Any()) continue;
                    var successes = matches.Where(m => m.Success);
                    if (!successes.Any()) continue;
                    var finds = successes.Select(m => m.Groups["cid"].Value);
                    var mostCommon = finds.FindMostPrevalentItem();
                    long thisCid = 0;
                    if (long.TryParse(mostCommon, out thisCid))
                    {
                        if (thisCid > 0)
                        {
                            result = thisCid;
                            break;
                        }
                    }
                }
                if (result >  0) break;
                logNumber++;
            }

            return result;
        }

        private static string ReadAllTextUnlocked(string path)
        {
            var encoding = Encoding.UTF8; // adjust if your log uses a different encoding
            using (var fs = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite | FileShare.Delete,         // <-- key bit
                bufferSize: 4096,
                options: FileOptions.SequentialScan))
            {
                using (var reader = new StreamReader(fs, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }

}
