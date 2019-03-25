using AutoMapper;
using SWGoH.Model;
using SWGoH.Model.Settings.Worker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
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

        static void Main(string[] args)
             => new Worker().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            _settings = new ApplicationSettings(new SettingsConfiguration()).GetSettingsWorker();
            _mapper = new MappingConfiguration().GetConfigureMapper();

            worker.DoWork += Worker_GuildWorker;
            worker.DoWork += Worker_QueueWorker;

            worker.RunWorkerAsync();
            Console.ReadKey();
        }

        static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine(e.ProgressPercentage.ToString());
        }

        static async void Worker_GuildWorker(object sender, DoWorkEventArgs e)
        {
            var delay = 5000;

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

        static async void Worker_QueueWorker(object sender, DoWorkEventArgs e)
        {
            var delay = 13300;

            try
            {
                var queueWorker = new QueueWorker();
                await queueWorker.Run();
                Thread.Sleep(delay);
                Worker_QueueWorker(sender, e);
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
