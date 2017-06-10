using Abp.Zero.EntityFrameworkCore;
using BookStore.Authorization.Roles;
using BookStore.Authorization.Users;
using BookStore.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace BookStore.EntityFrameworkCore
{
    public class BookStoreDbContext : AbpZeroDbContext<Tenant, Role, User, BookStoreDbContext>
    {
        /* Define an IDbSet for each entity of the application */
        public virtual DbSet<Book> Books { get; set; }
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {

        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //...
        }
    }
}
