//using AutoMapper;
//using SWGoH.Model;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Threading;
//using System.Threading.Tasks;
//using TripleZero.Repository.Mapping;
//using TripleZero.Repository.MongoDBRepository;
//using TripleZero.Worker.Helper;
//using TripleZero.Worker.Settings;
//using TripleZeroApi.Repository.MongoDBRepository;

//namespace TripleZero.Worker
//{
//    public class oldWorker
//    {
//        private static BackgroundWorker worker = new BackgroundWorker();

//        static void Main(string[] args)
//             => new oldWorker().MainAsync().GetAwaiter().GetResult();
//        public async Task MainAsync()
//        {
//            //await InitGuild();
//            //Console.ReadKey();

//            worker.DoWork += Worker_DoWork;
//            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
//            worker.ProgressChanged += Worker_ProgressChanged;
//            worker.WorkerReportsProgress = true;
//            worker.WorkerSupportsCancellation = true;

//            Console.WriteLine("Starting Application...");

//            worker.RunWorkerAsync();


//            Console.ReadKey();
//        }

//        static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
//        {
//            Console.WriteLine(e.ProgressPercentage.ToString());
//        }

//        static async void Worker_DoWork(object sender, DoWorkEventArgs e)
//        {
//            int countErrors = 0;
            

//            var a = new GuildWorker();
//            //await a.InitGuild();

//            Console.WriteLine("x");

//            var delay = 1000;
//            Thread.Sleep(delay);

//            while (countErrors < 10)
//            {
//                try
//                {
                    
//                    countErrors = countErrors + 1;
//                }
//                catch (Exception ex)
//                {

//                    delay = delay / 2 + delay;
//                    Consoler.WriteLineInColor(ex.Message, ConsoleColor.Red);
//                    Thread.Sleep(delay);
//                }
//            }

//            e.Result = countErrors;
//        }

//        //static async void Worker_DoWork(object sender, DoWorkEventArgs e)
//        //{
//        //    Console.WriteLine("Starting to do some work now...");
//        //    int i = 1;

//        //    var delay = 100;

//        //    while (i < 15)
//        //    {
//        //        try
//        //        {
//        //            Thread.Sleep(delay);

//        //            if (i > 11 && delay < 1000) throw new Exception("test ex");
//        //            worker.ReportProgress(Convert.ToInt32((100.0 * i) / 10));

//        //            i += 1;
//        //            delay = 100;
//        //        }
//        //        catch (Exception ex)
//        //        {

//        //            delay = delay / 2 + delay;
//        //            Consoler.WriteLineInColor(ex.Message, ConsoleColor.Red);
//        //            Thread.Sleep(delay);
//        //        }
//        //    }

//        //    e.Result = i;
//        //}

//        static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
//        {
//            if(e.Result?.ToString()!="0")
//                Consoler.WriteLineInColor($"Found {e.Result?.ToString()} errors", ConsoleColor.Red);
                        
//            worker.RunWorkerAsync();
//        }

        

//        private async Task GetMongoDBGuildConfigAsync()
//        {
//            var repoSettings = new ApplicationSettings(new SettingsConfiguration()).GetSettingsWorker();

//            //var aaaa = new MongoDBContext(repoSettings.MongoDBSettings);

//            IMapper mapper = new MappingConfiguration().GetConfigureMapper();

//            var bbb = new GuildConfigRepository(new MongoDBConnectionHelper(repoSettings.MongoDBSettings), mapper);
//            var queueRepo = new QueueRepository(new MongoDBConnectionHelper(repoSettings.MongoDBSettings), mapper);

//            //var result2 = await bbb.GetAll();
//            // var result = bbb.GetGuildConfigByAlias("fsa");

//            //var idse = await bbb.GetGuildConfigById("59fddf8ef36d2831457fa0cb");
//            //var idse1 = await bbb.GetGuildConfigById("59fddf8ef36d2831457fa0c1");

//            //var result3 = await bbb.GetGuildConfigByAlias("41st");

//            //var rereer = await bbb.GetGuildConfigByAlias("41");

//            var queueue = new SWGoH.Model.Queue()
//            {
//                Id = Guid.NewGuid().ToString(),
//                InsertedDate = DateTime.UtcNow,
//                Name = "fasfasf",
//                NextRunDate = DateTime.UtcNow.AddMinutes(5),
//                Priority = SWGoH.Model.Enums.QueuePriority.AutoUpdate,
//                Type = SWGoH.Model.Enums.QueueType.Guild,
//                Status = SWGoH.Model.Enums.QueueStatus.PendingProcess
//            };

//            var asdfasfasf = await queueRepo.AddToQueue(queueue);
//            Console.WriteLine(asdfasfasf);
//            try
//            {

//                var queue = await queueRepo.GetNextInQueue("my");
//                var list = await queueRepo.GetAll();

//                await queueRepo.SetQueuesUnProcessed(list);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                throw;
//            }


//            var agfsdgsdg = 1;
//        }
//    }
//}
