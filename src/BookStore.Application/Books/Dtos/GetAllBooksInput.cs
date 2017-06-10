using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System;

namespace BookStore.Books.Dtos
{
    public class GetAllBooksInput : IPagedResultRequest
    {
        public int MaxResultCount
        {
            get;set;
        }

        public int SkipCount
        {
            get; set;

        }

        public int? UserId { get; set; }
    }
}