using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using StarragAwpCore.Data;
using StarragAwpCore.Helpers;
using StarragAwpCore.Services;
using StarragAwpCore.Extensions;
using Microsoft.AspNetCore.Http;

namespace StarragAwpCore.Pages
{
    public class AboutModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailSender;
        private readonly SqlService _sqlService;
        private readonly DistributedCacheWithSql _cacheService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public AboutModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            ISqlService sqlService, 
            IDistributedCacheWithSqlService cacheService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailService;
            _sqlService = sqlService as SqlService;
            _cacheService = cacheService as DistributedCacheWithSql;
            _httpContextAccessor = httpContextAccessor;            
        }

        public string Message { get; set; }
       
        public async void OnGet()
        {
            var a = _session;
            //SESSION!!!!!!!!!!!!!! SUCCESS
            
            a.SetString("testString", "MattLovesDee");
            var ret = a.GetString("testString");

            Message = "Your application description page.";

            //send email
            await _emailSender.SendEmailAsync("", "", "");         

            //Use SqlService 
            var request = await _sqlService.OpenConnection();
            await request.AddAsync("StarragAwpCore", "notxml");
            await request.RemoveAsync("StarragAwpCore");

            //Optional sql access
            await _sqlService._request.GetAsync("StarragAwpCore");

            //CacheSerice access
            var servertime = await _cacheService.PullAsync<string>("LastServerStartTime");

            _cacheService.PushAll<string>("mm", "MandD", _cacheService._session);

            //var result = await _cacheService.PullAllAsync<string>("TestString",_session);


        }
    }
}
