using System.ComponentModel.DataAnnotations;

namespace BookStore.Books.Dtos
{
    public class DeleteBookInput
    {
        [Required]
        public int BookId { get; set; }
    }
}