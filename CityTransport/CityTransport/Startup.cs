using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CityTransport.Data;
using CityTransport.Models;
using CityTransport.Web.Common.Middlewares;
using CityTransport.Services.Admin.Interfaces;
using CityTransport.Services.Admin;
using Microsoft.AspNetCore.Routing;

namespace CityTransport
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<CityTransprtDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(opt =>
             {
                 opt.Password.RequireDigit = false;
                 opt.Password.RequiredLength = 6;
                 opt.Password.RequiredUniqueChars = 0;
                 opt.Password.RequireLowercase = false;
                 opt.Password.RequireNonAlphanumeric = false;
                 opt.Password.RequireUppercase = false;
             })
            .AddDefaultUI()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<CityTransprtDbContext>();

            RegisterServiceLayer(services);
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true); // seting route lowercase

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.SeedData();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=User}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegisterServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICustomerCardService, CustomerCardService>();
            services.AddScoped<ISubscriptionCardService, SubscriptionCardService>();
            services.AddScoped<ILineService, LineService>();
            services.AddScoped<IStationService, StationService>();
            services.AddScoped<ITimeTableService, TimeTableService>();
            services.AddScoped<IRouteService, RouteService>();

        }
    }
}
