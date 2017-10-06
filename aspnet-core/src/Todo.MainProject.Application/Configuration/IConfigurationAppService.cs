using System.Threading.Tasks;
using Todo.MainProject.Configuration.Dto;

namespace Todo.MainProject.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}