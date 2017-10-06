using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Todo.DemoPlugin
{
    [DependsOn(
         typeof(DemoPluginApplicationModule),
         typeof(AbpAspNetCoreModule)
     )]
    public class DemoPluginWebCoreModule : AbpModule
    {
        public DemoPluginWebCoreModule(IHostingEnvironment env)
        {
        }

        public override void PreInitialize()
        {
            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(DemoPluginApplicationModule).GetAssembly()
                 );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DemoPluginWebCoreModule).GetAssembly());
        }
    }
}
