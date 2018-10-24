using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using StarragAwpCore.Data;
using StarragAwpCore.Services;

namespace StarragAwpCore.Pages
{
    public class AboutModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailSender;
        private readonly SqlService _sqlService;
        private readonly ICacheService _cacheService;
        private readonly IDistributedCache _cache;

        public AboutModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            ISqlService sqlService, 
            ICacheService cacheService,
            IDistributedCache cache)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailService;
            _sqlService = sqlService as SqlService;
            _cacheService = cacheService as CacheService;
            _cache = cache;
        }

        public string Message { get; set; }
       
        public async void OnGet()
        {

            Message = "Your application description page.";

            //send email
            await _emailSender.SendEmailAsync("", "", "");         

            //Use SqlService 
            var request = await _sqlService.OpenConnection();
            await request.AddAsync("StarragAwpCore", "notxml");

            //Optional sql access
            await _sqlService._request.UpdateAsync("StarragAwpCore", "notxmlalso");

          

            //use distributed cache
            await _cache.PushAsync<int>("myInt", 3, 1);
            int myInt = await _cache.PullAsync<int>("myInt");

            //Use Push Class IS SUPER CLAS THAT IS CUSTOM WRITTEN TO MAKE ALL CACHING EASIER
            await Push.UserData();
            var str = await Pull.UserData();

        }
    }
}
