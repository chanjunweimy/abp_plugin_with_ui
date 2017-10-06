using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Todo.MainProject.Roles.Dto;
using Todo.MainProject.Users.Dto;

namespace Todo.MainProject.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}