using System.Reflection;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Todo.MainProject.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Todo.MainProject.Web.Host.Startup
{
    [DependsOn(
       typeof(MainProjectWebCoreModule))]
    public class MainProjectWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MainProjectWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MainProjectWebHostModule).GetAssembly());
        }
    }
}
