using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;


namespace AuntAlenciasCollection.BotHandling
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public async Task runAsync()
        {
            var config = new DiscordConfiguration
            { 
                Token= "123",
                TokenType = TokenType.Bot,
                AutoReconnect=true,
                MinimumLogLevel=Microsoft.Extensions.Logging.LogLevel.Debug
            };

            this.Client = new DiscordClient(config);
            this.Client.Ready += isClientReady;

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { "/" },
                EnableMentionPrefix = true,
                EnableDms = false
            };

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private Task isClientReady (DiscordClient Client, ReadyEventArgs e)
        {
            return null;
        }
    }
}
