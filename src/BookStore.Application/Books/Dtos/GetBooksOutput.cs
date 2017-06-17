using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Books.Dtos
{
    public class GetBooksOutput : IHasTotalCount
    {
        public List<BookDto> Books { get; set; }
        public int TotalCount { get ; set; }
    }
}
