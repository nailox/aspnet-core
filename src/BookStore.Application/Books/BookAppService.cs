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
using System.Linq;
using Castle.Core.Logging;
using Abp.Extensions;

namespace BookStore.Books
{
    public class BookAppService : ApplicationService,  IBookAppService
    {

        private readonly IRepository<Book> _bookRepository;
        private readonly UserManager _userManager;
        private readonly IObjectMapper _objectMapper;
        public ILogger Logger { get; set; }

        public BookAppService(IRepository<Book> bookRepository, UserManager userManager, IObjectMapper mpr)
        {
            _bookRepository = bookRepository;
        
            _userManager = userManager;

            _objectMapper = mpr;

            Logger = NullLogger.Instance;

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

                Logger.Info("Inserted book with title: " + book.Title);

            }

            catch (Exception e)
            { Console.WriteLine(e.StackTrace); }
      

    
}

 
        public GetBooksOutput GetBooks (GetAllBooksInput input)
        {
           
                if (input.Filter.IsNullOrEmpty())
            { 
                var books = _bookRepository.GetAllList().Skip(input.SkipCount).Take(input.MaxResultCount);
                return new GetBooksOutput
                {

                    Books = _objectMapper.Map<List<BookDto>>(books),
                    TotalCount = _bookRepository.Count()
                };
            }
                else
                {
                    var books = _bookRepository.GetAllList(
                        book => book.Title.Contains(input.Filter) ||  book.ISBN.Contains(input.Filter) || book.AuthorName.Contains(input.Filter))
                        .Skip(input.SkipCount).Take(input.MaxResultCount);

                //get count of filtered books for paging 
                var countFiltered = _bookRepository.GetAllList(
                        book => book.Title.Contains(input.Filter) || book.ISBN.Contains(input.Filter) || book.AuthorName.Contains(input.Filter));



                return new GetBooksOutput
                {

                    Books = _objectMapper.Map<List<BookDto>>(books),
                    TotalCount = countFiltered.Count()
                    };
                }
       
        }

        public GetBooksOutput GetMyBooks(GetAllBooksInput input)
        {
            // show books only for current user
            var books = _bookRepository.GetAllList(book => book.UserId == input.UserId).Skip(input.SkipCount).Take(input.MaxResultCount);

            return new GetBooksOutput
            {
                Books = _objectMapper.Map<List<BookDto>>(books),
                TotalCount = _bookRepository.Count(book => book.UserId == input.UserId)
            };
        }



        //buttons 
        public void UpdateBook(UpdateBookInput input)
        {
            
            Logger.Info(" Updated book with title: " + input.Title);


             var book = _bookRepository.Get(input.Id);

            if (!String.IsNullOrEmpty(input.Title))
            {

                book.Title = input.Title;
            }

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

          //  _bookRepository.Update(book);

            }
        public void DeleteBook(DeleteBookInput input)
        {
            _bookRepository.Delete(input.Id);

            Logger.Info(" Deleted book with id: " + input.Id);
        }

    }
}
