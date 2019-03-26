using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
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
using TripleZero.Repository.SWGoHHelpRepository;
using TripleZero.Worker.Helper;
using TripleZero.Worker.Settings;

namespace TripleZero.Worker
{
    public class QueueWorker
    {
        private MemoryCache _myCache;
        private SettingsWorker _settings;
        private IMapper _mapper;
        public QueueWorker(SettingsWorker settings, CacheClient cacheClient, IMapper mapper)
        {
            _settings = settings;            
            _mapper = mapper;
        }

        public QueueWorker(SettingsWorker settings, MemoryCache myCache, IMapper mapper)
        {
            _settings = settings;
            _mapper = mapper;
            _myCache = myCache;
        }

        //public QueueWorker()
        //{
        //    _settings = new ApplicationSettings(new SettingsConfiguration()).GetSettingsWorker();
        //    _mapper = new MappingConfiguration().GetConfigureMapper();
        //    _myCache = new MemoryCache(new MemoryCacheOptions());

        //}

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
            bool processResult = false;

            try
            {
                var queueAll = await queueRepo.GetAll();
                var queueManual = queueAll.Where(p => p.Priority == QueuePriority.ManualLoad).OrderBy(p => p.NextRunDate);
                var queueAuto = queueAll.Where(p => p.Priority == QueuePriority.AutoUpdate).OrderBy(p => p.NextRunDate);

                int count = 1;
                foreach(var queue in queueManual)
                {
                    Consoler.WriteLineInColor($"{count} : {queue.Name} : {queue.Status} : {queue.NextRunDate?.ToString("yyyy-MM-dd : HH:mm:ss")}", ConsoleColor.Green);
                    count += 1;
                }
                count = 1;
                foreach (var queue in queueAuto)
                {
                    Consoler.WriteLineInColor($"{count} : {queue.Name} : {queue.Status} : {queue.NextRunDate?.ToString("yyyy-MM-dd : HH:mm:ss")}", ConsoleColor.Green);
                    count += 1;
                }


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
                            processResult = await ProcessGuild(nextInQueue);
                            break;
                        case QueueType.Player:
                            processResult = await ProcessPlayer(nextInQueue);
                            break;
                        default:
                            throw new Exception("Unknown type in queue!!!");
                    }
                    Consoler.WriteLineInColor($"{logStringInit} : Finished processing '{nextInQueue.Name}'!!!", ConsoleColor.Green);

                    Consoler.WriteLineInColor($"{logStringInit} : Trying to delete from queue", ConsoleColor.Green);
                    var isDeleted = await queueRepo.DeleteFromQueue(nextInQueue);
                    if (!isDeleted) throw new Exception("Falied to delete from queue");
                    Consoler.WriteLineInColor($"{logStringInit} : Deleted from queue", ConsoleColor.Green);

                    if (!processResult)                    
                    {
                        var reQueue = new Queue()
                        {
                            Id = nextInQueue.Id,
                            InsertedDate = DateTime.UtcNow,
                            ItemId = nextInQueue.ItemId,
                            Name = nextInQueue.Name,
                            NextRunDate = nextInQueue.NextRunDate.Value.AddMinutes(1),
                            Priority = nextInQueue.Priority,
                            Type = nextInQueue.Type,
                            Status = QueueStatus.PendingProcess
                        };                        
                        var created = await queueRepo.AddToQueue(reQueue);
                        if (created)
                            Consoler.WriteLineInColor($"{logStringInit} : Created queue {reQueue.Name}", ConsoleColor.Cyan);
                        else
                            Consoler.WriteLineInColor($"{logStringInit} : Failed to creat queue {reQueue.Name}", ConsoleColor.Red);
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

        private async Task<bool> ProcessGuild(Queue queue)
        {
            var logStringInit = "RunningQueue.ProcessGuild:";
            Consoler.WriteLineInColor($"--------------------------{logStringInit} : START--------------------------", ConsoleColor.Green);

            var guildId = queue.ItemId;
            try
            {
                Consoler.WriteLineInColor($"{logStringInit} : Getting guild config for id : {guildId}", ConsoleColor.Green);
                var guildConfig = await new GuildConfigRepository(new MongoDBConnectionHelper(_settings.MongoDBSettings), _mapper).GetGuildConfigById(guildId);
                Consoler.WriteLineInColor($"{logStringInit} : Got config for guild : {guildConfig.Name} from id {guildId}", ConsoleColor.Green);    

                var allyCodeBool = int.TryParse(guildConfig.DefaultPlayerAllyCode.ToString().Replace("-", "").Replace(" ", "").Trim(), out int allyCode);
                if (!allyCodeBool) throw new Exception("Failed to parse allyCode");

                Consoler.WriteLineInColor($"{logStringInit} : Fetching guild data from API via allyCode {allyCode}", ConsoleColor.Green);
                Consoler.WriteLineInColor($"{logStringInit} : Accessing SWGoH API", ConsoleColor.Magenta);
                var guildRepoSWGoh = new SWGoHHelpGuildRepository(_settings.SWGoHHelpSettings, null, _mapper, false);                
                var guildResultSWGoH = await guildRepoSWGoh.GetGuild(allyCode);
                Consoler.WriteLineInColor($"{logStringInit} : Fetched data for guild {guildResultSWGoH.Name} from allyCode {allyCode}", ConsoleColor.Green);

                Consoler.WriteLineInColor($"{logStringInit} : Getting data for guild {guildConfig.Name} from DB", ConsoleColor.Green);
                var guildRepo = new GuildRepository(new MongoDBConnectionHelper(_settings.MongoDBSettings), _mapper);               
                var guildResult = await guildRepo.GetGuildByName(guildConfig.Name);
                Consoler.WriteLineInColor($"{logStringInit} : Got data for guild {guildConfig.Name} from DB", ConsoleColor.Green);

                guildResultSWGoH.Id = guildResult?.Id ?? Guid.NewGuid().ToString();

                Consoler.WriteLineInColor($"{logStringInit} : Updating guild {guildResultSWGoH.Name} in db", ConsoleColor.Green);
                var guildUpdated = await guildRepo.GuildUpdate(guildResultSWGoH);
                Consoler.WriteLineInColor($"{logStringInit} : Updated guild {guildResultSWGoH.Name} in db", ConsoleColor.Green);

                if (guildUpdated)
                {
                    foreach (var playerSWGoH in guildResultSWGoH.Players)
                    {
                        var playerDB = guildResult?.Players.FirstOrDefault(p => p.AllyCode == playerSWGoH.AllyCode);
                        if (playerDB == null || playerDB.RosterUpdateDate.ToString("yyyy-MM-dd HH:mm:ss") != playerSWGoH.RosterUpdateDate.ToString("yyyy-MM-dd HH:mm:ss"))
                        {
                            //TODO we have to queue him
                            var message = playerDB == null ? "new player!!!" : $"{playerDB.RosterUpdateDate.ToString("yyyy-MM-dd HH:mm:ss")} vs {playerSWGoH.RosterUpdateDate.ToString("yyyy-MM-dd HH:mm:ss")} ";
                            Consoler.WriteLineInColor($"{logStringInit} : Sending player {playerSWGoH.PlayerName} to queue : {message}", ConsoleColor.DarkMagenta);
                            var newQueue = new Queue()
                            {
                                Id = Guid.NewGuid().ToString(),
                                InsertedDate = DateTime.UtcNow,
                                ItemId = playerSWGoH.AllyCode,
                                Name = playerSWGoH.PlayerName,
                                NextRunDate = DateTime.UtcNow,
                                Priority = QueuePriority.AutoUpdate,
                                Type = QueueType.Player,
                                Status = QueueStatus.PendingProcess
                            };
                            var queueRepo = new QueueRepository(new MongoDBConnectionHelper(_settings.MongoDBSettings), _mapper);
                            var created = await queueRepo.AddToQueue(newQueue);
                            if (created)
                                Consoler.WriteLineInColor($"{logStringInit} : Created queue for player {newQueue.Name}", ConsoleColor.Cyan);
                            else
                                Consoler.WriteLineInColor($"{logStringInit} : Failed to creat queue for player {newQueue.Name}", ConsoleColor.Red);
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Consoler.WriteLineInColor($"{logStringInit} : ERROR {ex.Message}", ConsoleColor.Red);
                return false;
            }
            finally
            {
                Consoler.WriteLineInColor($"--------------------------{logStringInit} : END----------------------------", ConsoleColor.Green);                
            }
            return true;
        }

        private async Task<bool> ProcessPlayer(Queue queue)
        {
            var logStringInit = "RunningQueue.ProcessPlayer:";
            Consoler.WriteLineInColor($"--------------------------{logStringInit} : START--------------------------", ConsoleColor.Green);

            var allyCodeBool = int.TryParse(queue.ItemId.ToString().Replace("-", "").Replace(" ","").Trim(), out int allyCode);
            if (!allyCodeBool) throw new Exception("Failed to parse allyCode");

            try
            {
                Consoler.WriteLineInColor($"{logStringInit} : Fetching player data from API via allyCode {allyCode}", ConsoleColor.Green);
                Consoler.WriteLineInColor($"{logStringInit} : Accessing SWGoH API", ConsoleColor.Magenta);
                var playerRepoSWGoh = new SWGoHHelpPlayerRepository(_settings.SWGoHHelpSettings, _myCache  , _mapper);
                var player = await playerRepoSWGoh.GetPlayer(allyCode);
                Consoler.WriteLineInColor($"{logStringInit} : Fetched data for player {player.PlayerName} from allyCode {allyCode}", ConsoleColor.Green);

                if(player == null) throw new Exception($"Failed to get player for allyCode {allyCode}");

                var playerRepo = new PlayerRepository(new MongoDBConnectionHelper(_settings.MongoDBSettings), _mapper);
                if (player.Id == null) player.Id = player.AllyCode;
                var upsertResult = await playerRepo.Upsert(player);
                if (upsertResult)
                    Consoler.WriteLineInColor($"{logStringInit} : Saved player {player.PlayerName}", ConsoleColor.Cyan);
                else
                    Consoler.WriteLineInColor($"{logStringInit} : Failed to save player {player.PlayerName}", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                Consoler.WriteLineInColor($"{logStringInit} : ERROR {ex.Message}", ConsoleColor.Red);
                return false;
            }
            finally
            {
                Consoler.WriteLineInColor($"--------------------------{logStringInit} : END----------------------------", ConsoleColor.Green);
            }
            return true;
        }
    }
}
