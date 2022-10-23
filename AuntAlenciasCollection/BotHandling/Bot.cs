using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;

namespace AuntAlenciasCollection.BotHandling
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public SlashCommandsExtension Commands { get; private set; }

        public async Task runAsync()
        {
            var config = new DiscordConfiguration
            { 
                Token= "MTAzMDgyNzM1MDEzNDM3ODU1OA.GQlDXF.--uUDEiYkXrjC_TAX8BWUpMWVuId8RiMVRWkrY",
                TokenType = TokenType.Bot,
                AutoReconnect=true,
                MinimumLogLevel=Microsoft.Extensions.Logging.LogLevel.Debug
            };

            this.Client = new DiscordClient(config);
            this.Client.Ready += isClientReady;
            

            SlashCommandsConfiguration slashCommandsConfiguration = new SlashCommandsConfiguration();

            this.Commands = this.Client.UseSlashCommands(slashCommandsConfiguration);
            this.Commands.RegisterCommands<BotCommands>();
            

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private Task isClientReady (DiscordClient Client, ReadyEventArgs e)
        {
            Console.WriteLine("Aunt Alencia is going online...");  
            return Task.CompletedTask;
        }
    }
}
