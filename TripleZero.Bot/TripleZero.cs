﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using TripleZero.Bot.Infrastructure.DI;
using TripleZero.Bot.Settings;
using Autofac;
using TripleZero.Bot.Helper;
using TripleZero.Bot.Modules;
using SWGoH.Model;
using TripleZero.Modules;

namespace TripleZero.Bot
{
    class TripleZero
    {
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        static Autofac.IContainer autoFacContainer = null;
        static ApplicationSettings applicationSettings = null;
        private DiscordSocketClient client = null;
        static Logo logo = null;
        private IServiceProvider services = null;
        private CommandService commands = null;

        static void Main(string[] args)
            => new TripleZero().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            ///////////initialize autofac
            SettingsTripleZeroBot settingsTripleZeroBot = null;
            autoFacContainer = AutofacConfig.ConfigureContainer();
            using (var scope = autoFacContainer.BeginLifetimeScope())
            {
                applicationSettings = scope.Resolve<ApplicationSettings>();
                commands = scope.Resolve<CommandService>();
                client = scope.Resolve<DiscordSocketClient>();
                logo = scope.Resolve<Logo>();

                settingsTripleZeroBot = applicationSettings.GetTripleZeroBotSettings();

                await InstallCommands();

                await client.LoginAsync(TokenType.Bot, settingsTripleZeroBot.DiscordSettings.Token);
                await client.StartAsync();
                await client.SetGameAsync(string.Format("{0}help", settingsTripleZeroBot.DiscordSettings.Prefix));
            }

            //client.MessageReceived += MessageReceived;           

            while (client.ConnectionState != ConnectionState.Connected)
            {
                Consoler.WriteLineInColor(string.Format("Still not connected... {0}", DateTime.Now), ConsoleColor.Yellow);
                await Task.Delay(2000);
            }
            logo.ConsolePrintLogo(); //prints application name,version etc 
            //await TestCharAliasesDelete();
            //await TestDelete();
            //await TestGuildPlayers("41st");
            //await TestPlayerReport("tsitas_66");
            //await TestGuildModule("41s", "gk");
            //await TestCharacterModule("tsitas_66", "cls");
            await client.GetUser("TSiTaS", "1984").SendMessageAsync(logo.GetLogo());
            await Task.Delay(-1);


        }
        public async Task InstallCommands()
        {
            client.Log += Log;
            client.MessageReceived += HandleCommandAsync;
            //await commands.AddModuleAsync<PlayerModule>();
            //await commands.AddModuleAsync<CharacterModule>();
            //await commands.AddModuleAsync<GuildModule>();
            //await commands.AddModuleAsync<ArenaModule>();
            await commands.AddModuleAsync<ModsModule>();
            //await commands.AddModuleAsync<AdminModule>();
            //await commands.AddModuleAsync<HelpModule>();
            await commands.AddModuleAsync<FunModule>();
            //await commands.AddModuleAsync<DBStatsModule>();
        }
        //public async Task MessageReceived(SocketGuildUser user)
        //{
        //    var channel = client.GetChannel(370581837560676354) as SocketTextChannel;

        //    await channel.SendMessageAsync("safsgasgags");
        //}
        //public async Task UserJoined(SocketGuildUser user)
        //{
        //    var channel = client.GetChannel(370581837560676354) as SocketTextChannel;

        //    await channel.SendMessageAsync("safsgasgags");
        //}
        private async Task HandleCommandAsync(SocketMessage arg)
        {
            // Bail out if it's a System Message.
            var msg = arg as SocketUserMessage;
            if (msg == null)
            {
                return;
            }

            // We don't want the bot to respond to itself or other bots.
            // NOTE: Selfbots should invert this first check and remove the second
            // as they should ONLY be allowed to respond to messages from the same account.
            if (msg.Author.Id == client.CurrentUser.Id || msg.Author.IsBot) return;

            // Create a number to track where the prefix ends and the command begins
            int pos = 0;
            // Uncomment the second half if you also want
            // commands to be invoked by mentioning the bot instead.
            var prefix = applicationSettings.GetTripleZeroBotSettings().DiscordSettings.Prefix;
            if (msg.HasCharPrefix(Convert.ToChar(prefix), ref pos) || msg.HasMentionPrefix(client.CurrentUser, ref pos))
            {
                // Create a Command Context.
                var context = new SocketCommandContext(client, msg);

                Consoler.WriteLineInColor(string.Format("User : '{0}' sent the following command : '{1}'", context.Message.Author.ToString(), context.Message.ToString()), ConsoleColor.Green);
                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed succesfully).
                var result = await commands.ExecuteAsync(context, pos, services);

                // Uncomment the following lines if you want the bot
                // to send a message if it failed (not advised for most situations).
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Consoler.WriteLineInColor(string.Format("error  : '{0}' ", result.ErrorReason), ConsoleColor.Green);
                    await msg.Channel.SendMessageAsync(result.ErrorReason);
                }

                if (result.Error == CommandError.UnknownCommand)
                {
                    var message = msg.Channel.SendMessageAsync($"I am pretty sure that there is no command `{msg}`!!!\nTry `{prefix}help` to get an idea!").Result;
                    await Task.Delay(3000);
                    await message.DeleteAsync();
                }
            }
        }
    }
}