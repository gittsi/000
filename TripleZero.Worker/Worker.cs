using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SWGoH.Model;
using SWGoH.Model.Settings.Worker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using TripleZero.Core.Caching;
using TripleZero.Repository.Mapping;
using TripleZero.Repository.MongoDBRepository;
using TripleZero.Worker.Helper;
using TripleZero.Worker.Settings;
using TripleZeroApi.Repository.MongoDBRepository;

namespace TripleZero.Worker
{
    public class Worker
    {
        private static BackgroundWorker worker = new BackgroundWorker();
        private SettingsWorker _settings;
        private IMapper _mapper;
        private MemoryCache _myCache;

        static void Main(string[] args)
             => new Worker().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            _settings = new ApplicationSettings(new SettingsConfiguration()).GetSettingsWorker();
            _mapper = new MappingConfiguration().GetConfigureMapper();
            _myCache = new MemoryCache(new MemoryCacheOptions());

            worker.DoWork += Worker_GuildWorker;
            worker.DoWork += (sender, e) => Worker_QueueWorker(sender,e, _settings , _myCache, _mapper);

            worker.RunWorkerAsync();
            Console.ReadKey();
        }

        static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine(e.ProgressPercentage.ToString());
        }

        static async void Worker_GuildWorker(object sender, DoWorkEventArgs e)
        {
            var delay = 30000;

            try
            {
                var guildWorker = new GuildWorker();
                await guildWorker.InitGuild();                
                Thread.Sleep(delay);
                Worker_GuildWorker(sender, e);
            }
            catch (Exception ex)
            {
                delay = delay / 2 + delay;
                Consoler.WriteLineInColor(ex.Message, ConsoleColor.Red);
                Thread.Sleep(delay);
            }
        }

        static async void Worker_QueueWorker(object sender, DoWorkEventArgs e, SettingsWorker settings, MemoryCache myCache, IMapper mapper)
        {
            var delay = 250;

            try
            {
                var queueWorker = new QueueWorker(settings, myCache, mapper);
                await queueWorker.Run();
                Thread.Sleep(delay);
                Worker_QueueWorker(sender, e, settings, myCache, mapper);
            }
            catch (Exception ex)
            {
                delay = delay / 2 + delay;
                Consoler.WriteLineInColor(ex.Message, ConsoleColor.Red);
                Thread.Sleep(delay);
            }
        }

        //static async void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    Console.WriteLine("Ending Application...");
        //}
    }
}
