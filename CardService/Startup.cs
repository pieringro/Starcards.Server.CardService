﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardService.Models;
using CardService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMongoDBWrapper;

namespace CardService {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<MonsterCardService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            this.InitDb(env);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitDb(IHostingEnvironment env) {
            string configFile = this.getConfigFile(env);
            var config = new ConfigurationBuilder()
                .AddJsonFile(configFile)
                .Build();

            InitDbContext(config);
        }

        private string getConfigFile(IHostingEnvironment env){
            string configFile = "appsettings.json";
            if (env.IsDevelopment()) {
                configFile = "appsettings.Development.json";
            }
            return configFile;
        }

        private void InitDbContext(IConfiguration config) {
            int retry = 0;
            const int maxRetry = 3;
            Exception ex = null;
            while (retry < maxRetry) {
                try {
                    Console.WriteLine("Connect to MongoDB. Try number {0}", (retry + 1));
                    DBContext.GetInstance(config);
                    break;
                } catch (Exception e) {
                    ex = e;
                    Console.WriteLine("Error during startup DB. Retry...");
                    System.Threading.Thread.Sleep(1000);
                }
                retry++;
            }
            if (retry == maxRetry) {
                throw ex;
            }

        }
    }
}