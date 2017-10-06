using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Todo.DemoPlugin.Web.Host.Startup
{
    [DependsOn(
       typeof(DemoPluginWebCoreModule))]
    public class DemoPluginWebHostModule: AbpModule
    {
        public DemoPluginWebHostModule(IHostingEnvironment env)
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DemoPluginWebHostModule).GetAssembly());
        }
    }
}
