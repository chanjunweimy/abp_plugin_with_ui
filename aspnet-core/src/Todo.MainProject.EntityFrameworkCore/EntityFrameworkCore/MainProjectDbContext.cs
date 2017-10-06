using Abp.Zero.EntityFrameworkCore;
using Todo.MainProject.Authorization.Roles;
using Todo.MainProject.Authorization.Users;
using Todo.MainProject.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Todo.MainProject.EntityFrameworkCore
{
    public class MainProjectDbContext : AbpZeroDbContext<Tenant, Role, User, MainProjectDbContext>
    {
        /* Define an IDbSet for each entity of the application */
        
        public MainProjectDbContext(DbContextOptions<MainProjectDbContext> options)
            : base(options)
        {

        }
    }
}
