using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Todo.MainProject.Roles.Dto;

namespace Todo.MainProject.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
