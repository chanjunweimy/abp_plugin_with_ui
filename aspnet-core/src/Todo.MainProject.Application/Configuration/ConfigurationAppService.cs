using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Todo.MainProject.Configuration.Dto;

namespace Todo.MainProject.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MainProjectAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
