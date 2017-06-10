using BookStore.Authorization.Roles;
using BookStore.Authorization.Users;
using BookStore.MultiTenancy;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Identity
{
    public static class IdentityRegistrar
    {
        public static void Register(ServiceCollection services)
        {
            services.AddLogging();

            services.AddAbpIdentity<Tenant, User, Role, SecurityStampValidator>(options =>
                {
                    options.Cookies.ApplicationCookie.AuthenticationScheme = "AbpZeroTemplateAuthSchema";
                    options.Cookies.ApplicationCookie.CookieName = "AbpZeroTemplateAuth";
                })
                .AddUserManager<UserManager>()
                .AddRoleManager<RoleManager>()
                .AddSignInManager<SignInManager>()
                .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();
        }
    }
}
