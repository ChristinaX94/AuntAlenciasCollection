using DSharpPlus.Entities;
using DSharp​Plus.SlashCommands;

namespace AuntAlenciasCollection
{
    public class BotCommands: ApplicationCommandModule
    {
        [SlashCommand("ping", "pings bot")]
        public async Task Ping(InteractionContext ctx)
        {
            var server = ctx.Guild;
            var member = ctx.Member;

            Console.WriteLine("server: " + server);
            Console.WriteLine("member: " + member);

            await ctx.CreateResponseAsync("Pong").ConfigureAwait(false);
            //await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }

        [SlashCommand("showLocalPic", "Shows a local picture")]
        public async Task ShowPic(InteractionContext ctx)
        {
            var uri = new System.Uri(@"C:\Users\chris\source\repos\AuntAlenciasCollection\AuntAlenciasCollection\Commands\example.png");
            
            FileInfo fileInfo = new FileInfo(uri.AbsolutePath);
            var stream = fileInfo.OpenRead();
            var filename = Path.GetFileName(uri.AbsolutePath);

            DiscordEmbed embed = new DiscordEmbedBuilder
            {
                //ImageUrl = "https://epic7x.com/wp-content/uploads/2019/01/Alencia.png",
                ImageUrl = $"attachment://{filename}",
                Description = "Local Alencia",
                Color = DiscordColor.HotPink

            }.Build();
            
            
            var builder = new Discord​Message​Builder
            {
                Embed = embed

            }.WithFile(stream);

            var timer = TimeSpan.FromSeconds(60);   
            //await ctx.CreateResponseAsync(builder).ConfigureAwait(false);
            await ctx.DeferAsync(true);
            await ctx.Channel.SendMessageAsync(builder).ConfigureAwait(false);
            await ctx.DeleteResponseAsync();

        }

        [SlashCommand("showGlobalPic", "Shows a picture from the Internet")]
        public async Task ShowGlobalPic(InteractionContext ctx)
        {
            DiscordEmbed embed = new DiscordEmbedBuilder
            {
                ImageUrl = "https://epic7x.com/wp-content/uploads/2019/01/Alencia.png",
                Description = "Internet Alencia",
                Color = DiscordColor.Purple

            }.Build();

            await ctx.CreateResponseAsync(embed).ConfigureAwait(false);
            //await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);

        }

    }

}
