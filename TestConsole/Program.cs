using System;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Caching.Memory;
using TripleZero.Bot.Infrastructure.DI;
using TripleZero.Bot.Settings;
using TripleZero.Core;
using TripleZero.Repository.SWGoHHelpRepository.Authentication;
using AutoMapper;

namespace TestConsole
{
    class Program
    {
        //private DiscordSocketClient _client;

        //public static void Main(string[] args)
        //    => new Program().MainAsync().GetAwaiter().GetResult();

        //public async Task MainAsync()
        //{
        //    _client = new DiscordSocketClient();

        //    _client.Log += Log;
        //    _client.MessageReceived += MessageReceived;

        //    string token = "abcdefg..."; // Remember to keep this private!
        //    await _client.LoginAsync(TokenType.Bot, "Mzc4MDg1OTIwNzM1MzYzMDcz.DOWXlw.UL-EctlIXnBYJux1hijB406_tag");
        //    await _client.StartAsync();

        //    // Block this task until the program is closed.
        //    await Task.Delay(-1);
        //}

        //private async Task MessageReceived(SocketMessage message)
        //{
        //    if (message.Content == "!ping")
        //    {
        //        Task.Delay(5000);
        //        await message.Channel.SendMessageAsync("Pong!");                
        //    }
        //}

        private Task Log()
        {
           // Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        static Autofac.IContainer autoFacContainer = null;
        static ApplicationSettings applicationSettings = null;
        private IPlayerContext playerContext = null;
        private IServiceProvider services = null;
        private IMemoryCache memoryCache = null;


        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            applicationSettings = new ApplicationSettings(new SettingsConfiguration());
            var repoSettings = applicationSettings.GetTripleZeroRepositorySettings();
            IMapper mapper = null;
            var context = new PlayerContext(repoSettings, new MemoryCache(new MemoryCacheOptions()), mapper);

            var player = context.GetPlayerData(462747278);

            /////////////initialize autofac
            //autoFacContainer = AutofacConfig.ConfigureContainer();
            //using (var scope = autoFacContainer.BeginLifetimeScope())
            //{
            //    applicationSettings = scope.Resolve<ApplicationSettings>();

            //    var appSettings = applicationSettings.GetTripleZeroBotSettings();
            //    playerContext  = scope.Resolve<IPlayerContext>();
            //    //memoryCache = scope.Resolve<IMemoryCache>();

            //    //var aaa = scope.Resolve<Authentication>();
            //}

            //var result = playerContext.GetPlayerData(124141);
            //var cache = new MemoryCache(new MemoryCacheOptions());
            //var a = new Authentication(new TripleZero.Repository.SWGoHHelp.Dto.UserSettings() {username="TSiTaS", password="00000000", client_id="abc", client_secret="123" }, cache );
            //var resulta = a.GetToken();

            //var b = new Authentication(new TripleZero.Repository.SWGoHHelp.Dto.UserSettings() { username = "TSiTaS", password = "00000000", client_id = "abc", client_secret = "123" }, cache);
            //var resultb = a.GetToken();

            //var resultc = b.GetToken();
        }
    }
}
