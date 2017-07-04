using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Runtime.Session;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore
{
    [Table("Books")]
    public class Book : Entity
    {
      
     
        public virtual string AuthorName { get; set; }
        public virtual string ISBN { get; set; }
        public virtual string Summary { get; set; }
        public virtual string Title { get; set; }
        public virtual long? UserId { get; set; }

        public virtual int Year { get; set; }

        public virtual string ImageLink { get; set; }

        public virtual float? Rating { get; set; }



        public Book ()
        {
            //UserId = IAbpSession.UserId;
        }
    }
}
