using System;
using System.Threading.Tasks;
using AutoMapper;
using SWGoH.Model;
using TripleZero.Core.Caching;
using TripleZero.Repository.SWGoHHelpRepository;

namespace TripleZero.Core
{
    public class PlayerContext : IPlayerContext
    {
        private SettingsTripleZeroRepository _settings;
        private IMapper _mapper;
        private CacheClient _cacheClient;
        public PlayerContext(SettingsTripleZeroRepository settings, CacheClient cacheClient, IMapper mapper)
        {
            _settings = settings;
            _mapper = mapper;
            _cacheClient = cacheClient;
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
