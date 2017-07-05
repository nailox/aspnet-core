using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Books.Dtos
{
   public class UpdateRatingInput
    {
        
        public int Id { get; set; } //Id of book to update rating

        public float? NewRating { get; set; }
    }
}
