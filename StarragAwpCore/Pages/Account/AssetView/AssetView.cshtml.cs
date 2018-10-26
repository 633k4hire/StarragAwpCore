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
        /// </summary>
        public List<Asset> AssetCache { get; set; }
        public void OnGet()
        {
            
            //This will push to both session and global cache, but not sql
              this.PushAll<int>("TestInt", 3,false);

            //This will push to both session and global cache, and to sql
              this.PushAll<int>("TestInt", 3, true);       

            var myInt= this.PullAll<int>("TestInt");

            Message = "This is the AssetView Page";            
        }
    }
}