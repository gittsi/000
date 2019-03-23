using AutoMapper;
using SWGoH.Model;
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

        static void Main(string[] args)
             => new Worker().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            await InitGuild();
            Console.ReadKey();

            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            Console.WriteLine("Starting Application...");

            worker.RunWorkerAsync();


            Console.ReadKey();
        }

        static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine(e.ProgressPercentage.ToString());
        }

        static async void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Starting to do some work now...");
            int i = 1;

            var delay = 100;

            while (i < 20)
            {
                try
                {
                    Thread.Sleep(delay);

                    if (i > 11 && delay < 1000) throw new Exception("test ex");
                    worker.ReportProgress(Convert.ToInt32((100.0 * i) / 10));

                    i += 1;
                    delay = 100;
                }
                catch (Exception ex)
                {

                    delay = delay / 2 + delay;
                    Consoler.WriteLineInColor(ex.Message, ConsoleColor.Red);
                    Thread.Sleep(delay);
                }
            }

            e.Result = i;
        }

        static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Value Of i = " + e.Result.ToString());
            Console.WriteLine("Done now...");

            worker.RunWorkerAsync();
        }

        private async Task InitGuild()
        {
            var logStringInit = "InitGuild:";
            Consoler.WriteLineInColor($"--------------------------{logStringInit} : START--------------------------", ConsoleColor.Green);
            var repoSettings = new ApplicationSettings(new SettingsConfiguration()).GetSettingsWorker();
            IMapper mapper = new MappingConfiguration().GetConfigureMapper();

            var guildConfigRepo = new GuildConfigRepository(new MongoDBConnectionHelper(repoSettings.MongoDBSettings), mapper);
            var queueRepo = new QueueRepository(new MongoDBConnectionHelper(repoSettings.MongoDBSettings), mapper);

            Consoler.WriteLineInColor($"{logStringInit} : Getting guild configs", ConsoleColor.Green);

            try
            {
                var guildConfigs = await guildConfigRepo.GetAll();

                foreach (var guildConfig in guildConfigs)
                {
                    Consoler.WriteLineInColor($"{logStringInit} : Started name : {guildConfig.Name}", ConsoleColor.Green);
                    Consoler.WriteLineInColor($"{logStringInit} : Getting queue from itemId : {guildConfig.Id}", ConsoleColor.Green);
                    var res = await queueRepo.GetByItemId(guildConfig.Id);
                    if (res == null)
                    {
                        Consoler.WriteLineInColor($"{logStringInit} : Queue not found with specified id", ConsoleColor.Green);
                        var newQueue = new Queue()
                        {
                            Id = Guid.NewGuid().ToString(),
                            InsertedDate = DateTime.UtcNow,
                            ItemId = guildConfig.Id,
                            Name = guildConfig.Name,
                            NextRunDate = DateTime.UtcNow,
                            Priority = guildConfig.AutoQueue ? SWGoH.Model.Enums.QueuePriority.AutoUpdate : SWGoH.Model.Enums.QueuePriority.ManualLoad,
                            Type = SWGoH.Model.Enums.QueueType.Guild,
                            Status = SWGoH.Model.Enums.QueueStatus.PendingProcess
                        };
                        Consoler.WriteLineInColor($"{logStringInit} : Creating queue for guild", ConsoleColor.Green);
                        var created = await queueRepo.AddToQueue(newQueue);
                        if (created)
                            Consoler.WriteLineInColor($"{logStringInit} : Created queue for guild {guildConfig.Name}", ConsoleColor.Cyan);
                        else
                            Consoler.WriteLineInColor($"{logStringInit} : Failed to creat queue for guild {guildConfig.Name}", ConsoleColor.Red);
                    }
                    else
                    {
                        Consoler.WriteLineInColor($"{logStringInit} : Guild {guildConfig.Name} already in queue!!! Next run {res.NextRunDate.Value.ToString("yyyy-MM-dd HH:mm:ss")} ", ConsoleColor.Cyan);
                    }
                }
            }
            catch (Exception ex)
            {
                Consoler.WriteLineInColor($"{logStringInit} : ERROR {ex.Message}", ConsoleColor.Red);
            }
            finally
            {
                Consoler.WriteLineInColor($"--------------------------{logStringInit} : END----------------------------", ConsoleColor.Green);
            }
        }

        private async Task GetMongoDBGuildConfigAsync()
        {
            var repoSettings = new ApplicationSettings(new SettingsConfiguration()).GetSettingsWorker();

            //var aaaa = new MongoDBContext(repoSettings.MongoDBSettings);

            IMapper mapper = new MappingConfiguration().GetConfigureMapper();

            var bbb = new GuildConfigRepository(new MongoDBConnectionHelper(repoSettings.MongoDBSettings), mapper);
            var queueRepo = new QueueRepository(new MongoDBConnectionHelper(repoSettings.MongoDBSettings), mapper);

            //var result2 = await bbb.GetAll();
            // var result = bbb.GetGuildConfigByAlias("fsa");

            //var idse = await bbb.GetGuildConfigById("59fddf8ef36d2831457fa0cb");
            //var idse1 = await bbb.GetGuildConfigById("59fddf8ef36d2831457fa0c1");

            //var result3 = await bbb.GetGuildConfigByAlias("41st");

            //var rereer = await bbb.GetGuildConfigByAlias("41");

            var queueue = new SWGoH.Model.Queue()
            {
                Id = Guid.NewGuid().ToString(),
                InsertedDate = DateTime.UtcNow,
                Name = "fasfasf",
                NextRunDate = DateTime.UtcNow.AddMinutes(5),
                Priority = SWGoH.Model.Enums.QueuePriority.AutoUpdate,
                Type = SWGoH.Model.Enums.QueueType.Guild,
                Status = SWGoH.Model.Enums.QueueStatus.PendingProcess
            };

            var asdfasfasf = await queueRepo.AddToQueue(queueue);
            Console.WriteLine(asdfasfasf);
            try
            {

                var queue = await queueRepo.GetNextInQueue("my");
                var list = await queueRepo.GetAll();

                await queueRepo.SetQueuesUnProcessed(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }


            var agfsdgsdg = 1;
        }
    }
}
