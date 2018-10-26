using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarragAwpCore.Data;
using StarragAwpCore.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using StarragAwpCore.MiddleWare;
using StarragAwpCore.Helpers;

namespace StarragAwpCore
{
    public class Startup
    {
       
        public static string AppName = "StarragAwpCore";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = ".AwpCore.Session";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });

            //services.Configure<CookiePolicyOptions>(options =>
            //{               
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailService, EmailSender>();

            //Add SqlService for Distrubuted Data Cache with LASTSOUL DB
            services.AddSingleton<ISqlService, SqlService>();

            //Add Cache for Distrubuted Data Cache in-memory
            services.AddSingleton<IDistributedCacheWithSqlService, DistributedCacheWithSql>();

            services.AddHttpContextAccessor();

            //Add Web Farm Cache
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDistributedCache cache, IHttpContextAccessor accessor)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //image resize Middleware needs injection before static files
            app.UseImageResizerMiddleware();

            //Asset Middleware
            app.UseAssetMiddleware(cache);        

            app.UseStaticFiles();

            //app.UseCookiePolicy();                        

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc();
            
            //Enable SEssion           

            app.UseSignalR(builder =>
            {
                builder.MapHub<ClientHub>("/clientHub");
            });

            //Set Middleware aka "HttpHandler or HttpModule"

            //var assets = Pull.Assets();
            //cache.Push<List<Asset>>("MasterAssetCache", assets.Result, 24);

            
            cache.Push<string>("LastServerStartTime",DateTime.Now.ToString(),1);
        }
    }
   
}
