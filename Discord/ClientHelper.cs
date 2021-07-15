using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace IkosBots.Discord
{
    public class ClientHelper
    {
        private DiscordSocketClient _client;
        private ulong _discordChannelID;

        public ClientHelper(DiscordSocketClient client, ulong discordChannelID)
        {
            _client = client;
            _discordChannelID = discordChannelID;
        }

        public async Task WriteToChannel(string text)
        {
            await WriteToChannel(text, true);
        }
        public async Task WriteToChannel(string text, bool useConsoleFormat)
        {
            if (useConsoleFormat)
            {
                text = $"```{text}```";
            }

            try
            {
                IMessageChannel channel = _client.GetChannel(_discordChannelID) as IMessageChannel;
                if (channel != null)
                {
                    await channel.SendMessageAsync(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to write to channel: {text}\nReason: {e.Message}");
            }
        }
    }
}
