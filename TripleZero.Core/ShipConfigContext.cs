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
    public class ShipConfigContext : IShipConfigContext
    {
        //private IShipRepository _ShipRepository;
        private CacheClient _cacheClient;
        private IMapper _mapper;
        public ShipConfigContext(CacheClient cacheClient, IMapper mapper)
        {
            _cacheClient = cacheClient;
            _mapper = mapper;
        }

        public async Task<List<ShipConfig>> GetShipsConfig()
        {
            List<ShipConfig> ShipsConfig;

            string functionName = "GetShipConfig";
            string key = "get";
            var objCache = _cacheClient.GetDataFromRepositoryCache(functionName, key);
            if (objCache != null)
            {
                ShipsConfig = (List<ShipConfig>)objCache;
                ShipsConfig.ForEach(p=>p.LoadedFromCache = true);
                return ShipsConfig;
            }

            var repo = new ShipRepository(_mapper);
            ShipsConfig =  await repo.GetShips();

            //load to cache
            try
            {
                var b = await _cacheClient.AddToRepositoryCache(functionName, key, ShipsConfig, 120);
            }
            catch (Exception ex)
            {

            }

            return ShipsConfig;
        }
    }
}
