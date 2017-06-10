using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Books.Dtos
{
    public class SearchBookInput
    {
        public string AuthorName { get; set; }
        public string ISBN { get; set; }

        public string Summary { get; set; }


        public int? Year { get; set; }
    }
}
