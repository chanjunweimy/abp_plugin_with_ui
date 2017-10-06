using System.Reflection;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Todo.DemoPlugin
{
    public class DemoPluginApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            var thisAssembly = typeof(DemoPluginApplicationModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
    }
}