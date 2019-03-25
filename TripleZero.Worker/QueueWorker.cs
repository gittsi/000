using AutoMapper;
using SWGoH.Model;
using SWGoH.Model.Enums;
using SWGoH.Model.Settings.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripleZero.Core.Caching;
using TripleZero.Repository.Mapping;
using TripleZero.Repository.MongoDBRepository;
using TripleZero.Repository.SWGoHHelp;
using TripleZero.Worker.Helper;
using TripleZero.Worker.Settings;

namespace TripleZero.Worker
{
    public class QueueWorker
    {
        private CacheClient _cacheClient;
        private SettingsWorker _settings;
        private IMapper _mapper;
        public QueueWorker(SettingsWorker settings, IMapper mapper)
        {
            _settings = settings;            
            _mapper = mapper;
        }

        public QueueWorker()
        {
            _settings = new ApplicationSettings(new SettingsConfiguration()).GetSettingsWorker();
            _mapper = new MappingConfiguration().GetConfigureMapper();
        }

        //public QueueWorker()
        //{
        //    _settings = settings;
        //    _cacheClient = new CacheClient();
        //    _mapper = mapper;

        //    _ettings = new ApplicationSettings(new SettingsConfiguration()).GetSettingsWorker();
        //    IMapper _mapper = new MappingConfiguration().GetConfigureMapper();

        //}

        public async Task Run()
        {
            var logStringInit = "RunningQueue:";
            Consoler.WriteLineInColor($"--------------------------{logStringInit} : START--------------------------", ConsoleColor.Green);

            var queueRepo = new QueueRepository(new MongoDBConnectionHelper(_settings.MongoDBSettings), _mapper);

            try
            {
                Consoler.WriteLineInColor($"{logStringInit} : Getting next in queue", ConsoleColor.Green);
                var nextInQueue = await queueRepo.GetNextInQueue(_settings.GeneralSettings.ApplicationName);
                if (nextInQueue == null)
                {
                    Consoler.WriteLineInColor($"{logStringInit} : No item in queue ready for process", ConsoleColor.Cyan);
                }
                else
                {
                    Consoler.WriteLineInColor($"{logStringInit} : Found next in queue '{nextInQueue.Name}'", ConsoleColor.Green);
                    Consoler.WriteLineInColor($"{logStringInit} : Processing '{nextInQueue.Name}'...", ConsoleColor.Green);
                    switch (nextInQueue.Type)
                    {
                        case QueueType.Guild:
                            await ProcessGuild(nextInQueue);
                            break;
                        case QueueType.Player:
                            await ProcessPlayer(nextInQueue);
                            break;
                        default:
                            throw new Exception("Unknown type in queue!!!");
                    }
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

        private async Task ProcessGuild(Queue queue)
        {
            var guildId = queue.ItemId;
            var guildConfig = await new GuildConfigRepository(new MongoDBConnectionHelper(_settings.MongoDBSettings), _mapper).GetGuildConfigById(guildId);
            var allyCodeBool = int.TryParse(guildConfig.DefaultPlayerAllyCode.ToString().Replace("-", ""), out int allyCode);

            var guildRepoSWGoh = new SWGoHHelpGuildRepository(_settings.SWGoHHelpSettings, null, _mapper, false);
            var guildResultSWGoH = await guildRepoSWGoh.GetGuild(allyCode);

            var guildRepo = new GuildRepository(new MongoDBConnectionHelper(_settings.MongoDBSettings), _mapper);
            var guildResult = await guildRepo.GetGuildByName(guildConfig.Name);

            guildResultSWGoH.Id = guildResult?.Id ?? Guid.NewGuid().ToString();

            var r = await guildRepo.GuildUpdate(guildResultSWGoH);

            if (r)
            {
                foreach(var playerSWGoH in guildResultSWGoH.Players)
                {
                    var playerDB = guildResult.Players.FirstOrDefault(p => p.AllyCode == playerSWGoH.AllyCode);
                    if(playerDB==null || playerDB.RosterUpdateDate.ToString("YYYY-MM-dd HH:mm:ss") != playerSWGoH.RosterUpdateDate.ToString("YYYY-MM-dd HH:mm:ss"))
                    {
                        //TODO we have to queue him
                        Consoler.WriteLineInColor($"Found diff!!!! {playerSWGoH.PlayerNameInGame} ", ConsoleColor.DarkBlue);
                    }
                }

                
            }

            var a = 1;
        }

        private async Task ProcessPlayer(Queue queue)
        {
            var playerId = queue.ItemId;
        }
    }
}
