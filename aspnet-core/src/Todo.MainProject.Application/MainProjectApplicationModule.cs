using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Todo.MainProject.Authorization;

namespace Todo.MainProject
{
    [DependsOn(
        typeof(MainProjectCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MainProjectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MainProjectAuthorizationProvider>();
        }

        public override void Initialize()
        {
            Assembly thisAssembly = typeof(MainProjectApplicationModule).GetAssembly();
            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                //Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg.AddProfiles(thisAssembly);
            });
        }
    }
}