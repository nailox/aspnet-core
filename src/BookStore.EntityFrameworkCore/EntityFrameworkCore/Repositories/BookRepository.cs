using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFrameworkCore;
using BookStore.EntityFrameworkCore;
using BookStore.EntityFramework.Repositories;

namespace BookStore.EntityFrameworkCore.Repositories
{
    class BookRepository : BookStoreRepositoryBase<Book, int>
    {
        protected BookRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}

