using AutoMapper;
using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripleZero.Repository.Mapping;
using TripleZero.Repository.MongoDBRepository;
using TripleZero.Worker.Helper;
using TripleZero.Worker.Settings;

namespace TripleZero.Worker
{
    public class QueueWorker
    {
        public async Task Run()
        {
            var logStringInit = "RunningQueue:";
            Consoler.WriteLineInColor($"--------------------------{logStringInit} : START--------------------------", ConsoleColor.Green);
            var repoSettings = new ApplicationSettings(new SettingsConfiguration()).GetSettingsWorker();
            IMapper mapper = new MappingConfiguration().GetConfigureMapper();

            var queueRepo = new QueueRepository(new MongoDBConnectionHelper(repoSettings.MongoDBSettings), mapper);

            try
            {
                Consoler.WriteLineInColor($"{logStringInit} : Getting next in queue", ConsoleColor.Green);
                var nextInQueue = await queueRepo.GetNextInQueue(repoSettings.GeneralSettings.ApplicationName);
                if (nextInQueue == null)
                {
                    Consoler.WriteLineInColor($"{logStringInit} : No item in queue ready for process", ConsoleColor.Cyan);
                }
                else
                {
                    Consoler.WriteLineInColor($"{logStringInit} : Found next in queue '{nextInQueue.Name}'", ConsoleColor.Green);

                    Consoler.WriteLineInColor($"{logStringInit} : Processing '{nextInQueue.Name}'...", ConsoleColor.Green);
                    await Task.Delay(6000);
                    Consoler.WriteLineInColor($"{logStringInit} : Finished processing '{nextInQueue.Name}'!!!", ConsoleColor.Green);

                    Consoler.WriteLineInColor($"{logStringInit} : Trying to delete from queue", ConsoleColor.Green);
                    var isDeleted = await queueRepo.DeleteFromQueue(nextInQueue);
                    if (!isDeleted) throw new Exception("Falied to delete from queue");
                    Consoler.WriteLineInColor($"{logStringInit} : Deleted from queue", ConsoleColor.Green);
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
    }
}
