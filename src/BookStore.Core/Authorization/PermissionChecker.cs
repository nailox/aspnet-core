using Abp.Authorization;
using BookStore.Authorization.Roles;
using BookStore.Authorization.Users;
using BookStore.MultiTenancy;

namespace BookStore.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
