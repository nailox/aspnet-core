using System.Reflection;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BookStore.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BookStore.Web.Host.Startup
{
    [DependsOn(
       typeof(BookStoreWebCoreModule))]
    public class BookStoreWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BookStoreWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BookStoreWebHostModule).GetAssembly());
        }
    }
}
