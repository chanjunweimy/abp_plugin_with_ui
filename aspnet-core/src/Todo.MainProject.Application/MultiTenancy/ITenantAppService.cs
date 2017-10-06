using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Todo.MainProject.MultiTenancy.Dto;

namespace Todo.MainProject.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
