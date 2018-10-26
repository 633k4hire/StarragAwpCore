using Microsoft.AspNetCore.Http;
using StarragAwpCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarragAwpCore.Extensions
{
    public static class DistributedCacheWithSqlExtensions
    {
        public static async Task PushAll<T>(this DistributedCacheWithSql cache, string key, T value, ISession session, bool pushSql = true, bool PushSession = true, bool PushDistributedCache = true)
        {
            if (PushSession)
            {
                await cache._session.PushAsync<T>(key, value);
            }
            if (PushDistributedCache)
            {
                await cache._Cache.PushAsync<T>(key, value, 1);
            }
            if (pushSql)
            {
                await StarragAwpCore.Push.SqlCache<T>(new KeyValuePair<string, T>(key, value));
            }

        }

        public static async Task<T> PullAll<T>(this DistributedCacheWithSql cache, string key, ISession session)
        {
            var sessionCacheResult = await cache._session.PullAsync<T>(key);
            if (sessionCacheResult == null)
            {
                var distCacheResult = await cache._Cache.PullAsync<T>(key);
                if (distCacheResult != null)
                {
                    await cache._session.PushAsync<T>(key, distCacheResult);
                    return distCacheResult;
                }
                else
                {
                    //there is no local cache check sql
                    var sqlCacheResult = await StarragAwpCore.Pull.SqlCache<T>(key);
                    if (sqlCacheResult != null)
                    {
                        //push to both session and dist caches
                        await cache._Cache.PushAsync<T>(key, sqlCacheResult, 1);
                        await cache._session.PushAsync<T>(key, sqlCacheResult);
                        return sqlCacheResult;
                    }
                    else
                    {
                        //does not exist at all
                        return default(T);
                    }
                }
            }
            else
            {
                //IF NOT UP T ODATE IT WILL PULL FFROM THE DIST CACHE
                //check if session is up to date to dist?
                //push to sql?
                //var distCacheResult = await _Cache.PullAsync<T>(key);
                //if (distCacheResult != null)
                //{
                //    //see if they dont match
                //    if (!distCacheResult.Equals(sessionCacheResult))
                //    {
                //        //push to session?                        
                //        await _session.PushAsync<T>(key, distCacheResult);

                //    }
                //    return distCacheResult;
                //}
                //else
                //{
                //push to distcache?
                return sessionCacheResult;
                //}

            }
        }


    }
}
