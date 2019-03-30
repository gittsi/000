using AutoMapper;
using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripleZero.Core.Caching;
using TripleZero.Repository.SWGoHAPIRepository;

namespace TripleZero.Core
{
    public class CharacterConfigContext : ICharacterConfigContext
    {
        //private ICharacterRepository _characterRepository;
        private CacheClient _cacheClient;
        private IMapper _mapper;
        public CharacterConfigContext(CacheClient cacheClient, IMapper mapper)
        {
            _cacheClient = cacheClient;
            _mapper = mapper;
        }

        public CharacterConfigContext(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<CharacterConfig>> GetCharactersConfig()
        {
            List<CharacterConfig> charactersConfig;

            string functionName = "GetCharacterConfig";
            string key = "get";

            if (_cacheClient != null)
            {
                var objCache = _cacheClient.GetDataFromRepositoryCache(functionName, key);
                if (objCache != null)
                {
                    charactersConfig = (List<CharacterConfig>)objCache;
                    charactersConfig.ForEach(p => p.LoadedFromCache = true);
                    return charactersConfig;
                }
            }
            

            var repo = new CharacterRepository(_mapper);
            charactersConfig =  await repo.GetCharacters();

            //load to cache
            try
            {
                if (_cacheClient != null) await _cacheClient.AddToRepositoryCache(functionName, key, charactersConfig, 120);
            }
            catch (Exception ex)
            {

            }

            return charactersConfig;
        }
    }
}
