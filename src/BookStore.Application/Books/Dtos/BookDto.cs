using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Books.Dtos
{
    [AutoMap(typeof(Book))]
    public class BookDto : EntityDto<long>
    {
        
        public  string AuthorName { get; set; }
        public  string ISBN { get; set; }
        public  string Summary { get; set; }
        public  string Title { get; set; }
        public  long? UserId { get; set; }

        public  int Year { get; set; }

        public string ImageLink { get; set; }

        public float? Rating { get; set; }
    }
}
