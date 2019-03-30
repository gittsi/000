using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SWGoH.Model;
using SWGoH.Model.Settings.TripleZeroRepository;
using TripleZero.Core.Caching;
using TripleZero.Repository.Mapping;
using TripleZero.Repository.MongoDBRepository;
using TripleZero.Repository.SWGoHHelpRepository;

namespace TripleZero.Core
{
    public class PlayerContext : IPlayerContext
    {
        private SettingsTripleZeroRepository _settings;
        private IMapper _mapper;
        private CacheClient _cacheClient;
        private MongoDBConnectionHelper _mongoDBConnectionHelper;

        public PlayerContext(SettingsTripleZeroRepository settings, CacheClient cacheClient, IMapper mapper)
        {
            _settings = settings;
            if (_mapper == null) _mapper = new MappingConfiguration().GetConfigureMapper();
            _cacheClient = cacheClient;
            new MongoDBConnectionHelper(_settings.MongoDBSettings);
            _mongoDBConnectionHelper = new MongoDBConnectionHelper(_settings.MongoDBSettings);
        }

        public PlayerContext(MongoDBConnectionHelper mongoDBConnectionHelper, IMapper mapper)
        {
            //_settings = settings;
            if (_mapper == null) _mapper = new MappingConfiguration().GetConfigureMapper();
            //_cacheClient = cacheClient;
            _mongoDBConnectionHelper = mongoDBConnectionHelper;
        }

        public async Task<List<Player>> GetGuildPlayersData(string guildAlias)
        {
            var guildName = "";
            var guildConfigRepo = new GuildConfigRepository(_mongoDBConnectionHelper, _mapper);
            var guildConfig = await guildConfigRepo.GetGuildConfigByAlias(guildAlias);

            if (guildConfig == null) guildName = guildAlias;
            else guildName = guildConfig.Name;

            var playerRepo = new PlayerRepository(_mongoDBConnectionHelper, _mapper);
            var players = await playerRepo.GetByGuild(guildName);

            var characterConfigContext = new CharacterConfigContext(_cacheClient, _mapper);
            var characterConfig = await characterConfigContext.GetCharactersConfig();

            var shipConfigContext = new ShipConfigContext(_cacheClient, _mapper);
            var shipConfig = await shipConfigContext.GetShipsConfig();

            foreach (var player in players)
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
            return players;
        }

        public async Task<Player> GetPlayerData(int allyCode)
        {
            string functionName = "GetPlayerRepo";
            string key = allyCode.ToString();
            var objCache = _cacheClient.GetDataFromRepositoryCache(functionName, key);
            if (objCache != null)
            {
                var player = (Player)objCache;
                player.LoadedFromCache = true;
                return player;
            }

            var playerRepo = new SWGoHHelpPlayerRepository(_settings.SWGoHHelpSettings, _cacheClient, _mapper);
            var playerResult = await playerRepo.GetPlayer(allyCode);


            var characterConfigContext = new CharacterConfigContext(_cacheClient, _mapper);
            var characterConfig = await characterConfigContext.GetCharactersConfig();

            var shipConfigContext = new ShipConfigContext(_cacheClient, _mapper);
            var shipConfig = await shipConfigContext.GetShipsConfig();

            foreach (var character in playerResult.Characters)
            {
                character.Name = characterConfig.FirstOrDefault(p => p.Command == character.Id).Name;
            }

            foreach (var ship in playerResult.Ships)
            {
                ship.Name = shipConfig.FirstOrDefault(p => p.Command == ship.Id).Name;
            }

            //load to cache
            try
            {
                var b = await _cacheClient.AddToRepositoryCache(functionName, key, playerResult, _settings.CachingSettings.RepositoryCachingInMinutes);
            }
            catch (Exception ex)
            {

            }

            return playerResult;
        }

        public async Task<Player> GetPlayerData(string alias)
        {
            var allyCode = alias.Replace("-", "");

            if (int.TryParse(allyCode, out int result))
            {
                return await GetPlayerData(result);
            }
            else
                return null;
        }
    }
}
