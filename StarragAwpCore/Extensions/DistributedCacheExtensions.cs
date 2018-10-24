using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarragAwpCore
{
    public static class Cache
    {
        public static  void Push<T>(this IDistributedCache _cache, string key, T item, int expirationInHours)
        {

            var json = JsonConvert.SerializeObject(item);

             _cache.SetString(key, json, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(expirationInHours)
            });
        }

        public static  T Pull<T>(this IDistributedCache _cache, string key)
        {
            var json = _cache.GetString(key);

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static async Task PushAsync<T>( this IDistributedCache _cache, string key, T item, int expirationInHours)
        {
            
            var json = JsonConvert.SerializeObject(item);

            await _cache.SetStringAsync(key, json, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(expirationInHours)
            });
        }

        public static async Task<T> PullAsync<T>(this IDistributedCache _cache, string key)
        {
            var json = await _cache.GetStringAsync(key);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
