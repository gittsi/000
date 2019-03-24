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
    public class GuildWorker
    {
        public async Task InitGuild()
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
                            NextRunDate = DateTime.UtcNow.AddMinutes(guildConfig.RetrieveEveryXMinutes),
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
    }
}
