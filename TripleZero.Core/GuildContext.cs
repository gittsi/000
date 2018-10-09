using System;
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
            string functionName = "GetGuildRepo";
            string key = allyCode.ToString();
            var objCache = _cacheClient.GetDataFromRepositoryCache(functionName, key);
            if (objCache != null)
            {
                var guild = (Guild)objCache;
                guild.LoadedFromCache = true;
                return guild;
            }

            var guildRepo = new SWGoHHelpGuildRepository(_settings.SWGoHHelpSettings, _cacheClient, _mapper);
            var guildResult = await guildRepo.GetGuild(allyCode);

            var characterConfigContext = new CharacterConfigContext(_cacheClient, _mapper);
            var characterConfig = await characterConfigContext.GetCharactersConfig();

            var shipConfigContext = new ShipConfigContext(_cacheClient, _mapper);
            var shipConfig = await shipConfigContext.GetShipsConfig();
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
