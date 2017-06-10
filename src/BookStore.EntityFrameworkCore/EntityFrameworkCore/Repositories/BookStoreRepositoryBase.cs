using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using BookStore.EntityFrameworkCore;

namespace BookStore.EntityFramework.Repositories
{
    public abstract class BookStoreRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<BookStoreDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected BookStoreRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class BookStoreRepositoryBase<TEntity> : BookStoreRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected BookStoreRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
