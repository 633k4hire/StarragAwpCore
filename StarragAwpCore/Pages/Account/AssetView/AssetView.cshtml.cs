using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using StarragAwpCore.Data;
using StarragAwpCore.Helpers;
using StarragAwpCore.Services;
using StarragAwpCore.Extensions;

namespace StarragAwpCore.Pages.Account.AssetView
{
    public class AssetViewModel : StarragPageModel
    {
        public AssetViewModel(IEmailService emailService,ISqlService sqlService,IDistributedCacheWithSqlService cacheService,IHttpContextAccessor httpContextAccessor)
        {
            this._emailSender = emailService;
            _sqlService = sqlService as SqlService;
            _cacheService = cacheService as DistributedCacheWithSql;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Use the CacheService to see if anything needs to be pulled down(use signalr to set this flag), if not then CacheService Will pull from Sqlservice
        ///   This will push to both session and global cache, but not sql
        ///   this.PushAll<int>("TestInt", 3,false);
        ///
        ///   This will push to both session and global cache, and to sql
        ///   this.PushAll<int>("TestInt", 3, true);
        ///
        ///    var myInt = this.PullAll<int>("TestInt");
        /// </summary>
   
        //private List<Asset> mAssets;

        //public List<Asset> AssetCache
        //{
        //    get
        //    {
        //        var task = this.PullAll<List<Asset>>(Startup.AssetCache); // or this.PullAssetCache();      
        //        Task.WaitAll(task);
        //        mAssets = task.Result;
        //        return mAssets;
        //    }
        //    set
        //    {
        //        mAssets = value;
        //    }
        //}


        public void OnGet()
        {
            //Pull Cache of AssetList
            //var task = this.PullAll<List<Asset>>(Startup.AssetCache); // or this.PullAssetCache();      
            //Task.WaitAll(task);
            //AssetCache = task.Result;
            var a = this.AssetCache;
            
            Message = "This is the AssetView Page";            
        }
    }
}