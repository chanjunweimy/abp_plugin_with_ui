using System.Threading.Tasks;
using Abp.Application.Services;
using Todo.MainProject.Sessions.Dto;

namespace Todo.MainProject.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
