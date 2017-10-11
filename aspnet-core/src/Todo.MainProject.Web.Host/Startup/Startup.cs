using System;
using System.IO;
using System.Linq;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Todo.MainProject.Configuration;
using Todo.MainProject.Identity;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Abp.Extensions;
using Abp.PlugIns;
using Abp.Resources.Embedded;
using Todo.MainProject.Authentication.JwtBearer;
using Todo.MainProject.Web.Host.Services;

#if FEATURE_SIGNALR
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using Todo.MainProject.Owin;
using Abp.Owin;
#endif


namespace Todo.MainProject.Web.Host.Startup
{
    public class Startup
    {
        private const string DefaultCorsPolicyName = "localhost";

        private readonly IConfigurationRoot _appConfiguration;
        private readonly string _contentRootPath;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
            _contentRootPath = env.ContentRootPath;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //MVC
            services.AddMvc(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory(DefaultCorsPolicyName));
            });

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            //Configure CORS for angular2 UI
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    //App:CorsOrigins in appsettings.json can contain more than one address with splitted by comma.
                    builder
                        .WithOrigins(_appConfiguration["App:CorsOrigins"].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o => o.RemovePostFix("/")).ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            IPluginService pluginService;
            var pluginPath = Path.Combine(_contentRootPath, "PlugIns");
            if (Directory.Exists(pluginPath))
            {
                pluginService = new PluginService(pluginPath);
                services.AddSingleton<IPluginService>(pluginService);
            }
            else
            {
                pluginService = new NullPluginService();
                services.AddSingleton<IPluginService>(pluginService);
            }

            //Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "MainProject API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);

                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                // Assign scope requirements to operations based on AuthorizeAttribute
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            //Configure Abp and Dependency Injection
            return services.AddAbp<MainProjectWebHostModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );

                if (pluginService.IsNullService())
                {
                    return;
                }
                var pluginObjects = pluginService.GetPluginObjects();
                foreach (var pluginObject in pluginObjects)
                {
                    options.PlugInSources.AddFolder(pluginObject.Path);
                }
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(); //Initializes ABP framework.

            app.UseCors(DefaultCorsPolicyName); //Enable CORS!

            
            app.UseStaticFiles();
            app.UseEmbeddedFiles();

            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

#if FEATURE_SIGNALR
            //Integrate to OWIN
            app.UseAppBuilder(ConfigureOwinServices);
#endif

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.InjectOnCompleteJavaScript("/swagger/ui/abp.js");
                options.InjectOnCompleteJavaScript("/swagger/ui/on-complete.js");
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MainProject API V1");
            }); //URL: /swagger
        }

#if FEATURE_SIGNALR
        private static void ConfigureOwinServices(IAppBuilder app)
        {
            app.Properties["host.AppName"] = "MainProject";

            app.UseAbp();
            
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true
                };
                map.RunSignalR(hubConfiguration);
            });
        }
#endif
    }
}
