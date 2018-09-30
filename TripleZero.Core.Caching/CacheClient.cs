using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripleZero.Core.Caching.Strategy;

namespace TripleZero.Core.Caching
{
    public class CacheClient
    {
        private CachingStrategyContext _cachingStrategyContext;
        private CachingModuleStrategy _cachingModuleStrategy;
        private CachingRepositoryStrategy _cachingRepositoryStrategy;

        public CacheClient(CachingStrategyContext cachingStrategyContext, CachingModuleStrategy cachingModuleStrategy, CachingRepositoryStrategy cachingRepositoryStrategy)
        {
            _cachingStrategyContext = cachingStrategyContext;
            _cachingModuleStrategy = cachingModuleStrategy;
            _cachingRepositoryStrategy = cachingRepositoryStrategy;
        }

        public CacheClient(SettingsTripleZeroRepository settingsTripleZeroRepository, SettingsTripleZeroBot settingsTripleZeroBot)
        {
            _cachingStrategyContext = new CachingStrategyContext();
            _cachingModuleStrategy = new CachingModuleStrategy(settingsTripleZeroBot);
            _cachingRepositoryStrategy = new CachingRepositoryStrategy(settingsTripleZeroRepository);
        }

        public string GetCachedDataRepositoryMessage() { return "\n**(cached data^^)**";  }

        public string GetMessageFromModuleCache(string functionName, string key)
        {

            _cachingStrategyContext.SetStrategy(_cachingModuleStrategy);

            string retStr = "";
            string loadingStr = "";

            string strCacheKey = string.Concat(functionName, "-", key);
            var objCache = _cachingStrategyContext.CacheGetFromKey(strCacheKey);
            if (objCache != null)
            {                
                loadingStr = "\n**(cached data^)**";
                retStr = (string)objCache;                
                return string.Concat(loadingStr,retStr);
            }
            return string.Empty;
        }
        public async Task<bool> AddToModuleCache(string functionName, string key, string retStr)
        {         
            _cachingStrategyContext.SetStrategy(_cachingModuleStrategy);
            return await AddToCache(_cachingStrategyContext, functionName, key, retStr);
        }
        public object GetDataFromRepositoryCache(string functionName, string key)
        {          
            _cachingStrategyContext.SetStrategy(_cachingRepositoryStrategy);
            string strCacheKey = string.Concat(functionName, "-", key);
            var objCache = _cachingStrategyContext.CacheGetFromKey(strCacheKey);
            return objCache;
        }
        public async Task<bool> AddToRepositoryCache(string functionName, string key, object obj)
        {       
            _cachingStrategyContext.SetStrategy(_cachingRepositoryStrategy);
            return await AddToCache(_cachingStrategyContext, functionName, key, obj);
        }
        public async Task<bool> AddToRepositoryCache(string functionName, string key, object obj, short minutesBeforeExpiration)
        {
            _cachingStrategyContext.SetStrategy(_cachingRepositoryStrategy);
            return await AddToCache(_cachingStrategyContext, functionName, key, obj, minutesBeforeExpiration);
        }
        private async Task<bool> AddToCache(CachingStrategyContext cachingStrategyContext, string functionName, string key, object retStr)
        {
            await Task.FromResult(1);

            string strCacheKey = string.Concat(functionName, "-", key);
            return cachingStrategyContext.CacheAdd(strCacheKey, retStr);
        }
        private async Task<bool> AddToCache(CachingStrategyContext cachingStrategyContext, string functionName, string key, object retStr, short minutesBeforeExpiration)
        {
            await Task.FromResult(1);

            string strCacheKey = string.Concat(functionName, "-", key);
            return cachingStrategyContext.CacheAdd(strCacheKey, retStr, minutesBeforeExpiration);
        }

        private async Task ClearCache(CachingStrategyContext cachingStrategyContext)
        {
            await Task.FromResult(1);
            cachingStrategyContext.ClearCache();
        }
        public async Task ClearRepositoryCache()
        {           
            _cachingStrategyContext.SetStrategy(_cachingRepositoryStrategy);

            await ClearCache(_cachingStrategyContext);
        }
        public async Task ClearModuleCache()
        {
            _cachingStrategyContext.SetStrategy(_cachingModuleStrategy);

            await ClearCache(_cachingStrategyContext);
        }
        public async Task ClearAllCaches()
        {
            _cachingStrategyContext.SetStrategy(_cachingRepositoryStrategy);
            await ClearCache(_cachingStrategyContext);

            _cachingStrategyContext.SetStrategy(_cachingModuleStrategy);
            await ClearCache(_cachingStrategyContext);
        }

    }
}
