using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApi.ConfigureInMemory;
using AspNetCoreApi.Options;
using AspNetCoreApiData;
using AspNetCoreData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCoreApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            var myWindow = new MyWindow();
            var dictionaryInMemoryDemo = myWindow.GetDictionary();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddXmlFile("XmlConfiguration.xml")
                .AddInMemoryCollection(dictionaryInMemoryDemo);
                

            Configuration = builder.Build();
            var xxx = Configuration["Logging:Debug:LogLevel:Default"];//read appsettings.json
            var yyy = Configuration["wizard:Harry:age"];//read xml

            var left = configuration.GetValue<int>("App:MainWindow:Left", 80);
            var window = new MyWindowDemo();
            Configuration.GetSection("App:MainWindow").Bind(window);
            var pp=window.GetType().GetProperties();
            var zzz = window.Height;

            //            var appConfig = new AppSettings();
            //            configuration.GetSection("App").Bind(appConfig);
            //            var app1 = appConfig.Window.Height;

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEntityFrameworkConfig(options =>
                    options.UseSqlServer(Configuration.GetConnectionString(
                        "DefaultConnection"))
                )
                .Build();
            var key1 = Configuration["key1"];

            
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //update-database
            
            services.AddDbContext<ApiDbcontext>(option => option.UseSqlServer(Configuration.GetConnectionString(
                "DefaultConnection")));

            //1 
            services.Configure<MyOptions>(Configuration);
            //2
            services.Configure<MyOptionsWithDelegateConfig>(myoption =>
            {
                myoption.Option1 = "delegate option";
                myoption.Option2 = 500;
            });
            //3
            services.Configure<MySnapshotOptions>(Configuration);
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment environment)
        {
            var myConfig = Configuration["MyConfig"];
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            if (environment.IsProduction() || environment.IsStaging())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseMvc();


        }
    }
}
