using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using BookStore.Localization;
using Abp.Zero.Configuration;
using BookStore.MultiTenancy;
using BookStore.Authorization.Roles;
using BookStore.Authorization.Users;
using BookStore.Timing;
using Abp.MultiTenancy;
using Abp.Localization;

namespace BookStore
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class BookStoreCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            BookStoreLocalizationConfigurer.Configure(Configuration.Localization);

            Configuration.Localization.Languages.Add(new LanguageInfo("bg", "Български", "famfamfam-flag-bulgaria", true));

            //Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = BookStoreConsts.MultiTenancyEnabled;

            //Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Modules.Zero().RoleManagement.StaticRoles.Add(new StaticRoleDefinition("Author", MultiTenancySides.Tenant));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BookStoreCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}