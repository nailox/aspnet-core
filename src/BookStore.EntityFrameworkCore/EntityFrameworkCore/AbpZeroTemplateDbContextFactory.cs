using BookStore.Configuration;
using BookStore.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace BookStore.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class BookStoreDbContextFactory : IDbContextFactory<BookStoreDbContext>
    {
        public BookStoreDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<BookStoreDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            BookStoreDbContextConfigurer.Configure(builder, configuration.GetConnectionString(BookStoreConsts.ConnectionStringName));
            
            return new BookStoreDbContext(builder.Options);
        }
    }
}