using Common.Layer.HttpClient;
using Common.Layer.Models;
using File.Repository.Implementations;
using File.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IO;

namespace DotnetCoreWithAngular6
{
    public class Startup
    {
        public IConfigurationRoot ConfigurationRoot { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            ConfigurationRoot = builder.Build();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = ConfigurationRoot.Get<Configuration>();
            services.AddTransient<Configuration>((p) => config);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist/ClientApp";
            });

            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
            .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                 builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });
            
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IFeederRepository, FeederRepository>();
            services.AddTransient<ITransformerRepository, TransformerRepository>();

            services.AddTransient<ISendMessage, SendMessage>();
            services.AddTransient<IHttpClientWrapper, HttpclientWrapper>();

            services.AddTransient<Data.Repository.Interfaces.IUserRepository, Data.Repository.Implementations.UserRepository>();
            services.AddTransient<Data.Repository.Interfaces.IAddressRepository, Data.Repository.Implementations.AddressRepository>();
            services.AddTransient<Data.Repository.Interfaces.IFeederRepository, Data.Repository.Implementations.FeederRepository>();
            services.AddTransient<Data.Repository.Interfaces.ITransformerRepository, Data.Repository.Implementations.TransformerRepository>();
            services.AddTransient<Data.Repository.Interfaces.ISendMessage, Data.Repository.Implementations.SendMessage>();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });           
        }
    }
}
