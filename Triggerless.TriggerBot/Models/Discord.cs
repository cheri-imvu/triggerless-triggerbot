using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Triggerless.TriggerBot.Models
{
    public class Discord
    {
        public static async Task<string> GetInviteLink()
        {
            string result = string.Empty;
            using (var client = new HttpClient())
            {
                var code = await client.GetStringAsync("https://triggerless.com/triggerbot/invite-code.txt").ConfigureAwait(false);
                result = $"https://discord.gg/{code}";
            }
            return result;
        }

        private static async Task<int> SendMessageToBotAsync(string apiUrl, string title, string body)
        {
            var result = -1;
            var payload = new
            {
                title = title,
                body = body
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Message sent successfully!");
                        string responseString = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Server says: " + responseString);
                        result = 0;
                    }
                    else
                    {
                        Console.WriteLine("Error: " + response.StatusCode);
                        string error = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Details: " + error);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception occurred:");
                    Console.WriteLine(ex);
                }
            }

            return result;
        }

        public static async Task<int> SendMessage(string title, string body)
        {
            return await SendMessageToBotAsync(
                "http://localhost:61120/api/bot/sendmessage",
                title,
                body
            );
        }
    }
}
