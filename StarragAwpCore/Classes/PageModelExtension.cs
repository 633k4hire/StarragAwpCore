using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarragAwpCore.Data;
using StarragAwpCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Microsoft.AspNetCore.Mvc.RazorPages
{
    public class StarragPageModel: PageModel
    {
        public StarragPageModel()
        {

        }
        public UserManager<ApplicationUser> _userManager;
        public SignInManager<ApplicationUser> _signInManager;
        public IEmailService _emailSender;
        public SqlService _sqlService;
        public DistributedCacheWithSql _cacheService;
        public IHttpContextAccessor _httpContextAccessor;
        public ISession _session => _httpContextAccessor.HttpContext.Session;

        public string Message { get; set; }

        public StarragPageModel(            
            IEmailService emailService,
            ISqlService sqlService,
            IDistributedCacheWithSqlService cacheService,
            IHttpContextAccessor httpContextAccessor)
        {            
            _emailSender = emailService;
            _sqlService = sqlService as SqlService;
            _cacheService = cacheService as DistributedCacheWithSql;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}


namespace StarragAwpCore.Extensions
{
    public static class PageModelExtension
    {
        public static async Task<T> PullAll<T>(this StarragPageModel pageModel, string key)
        {
            return await pageModel._cacheService.PullAll<T>(key, pageModel._cacheService._session);
           
        }
        public static async Task PushAll<T>(this StarragPageModel pageModel, string key, T value, bool pushSql = false, bool PushSession = true, bool PushDistributedCache = true)
        {
            await pageModel._cacheService.PushAll<T>(key, value, pageModel._cacheService._session, pushSql, PushSession, PushDistributedCache);
        }

    }


}