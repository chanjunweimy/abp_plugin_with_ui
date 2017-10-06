using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Todo.MainProject.Controllers
{
    public abstract class MainProjectControllerBase: AbpController
    {
        protected MainProjectControllerBase()
        {
            LocalizationSourceName = MainProjectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}