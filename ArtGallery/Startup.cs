using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtGallery.Data;
using ArtGallery.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtGallery
{
    public class Startup
    {
        public IConfiguration _config { get; }

        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Adding db context and sql server services throughout our application
            services.AddDbContext<ArtGalleryContext>( cfg => {
                cfg.UseSqlServer(_config.GetConnectionString("ArtGalleryConnectionString"));               
            });

            services.AddTransient<IMailService, NullMailService>();
            // support for real mail service

            // Adds Mvc to application services
            services.AddMvc();           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Middleware! works in sequence, keep that in mind.

            // Enable HTML developer exceptions in development enviroment
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }     
            else
            {
                // When encountering errors in production we return a razor page Error     
                app.UseExceptionHandler("/error");
            }

            // use default files such as index.html
            //app.UseDefaultFiles();

            // Serve static files
            app.UseStaticFiles();

            // Nuget package useNodeModules serves node_modules to the client
            app.UseNodeModules(env);

            // Use MVC pattern in the asp.net application
            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default", 
                    "/{controller}/{action}/{id?}", 
                    new { Controller = "App", Action="Index"});
            });
        }
    }
}
