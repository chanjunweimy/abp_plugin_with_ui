using System.Threading.Tasks;
using Abp.Application.Services;
using Todo.MainProject.Authorization.Accounts.Dto;

namespace Todo.MainProject.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
