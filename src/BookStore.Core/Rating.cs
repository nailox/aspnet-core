using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStore
{
    [Table("Ratings")]
   public class Rating : Entity
    {
        public virtual long? UserId { get; set; }

        public virtual int BookId { get; set; }

        public Rating()
        {
            
        }
    }
}
