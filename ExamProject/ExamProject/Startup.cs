using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ExamProject.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ExamProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<examDBContext>(x=>x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAppRepository, AppRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IMyHtmlAgilityRepository, MyHtmlAgilityRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Auth/Login/";
                });
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()

                    .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
