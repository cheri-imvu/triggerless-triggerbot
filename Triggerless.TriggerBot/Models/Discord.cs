using DSharpPlus.Entities;
using DSharpPlus;
using System.Threading.Tasks;
using System;

namespace Triggerless.TriggerBot.Models
{
    public class Discord: IDisposable
    {
        private static DiscordClient _client;
        private static ulong _channelId = 1360224553208516638;

        public static async Task<int> CleanupChannel()
        {
            if (_client == null)
            {
                await GetClient().ConfigureAwait(false);
            }
            DiscordChannel channel = await _client.GetChannelAsync(_channelId);
            var messages = await channel.GetMessagesAsync(1000).ConfigureAwait(false);
            foreach (var message in messages)
            { 
                if (message.Embeds.Count > 0) 
                {
                    var embed = message.Embeds[0];
                    if (embed.Title == "Scan Failure")
                    {
                        await message.DeleteAsync("bug message").ConfigureAwait(false);
                    }
                }
        
            }
            return 0;

        }
        public static async Task<int> SendMessage(string title, string body)
        {
            try
            {
                if (_client == null)
                {
                    await GetClient().ConfigureAwait(false);
                }

                DiscordEmbedBuilder embed = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.HotPink,
                    Title = title,
                    Description = body
                };

                DiscordChannel channel = await _client.GetChannelAsync(_channelId);

                await channel.SendMessageAsync(embed).ConfigureAwait(false);

                return 0;
            }
            catch (Exception ex) 
            { 
                return -1;
            }

        }

        private static async Task GetClient()
        {
            string token;
            using (var cxn = new SQLiteDataAccess().GetAppCacheCxn())
            {
                cxn.Open();
                var sql = "SELECT value FROM settings WHERE setting='bot'";
                var cmd = cxn.CreateCommand();
                cmd.CommandText = sql;
                var result = cmd.ExecuteScalar(System.Data.CommandBehavior.SingleResult);
                token = result.ToString();
            }

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            _client = new DiscordClient(discordConfig);
            _client.Ready += (sender, args) => Task.CompletedTask;

            await _client.ConnectAsync().ConfigureAwait(false);
        }

        public static void ShutdownClient()
        {
            _client.DisconnectAsync().RunSynchronously();
            _client.Dispose();
            _client = null;
        }

        public void Dispose()
        {
            if (_client != null)
            {
                _client.DisconnectAsync().RunSynchronously();
                _client.Dispose();
                _client = null;
            }
        }
    }
}
