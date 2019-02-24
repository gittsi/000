using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SWGoH.Model;
using TripleZero.Core.Caching;
using TripleZero.Repository.SWGoHHelp;


namespace TripleZero.Core
{
    public class GuildContext : IGuildContext
    {
        private CacheClient _cacheClient;
        private SettingsTripleZeroRepository _settings;
        private IMapper _mapper;
        public GuildContext(SettingsTripleZeroRepository settings, CacheClient cacheClient, IMapper mapper)
        {
            _settings = settings;
            _cacheClient = cacheClient;
            _mapper = mapper;
        }

        public async Task<Guild> GetGuildData(int allyCode)
        {
            Stopwatch sw = new Stopwatch();
            if (_settings.DiagnosticSettings.ConsolePerformanceWatcher)
                sw.Start();  

            string functionName = "GetGuildRepo";
            string key = allyCode.ToString();
            var objCache = _cacheClient.GetDataFromRepositoryCache(functionName, key);
            if (objCache != null)
            {
                var guild = (Guild)objCache;
                guild.LoadedFromCache = true;

                if (_settings.DiagnosticSettings.ConsolePerformanceWatcher)
                {
                    sw.Stop();
                    Console.WriteLine($"Get Guild from cache : {sw.Elapsed}");
                }

                return guild;
            }


            var guildRepo = new SWGoHHelpGuildRepository(_settings.SWGoHHelpSettings, _cacheClient, _mapper,_settings.DiagnosticSettings.ConsolePerformanceWatcher);
            var guildResult = await guildRepo.GetGuild(allyCode);

            var playerContext = new PlayerContext(_settings, _cacheClient, _mapper);
            var newPlayers = new List<Player>();
            foreach(var player in guildResult.Players)
            {
                var playerResult = await playerContext.GetPlayerData(player.AllyCode);
                newPlayers.Add(playerResult);
            }
            guildResult.Players = newPlayers;

            var characterConfigContext = new CharacterConfigContext(_cacheClient, _mapper);

            if (_settings.DiagnosticSettings.ConsolePerformanceWatcher)
            {
                sw = new Stopwatch();
                sw.Start();
            }
            var characterConfig = await characterConfigContext.GetCharactersConfig();
            if (_settings.DiagnosticSettings.ConsolePerformanceWatcher)
            {
                sw.Stop();
                Console.WriteLine($"Get character config : {sw.Elapsed}");
            }

            var shipConfigContext = new ShipConfigContext(_cacheClient, _mapper);
            if (_settings.DiagnosticSettings.ConsolePerformanceWatcher)
            {
                sw = new Stopwatch();
                sw.Start();
            }
            var shipConfig = await shipConfigContext.GetShipsConfig();
            if (_settings.DiagnosticSettings.ConsolePerformanceWatcher)
            {
                sw.Stop();
                Console.WriteLine($"Get ship config : {sw.Elapsed}");
            }

            if (_settings.DiagnosticSettings.ConsolePerformanceWatcher)
            {
                sw = new Stopwatch();
                sw.Start();
            }
            foreach (var player in guildResult.Players)
            {
                foreach (var character in player.Characters)
                {
                    character.Name = characterConfig.FirstOrDefault(p => p.Command == character.Id).Name;
                }

                foreach (var ship in player.Ships)
                {
                    ship.Name = shipConfig.FirstOrDefault(p => p.Command == ship.Id).Name;
                }
            }
            if (_settings.DiagnosticSettings.ConsolePerformanceWatcher)
            {
                sw.Stop();
                Console.WriteLine($"Map character and ship name : {sw.Elapsed}");
            }


            //load to cache
            try
            {
                var b = await _cacheClient.AddToRepositoryCache(functionName, key, guildResult, _settings.CachingSettings.RepositoryCachingInMinutes);
            }
            catch (Exception ex)
            {

            }

            return guildResult;
        }

        public async Task<Guild> GetGuildData(string alias)
        {
            var allyCode = alias.Replace("-", "");

            if (int.TryParse(allyCode, out int result))
            {
                return await GetGuildData(result);
            }
            else
                return null;
        }
    }
}
