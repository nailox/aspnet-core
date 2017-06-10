using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BookStore.Authorization;

namespace BookStore
{
    [DependsOn(
        typeof(BookStoreCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class BookStoreApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<BookStoreAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BookStoreApplicationModule).GetAssembly());
        }
    }
}