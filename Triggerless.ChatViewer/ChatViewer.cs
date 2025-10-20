using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Triggerless.ChatViewer;

namespace Triggerless.ChatViewerPlugIn
{
    public partial class ChatViewer : UserControl
    {
        public ChatViewer()
        {
            InitializeComponent();
        }

        private void Reload(object sender, EventArgs e)
        {
            grdChat.Rows.Clear();
            int j = 5;
            var dir = PlugIn.Location.ImvuFileLocation;

            var pattern = @"notifyNewMessage called for (\d+)/u'(.*?)'/";
            var names = new Dictionary<long, string>();
            var rx = new Regex(pattern);
            var chats = new List<Chat>();
            var baseUrl = $"{PlugIn.Location.TriggerlessDomain}/api/user/";
            var client = new HttpClient();
            const int CID_INDEX = 1;
            const int TEXT_INDEX = 2;

            do
            {
                var filename = "IMVULog.log" + ((j == 0) ? "" : $".{j}");
                var lines = PlugIn.Shared.ReadAllLinesUnlocked(Path.Combine(dir, filename));
                foreach (string line in lines)
                {
                    var m = rx.Match(line);
                    if (m.Success)
                    {
                        var what = DecodeUnicodeEscapes(m.Groups[TEXT_INDEX].Value.Trim());
                        var cid = long.Parse(m.Groups[CID_INDEX].Value);
                        if (!names.ContainsKey(cid))
                        {
                            var url = $"{baseUrl}{cid}";
                            var json = client.GetStringAsync(url).Result;
                            var u = new user();
                            JsonConvert.PopulateObject(json, u);
                            names[cid] = u.avatarname;
                        }
                        var who = names[cid];
                        if (!what.StartsWith("*"))
                        {
                            chats.Add(new Chat { Who = who, What = what });
                        }

                    }
                }
                j--;

            } while (j > -1);

            var output = Path.Combine(dir, "chat-dump.txt");
            if (File.Exists(output)) File.Delete(output);
            var sb = new StringBuilder();
            foreach (var chat in chats)
            {
                sb.AppendLine($"{chat.Who}: {chat.What}");
                grdChat.Rows.Add(chat.Who, chat.What);
            }
            File.WriteAllText(output, sb.ToString());


        }

        static string DecodeUnicodeEscapes(string input)
        {
            // 1) \UXXXXXXXX (code points > U+FFFF)
            input = Regex.Replace(input, @"\\U([0-9A-Fa-f]{8})", m =>
            {
                var codePoint = (int)uint.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                return char.ConvertFromUtf32(codePoint); // returns surrogate pair if needed
            });

            // 2) \uFFFF (BMP)
            input = Regex.Replace(input, @"\\u([0-9A-Fa-f]{4})", m =>
            {
                var codeUnit = int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                return ((char)codeUnit).ToString();
            });

            return input;
        }
    }
}
