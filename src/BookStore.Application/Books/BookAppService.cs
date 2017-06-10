using BookStore.Books.Dtos;
using System;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using System.Collections.Generic;
using Abp.AutoMapper;
using BookStore.Users;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using BookStore.Authorization.Users;

namespace BookStore.Books
{
    public class BookAppService : ApplicationService,  IBookAppService
    {

        private readonly IRepository<Book> _bookRepository;
        private readonly UserManager _userManager;
        private readonly IObjectMapper _objectMapper;


        public BookAppService(IRepository<Book> bookRepository, UserManager userManager, IObjectMapper mpr)
        {
            _bookRepository = bookRepository;
        
            _userManager = userManager;

            _objectMapper = mpr;
            
        }

        public SearchBookOutput SearchBook (SearchBookInput input)
        {

            //null check ? 
            var books = _bookRepository.Single(book => book.ISBN == input.ISBN || book.AuthorName == input.AuthorName);

            return new SearchBookOutput
            {
               // BookResults = Mapper.Map<List<BookDto>>(books)
            };

           
        }

     
        public void CreateBook(CreateBookInput input)
        {
            //TODO: add logger
            
            try
            {
              
                var book = new Book
                {
                    Title = input.Title,
                    Summary = input.Summary,
                    Year = input.Year,
                    ISBN = input.ISBN,
                    UserId = AbpSession.UserId,
                    AuthorName = input.AuthorName


                };

                _bookRepository.Insert(book);

                System.Diagnostics.Debug.WriteLine("book insterted in repo");

            }

            catch (Exception e)
            { Console.WriteLine(e.StackTrace); }
      

    
}

      //show all books 
        public GetBooksOutput GetBooks (GetAllBooksInput input)
        {
            if (input.UserId == null) {
            var books = _bookRepository.GetAllList();

                return new GetBooksOutput
                {
                    //  Books = List<BookDto>();
                    // Books = Mapper.Map<List<BookDto>>(books)
                    Books = _objectMapper.Map<List<BookDto>>(books)
            };
            }

            else
            {
                var books = _bookRepository.GetAllList(book => book.UserId == input.UserId);

                return new GetBooksOutput
                {
                    Books = _objectMapper.Map<List<BookDto>>(books)
                };
            }
        }
        //show my books , also get here from logo click
     
     

        //buttons 
        public void UpdateBook(UpdateBookInput input)
        {
            var book = _bookRepository.Get(input.BookId);

            if (!String.IsNullOrEmpty(input.ISBN))
            {

                book.ISBN = input.ISBN;
            }

            if (!String.IsNullOrEmpty(input.Summary))
            {
                book.Summary = input.Summary;
            }

            if (input.Year.HasValue)
            {
                book.Year = input.Year.Value;
            }

            _bookRepository.Update(book);

            }
        public void DeleteBook(DeleteBookInput input)
        {
            _bookRepository.Delete(input.BookId);
        }

    }
}
