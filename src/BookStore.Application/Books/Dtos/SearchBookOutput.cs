using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Books.Dtos
{
   public class SearchBookOutput
    {
        public List<BookDto> BookResults{ get; set; }
    }
}
