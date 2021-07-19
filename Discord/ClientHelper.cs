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

        public async Task<ulong> WriteToChannel(string text)
        {
            return await WriteToChannel(text, true, 0);
        }
        public async Task<ulong> WriteToChannel(string text, ulong previousMsgToDelete)
        {
            return await WriteToChannel(text, true, previousMsgToDelete);
        }

        /// <summary>
        /// General method to write to my channel.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="useConsoleFormat"></param>
        /// <param name="previousMsgToDelete">If non zero then we try to delete message first.</param>
        /// <returns>Returns ID of new message.</returns>
        public async Task<ulong> WriteToChannel(string text, bool useConsoleFormat, ulong previousMsgToDelete)
        {
            ulong msgID = 0;

            if (useConsoleFormat)
            {
                text = $"```{text}```";
            }

            try
            {
                IMessageChannel channel = _client.GetChannel(_discordChannelID) as IMessageChannel;
                if (channel != null)
                {
                    if (previousMsgToDelete != 0)
                    {
                        await channel.DeleteMessageAsync(previousMsgToDelete);
                    }

                    var msg = await channel.SendMessageAsync(text);
                    msgID = msg.Id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to write to channel: {text}\nReason: {e.Message}");
            }

            return msgID;
        }
    }
}
