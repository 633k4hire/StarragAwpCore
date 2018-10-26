using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using StarragAwpCore.Extensions;

namespace StarragAwpCore.Services
{
    public class DistributedCacheWithSql : IDistributedCacheWithSqlService
    {
        public static readonly string AppName = "StarragAwpCore_CacheService";
        public IDistributedCache _Cache;
        public ISqlService _Sql;
        public ISession _session => _httpaccessor.HttpContext.Session;
        public IHttpContextAccessor _httpaccessor;
        public DistributedCacheWithSql(IDistributedCache cache, ISqlService sql, IHttpContextAccessor httpaccessor)
        {
            _Cache = cache;
            _Sql = sql;
            _httpaccessor = httpaccessor;
            
        }

        public byte[] Get(string key)
        {                       //then check global
            var item = _Cache.Get(key); 
        

            return item;
        }

        public async Task<byte[]> GetAsync(string key, CancellationToken token = default(CancellationToken))
        {
            var item = await _Cache.GetAsync(key, token);
     
            return item;
        }

        public void Refresh(string key)
        {
            //maybe here we pull down from sql all asset data?
            _Cache.Refresh(key);
        }

        public async Task RefreshAsync(string key, CancellationToken token = default(CancellationToken))
        {
            //maybe here we pull down from sql all asset data?
            await _Cache.RefreshAsync(key, token);
        }

        public void Remove(string key)
        {
            _Cache.Remove(key);
        }

        public async Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
        {
            await _Cache.RemoveAsync(key, token);
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            _Cache.Set(key, value, options);
        }

        public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
        {
            await _Cache.SetAsync(key, value, options, token);
        }

        public async Task PushAllAsync<T>(string key, T value, ISession session=null, bool pushSql=true, bool PushDistributedCache = true)
        {
            if (session!=null)
            {
                await session.PushAsync<T>(key, value);
            }
            if (PushDistributedCache)
            {
                await _Cache.PushAsync<T>(key, value, 1);
            }
            if (pushSql)
            {
                await StarragAwpCore.Push.SqlCache<T>(new KeyValuePair<string, T>(key, value));
            }
            
        }

        public async Task<T> PullAllAsync<T>(string key, ISession session = null)
        {
            T sessionCacheResult=default(T);
            if (session != null)
            {
                sessionCacheResult = await session.PullAsync<T>(key);
            }            

            if (sessionCacheResult==null)
            {
                var distCacheResult = await _Cache.PullAsync<T>(key);
                if (distCacheResult !=null)
                {                   
                    await session.PushAsync<T>(key, distCacheResult);
                    return distCacheResult;
                }
                else
                {
                    //there is no local cache check sql
                    var sqlCacheResult = await StarragAwpCore.Pull.SqlCache<T>(key);
                    if (sqlCacheResult != null)
                    {
                        //push to both session and dist caches
                        await _Cache.PushAsync<T>(key, sqlCacheResult,1);
                        await session.PushAsync<T>(key, sqlCacheResult);
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
