using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Books.Dtos
{
    public class UpdateBookInput
    {
        
        public int Id { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public string Summary { get; set; }


        public int? Year { get; set; }
    }
}
