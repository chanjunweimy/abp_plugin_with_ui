using Abp.Authorization;
using Todo.MainProject.Authorization.Roles;
using Todo.MainProject.Authorization.Users;

namespace Todo.MainProject.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
