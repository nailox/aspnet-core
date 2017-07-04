using Abp.Application.Services;
using BookStore.Books.Dtos;
using BookStore.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public interface IBookAppService : IApplicationService
    {
        void UpdateBook(UpdateBookInput input);
        void CreateBook(CreateBookInput input);
        void DeleteBook(DeleteBookInput input);

        Boolean UpdateRating(UpdateRatingInput input);

        GetBooksOutput GetBooks(GetAllBooksInput input);



    }
}
