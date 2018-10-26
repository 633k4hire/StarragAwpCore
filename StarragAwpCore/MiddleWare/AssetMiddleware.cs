using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarragAwpCore.Extensions;
using StarragAwpCore.Helpers;

namespace StarragAwpCore.MiddleWare
{
    public class AssetMiddleware
    {
       
        public IDistributedCache _cache { get; set; }

        private readonly RequestDelegate _next;

        public AssetMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            //HttpContext.ApplicationServices.GetRequiredService<IMemoryCache>()
            
            _cache = cache;
            _next = next;
            
        }

        public async Task Invoke(HttpContext context)
        {
            // Do something with context near the beginning of request processing.

            //check if there is a cache of the List<Asset>
            var a = _cache;

            
            //if (Program.GlobalAssetCache!=null)
            //{
            //    //Program.GlobalAssetCache = await Pull.Assets();

            //    // check for signalr flag
            //}
            //else
            //{
            //    Program.GlobalAssetCache = await Pull.Assets();
            //}

            try
            {
                //var assets = context.Session.Pull<List<Asset>>("sessionAssetCache");

                ////get session data for user and update it if it does not exist
                ////ContextProviderExtension.HttpContextAccessor.HttpContext
                //if (assets == null)
                //{
                //    //Pull Assets and Cache
                //    var request = await SqlCore.SQLfunc.OpenConnectionAsync(new SqlCore.SQL_Request());
                //    request = await SqlCore.SQLfunc.GetAssetsAsync(request);
                //    assets = request.Tag as List<Asset>;
                //    //Push into Session Cache
                //    context.Session.Push<List<Asset>>("sessionAssetCache", assets);
                //}
            }
            catch
            {
            }

            await _next.Invoke(context);

            // Clean up.
        }
    }

    public static class AssetMiddlewareExtensions
    {
        public static IApplicationBuilder UseAssetMiddleware(this IApplicationBuilder builder, IDistributedCache cache)
        {
           // AssetMiddleware._cache = cache;
            return builder.UseMiddleware<AssetMiddleware>();
        }
    }
}

