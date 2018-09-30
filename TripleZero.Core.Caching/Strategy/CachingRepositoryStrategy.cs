using System;
using System.Collections.Generic;
using System.Text;
using CacheManager.Core;
using SWGoH.Model;

namespace TripleZero.Core.Caching.Strategy
{
    public class CachingRepositoryStrategy : CachingStrategy
    {
        ICacheManager<object> _cacheFactory;
        SettingsTripleZeroRepository _settings;
        public CachingRepositoryStrategy(SettingsTripleZeroRepository settings)
        {
            _cacheFactory = new CachingFactory(new CacheConfiguration()).GetFactoryRepository();
            _settings = settings;
        }

        public override bool CacheAdd(string key, object obj, short minutesBeforeExpiration)
        {
            bool isAdded = _cacheFactory.Add(key, obj);
            _cacheFactory.Expire(key, ExpirationMode.Absolute, TimeSpan.FromMinutes(minutesBeforeExpiration));
            return isAdded;
        }
        public override bool CacheAdd(string key, object obj)
        {
            bool isAdded = _cacheFactory.Add(key, obj);
            if(!isAdded)
            {                
                return false;
            }
            int minutesBeforeExpiration = _settings.CachingSettings.RepositoryCachingInMinutes;

            _cacheFactory.Expire(key, ExpirationMode.Absolute, TimeSpan.FromMinutes(minutesBeforeExpiration));             
            return isAdded;
        }

        public override object CacheGetFromKey(string key)
        {
            return _cacheFactory.Get(key);
        }

        public override void ClearCache()
        {
            _cacheFactory.Clear();
        }
    }
}
