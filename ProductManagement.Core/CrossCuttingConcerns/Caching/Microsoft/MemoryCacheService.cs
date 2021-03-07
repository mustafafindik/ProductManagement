using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Core.Utilities.IoC;

namespace ProductManagement.Core.CrossCuttingConcerns.Caching.Microsoft
{
    public  class MemoryCacheService : ICacheService
    {

        private readonly IMemoryCache _cache;
        public MemoryCacheService()
        {
            _cache = ServiceHelper.ServiceProvider.GetService<IMemoryCache>();
        }

        /// <summary>
        ///  Gelen T Gereric Sınıfına ait data key ile cache'den çekilir
        /// </summary>
        /// <typeparam name="T">T Generic Sınıfı</typeparam>
        /// <param name="key">cache Key</param>
        /// <returns>T Genereic Cache data</returns>
        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }
        /// <summary>
        ///   Gelen obje ait data key ile ile cache'den çekilir
        /// </summary>
        /// <param name="key">cache Key</param>
        /// <returns>Object Cache data</returns>
        public object Get(string key)
        {
            return _cache.Get(key);
        }

        /// <summary>
        ///  Key ile birlite gelen data Cache'lenir. 
        /// </summary>
        /// <param name="key">Cache Key</param>
        /// <param name="data">Cache'lenecek Data</param>
        /// <param name="duration">Süre</param>
        public void Add(string key, object data, int duration)
        {
            _cache.Set(key, data, TimeSpan.FromMinutes(duration));
        }


        /// <summary>
        ///  Verilen Keye ait Cache var mı ? 
        /// </summary>
        /// <param name="key">Cache Key</param>
        /// <returns>Var yada yok</returns>
        public bool IsAdd(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        /// <summary>
        ///  Keye ait cache silinir.
        /// </summary>
        /// <param name="key">Cache Key</param>
        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        ///  paterne ait cache değerini siler.
        /// </summary>
        /// <param name="pattern">Cache key Patern. Örneğin ICitySerice.Get</param>
        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_cache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
            }
        }
    }
}
