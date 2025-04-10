﻿using AuntAlenciasCollection.DataHandling;
using DSharpPlus.Entities;
using DSharp​Plus.SlashCommands;
using Google.Protobuf.WellKnownTypes;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace AuntAlenciasCollection
{
    public class BotCommands: ApplicationCommandModule
    {
        private DataManager _dataManager { get; set; }
        public BotCommands()
        {
            _dataManager = new DataManager();
        }

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
                ImageUrl = $"attachment://{filename}",
                Description = "Local Alencia",
                Color = DiscordColor.HotPink

            }.Build();
            
            
            var builder = new Discord​Message​Builder
            {
                Embed = embed

            }.WithFile(stream);

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

        [SlashCommand("copyCat", "Copies your message")]
        public async Task copy(InteractionContext ctx, [Option("message", "Message To Copy")] string message)
        {
            await ctx.CreateResponseAsync("Aunt Alencia says: " + message).ConfigureAwait(false);
        }

        [SlashCommand("add", "Tests Parameters and 'add' operation")]
        public async Task copy(InteractionContext ctx,
                              [Option("message", "Message To Copy")] string message,
                              [Option("number1", "number1 To Add")] double number1,
                              [Option("number2", "number2 To Add")] double number2)
        {
            await ctx.CreateResponseAsync("Message: " + message + "\nResult: " + (number1 + number2)).ConfigureAwait(false);
        }



        [SlashCommand("copyImage", "Copies your image")]
        public async Task copyImage(InteractionContext ctx, [Option("discordAttachment", "Image To Copy")] DiscordAttachment discordAttachment)
        {
            DiscordEmbed embed = new DiscordEmbedBuilder
            {
                ImageUrl = discordAttachment.Url,
                Description = "Copied Picture",
                Color = DiscordColor.CornflowerBlue

            }.Build();

            await ctx.CreateResponseAsync(embed).ConfigureAwait(false);
        }

        [SlashCommand("copyCharacter", "Copies your image by labelling the character")]
        public async Task copyCharacter(InteractionContext ctx,
            [Option("character", "Character in the picture")] string character,
            [Option("discordAttachment", "Image To Copy")] DiscordAttachment discordAttachment)
        {
            DiscordEmbed embed = new DiscordEmbedBuilder
            {
                ImageUrl = discordAttachment.Url,
                Description = character,
                Color = DiscordColor.Aquamarine

            }.Build();

            await ctx.CreateResponseAsync(embed).ConfigureAwait(false);
        }

        [SlashCommand("savePicture", "Saves the picture on Desktop")]
        public async Task savePicture(InteractionContext ctx,
            [Option("discordAttachment", "Image To save")] DiscordAttachment discordAttachment)
        {
            try
            {
                string filetype = Path.GetExtension(discordAttachment.Url);

                var fileNameContents = discordAttachment.FileName.Split('.');
                string filename = fileNameContents[0] + "_copy";
                var clientHelper = new WebClient();
                byte[] buffer = clientHelper.DownloadData(discordAttachment.Url);

                FileStream file = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\"+filename + "."+ fileNameContents[1]);
                file.Write(buffer, 0, buffer.Length);
                file.Close();

                await ctx.CreateResponseAsync("File copied").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await ctx.CreateResponseAsync("Error saving file").ConfigureAwait(false);
                throw;
            }
            
        }

        [SlashCommand("saveOnDB", "Saves the picture on Database")]
        public async Task saveOnDB(InteractionContext ctx,
            [Option("character", "Character in the picture")] string character,
            [Option("discordAttachment", "Image To save")] DiscordAttachment discordAttachment)
        {
            try
            {
                string filetype = Path.GetExtension(discordAttachment.Url);

                var fileNameContents = discordAttachment.FileName.Split('.');
                string filename = fileNameContents[0];
                string fileType = fileNameContents[1];
                var clientHelper = new WebClient();
                byte[] pictureData = clientHelper.DownloadData(discordAttachment.Url);

                var result = _dataManager.savePicture(character: character, picture: pictureData, url: discordAttachment.Url);
                if (!result.success)
                {
                    await ctx.CreateResponseAsync("Error saving file on Database!\n\rReason: "+ result.message).ConfigureAwait(false);
                }

                await ctx.CreateResponseAsync("File saved successfully!").ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await ctx.CreateResponseAsync("Error saving file!").ConfigureAwait(false);
                throw;
            }

        }


        [SlashCommand("loadURL", "Loads the URL of the picture from Database")]
        public async Task loadURL(InteractionContext ctx,
            [Option("character", "Character to load")] string character)
        {
            try
            {
                var message = (string)_dataManager.loadPictureURL(character: character);
                if (String.IsNullOrEmpty(message) || message.Equals("Error"))
                {
                    await ctx.CreateResponseAsync("Error retrieving file!").ConfigureAwait(false);
                }

                DiscordEmbed embed = new DiscordEmbedBuilder
                {
                    ImageUrl = message,
                    Description = character,
                    Color = DiscordColor.PhthaloGreen

                }.Build();

                await ctx.CreateResponseAsync(embed).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await ctx.CreateResponseAsync("Error retrieving file!").ConfigureAwait(false);
                throw;
            }
        }

        [SlashCommand("loadPic", "Loads the picture from Database")]
        public async Task loadPic(InteractionContext ctx,
            [Option("character", "Character to load")] string character)
        {
            try
            {
                //file must not exist... for now
                var message = _dataManager.loadPicturePNG(character: character);
                if (message == null)
                {
                    await ctx.CreateResponseAsync("Error retrieving file!").ConfigureAwait(false);
                }

                var tempPath = Path.Combine(Path.GetTempPath(), "Character.png");

                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }

                MemoryStream ms = new MemoryStream(message, 0, message.Length);
                ms.Write(message, 0, message.Length);
                
                FileStream stream = new FileStream(tempPath, FileMode.Create);
                ms.WriteTo(stream);
                stream.Close();

                FileInfo fileInfo = new FileInfo(tempPath);
                var stream2 = fileInfo.OpenRead();

                

                DiscordEmbed embed = new DiscordEmbedBuilder
                {
                    ImageUrl = $"attachment://Character.png",
                    Description = "Local Alencia",
                    Color = DiscordColor.HotPink

                }.Build();


                var builder = new Discord​Message​Builder
                {
                    Embed = embed

                }.WithFile(stream2);

                await ctx.DeferAsync(true);
                await ctx.Channel.SendMessageAsync(builder).ConfigureAwait(false);
                
                stream2.Close();
                
                await ctx.DeleteResponseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await ctx.CreateResponseAsync("Error retrieving file!").ConfigureAwait(false);
                throw;
            }

        }
    }

}
