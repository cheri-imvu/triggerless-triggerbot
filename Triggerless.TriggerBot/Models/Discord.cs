using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Triggerless.TriggerBot.Models
{
    public class Discord
    {
        public enum ResultStatus
        {
            NotSet = -1, Success = 0, NoNetwork = 1, Failed = 2, FailedToConnect = 3,
        }

        public class Result
        {
            public ResultStatus Status { get; set; } = ResultStatus.NotSet;
            public string Message { get; set; } = string.Empty;
            public Exception Exception { get; set; }
        }

        public static async Task<string> GetInviteLink()
        {
            string result = string.Empty;
            string code = "mVjsqJZenQ";
            try
            {
                if (Common.HasTriggerlessConnection)
                using (var client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 3);
                    code = await client.GetStringAsync($"{PlugIn.Location.TriggerlessDomain}/triggerbot/invite-code.txt").ConfigureAwait(false);
                }
            }
            catch (Exception) 
            { 
                Common.HasTriggerlessConnection = false;
            }

            result = $"https://discord.gg/{code}";
            return result;
        }

        private static async Task<Result> SendMessageToBotAsync(string apiUrl, string title, string body)
        {
            var result = new Result();
            if (!Common.HasTriggerlessConnection)
            {
                result.Status = ResultStatus.NoNetwork;
                result.Message = "Unable to Connect";
                return result;
            }
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
                    client.DefaultRequestHeaders.Add("CustomerId", Program.Cid.ToString());
                    var response = await client.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        result.Status = ResultStatus.Success;
                        result.Message = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        result.Status = ResultStatus.Failed;
                        string error = await response.Content.ReadAsStringAsync();
                        result.Message = $"{response.StatusCode}: {error}";
                    }
                }
                catch (Exception ex)
                {
                    result.Status = ResultStatus.Failed;
                    result.Message = ex.Message;
                    result.Exception = ex;
                }
            }

            return result;
        }

        public static async Task<Result> SendMessage(string title, string body)
        {
            return await SendMessageToBotAsync(
                $"{PlugIn.Location.TriggerlessDomain}/api/bot/sendmessage",
                title,
                body
            );
        }
    }
}
