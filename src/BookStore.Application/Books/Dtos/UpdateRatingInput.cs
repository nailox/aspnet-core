using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Books.Dtos
{
   public class UpdateRatingInput
    {
        [Required]
        public int Id { get; set; } //Id of book to update rating

        public int NewRating { get; set; }
    }
}
