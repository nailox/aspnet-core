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
        [Required]
        public int BookId { get; set; }

        public string ISBN { get; set; }

        public string Summary { get; set; }


        public int? Year { get; set; }
    }
}
