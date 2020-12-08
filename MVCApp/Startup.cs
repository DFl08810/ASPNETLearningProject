using CommandCore.Factories;
using CommandCore.Services;
using DataCore;
using DataCore.DataAccess;
using DataCore.Entities;
using IdentityLib;
using IdentityLib.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCApp.EntityServices;
using MVCApp.Models.Factories;
using MVCApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp
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

            services.AddControllersWithViews();
            #region PresentationServices
            services.AddTransient<IArticleModelFactory, ArticleModelFactory>();
            services.AddTransient<IArticleModelService, ArticleModelService>();
            services.AddTransient<IAccountModelFactory, AccountModelFactory>();
            services.AddTransient<IAccountModelService, AccountModelService>();
            #endregion
            #region CommandServices
            services.AddTransient<IArticleFactory, ArticleFactory>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IAccountFactory, AccountFactory>();
            services.AddTransient<IAccountService, AccountService>();
            #endregion
            #region DataServices
            services.AddDbContext<DataContext>();
            services.AddTransient<IDataAccess<Article>, ArticleDataAccess>();
            services.AddTransient<IDataAccess<Account>, AccountDataAccess>();
            #endregion
            #region Identity
            services.AddDbContext<IdentityDataContext>();
            services.AddIdentity<User, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>();
            //Claims policy settings
            services.AddAuthorization(options =>
            {
                //setup for article publishing rights
                options.AddPolicy("AllowPublishing", policy => policy.RequireClaim("CanPublish"));
            });
            #endregion
            services.AddTransient<ICredentialsService, CredentialsService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ICredentialsService cred)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            cred.InitializeDefaults();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
