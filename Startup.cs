using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using teleRDV.Models;

namespace alfred
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            teleRDV.Models.Context.Init();
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<DataOptions>(Configuration.GetSection("DataOptions"));
            services.AddTransient<Context>();
            
            services.AddIdentity<User,Role>()
                .RegisterMongoStores<User, Role>(Configuration.GetConnectionString("DefaultConnection"))                              
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
            .AddTemporarySigningCredential()
            .AddInMemoryScopes(IdentityServerConfig.GetScopes())
            .AddInMemoryClients(IdentityServerConfig.GetClients())
            .AddAspNetIdentity<User>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentity();
            app.UseIdentityServer();
            
            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:5000",
                ScopeName = "api1",
                
                RequireHttpsMetadata = false
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
