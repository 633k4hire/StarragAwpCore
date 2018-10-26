using Newtonsoft.Json;
using StarragAwpCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarragAwpCore
{
    public static class Push
    {
        public static async Task<bool> SqlCache<T>(KeyValuePair<string, T> keypair)
        {
            try
            {
                var request = await SqlCore.SQLfunc.CacheServiceGetAsync(new SqlCore.SQL_Request(), keypair.Key, false);
                if (request.Tag==null)
                {
                    var a = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(keypair.Value));
                    request = await SqlCore.SQLfunc.CacheServiceAddAsync(new SqlCore.SQL_Request(), keypair.Key, a, true);
                    return true;
                }
                else
                {
                    var a = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(keypair.Value));
                    request = await SqlCore.SQLfunc.CacheServiceUpdateAsync(new SqlCore.SQL_Request(), keypair.Key, a, true);
                    return true;
                }                
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public static class Pull
    {

        public static async Task<T> SqlCache<T>(string KeyName)
        {
            try
            {
                var request = await SqlCore.SQLfunc.CacheServiceGetAsync(new SqlCore.SQL_Request(), KeyName, true);
                if (request.Tag == null)
                {
                    return default(T);
                }
                else
                {
                    var ret = (KeyValuePair<string, byte[]>)request.Tag;
                    var a = Encoding.UTF8.GetString(ret.Value);
                    return JsonConvert.DeserializeObject<T>(a);
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static async Task<List<Asset>> Assets()
        {
            try
            {
                var request = await SqlCore.SQLfunc.OpenConnectionAsync(new SqlCore.SQL_Request());
                request = await SqlCore.SQLfunc.GetAssetsAsync(request);
                var assets = request.Tag as List<Asset>;
                return assets;
            }
            catch (Exception)
            {
                return new List<Asset>();
            }           
        }

        public static async Task<Asset> Asset(string assetNumber)
        {
            try
            {
                var request = await SqlCore.SQLfunc.OpenConnectionAsync(new SqlCore.SQL_Request());
                request = await SqlCore.SQLfunc.GetAssetAsync(request,assetNumber);
                var asset = request.Tag as Asset;
                return asset;
            }
            catch (Exception)
            {
                return new Asset();
            }
        }

    }
}
